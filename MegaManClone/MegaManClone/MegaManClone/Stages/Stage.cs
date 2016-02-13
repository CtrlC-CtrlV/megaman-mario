using MegaManClone.Commands;
using MegaManClone.Controllers;
using MegaManClone.Entities;
using MegaManClone.Entities.MegamanStates;
using MegaManClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MegaManClone.Stages
{
    class Stage : Scene
    {
        #region Fields

        Song backgroundMusic;
        int cellHeight = 50;
        int cellWidth = 50;
        Vector2 checkpoint;
        List<ICollidable> collidables = new List<ICollidable>();
        bool complete = false;
        int completeTimer = 0;
        readonly int completeTimeOut = 2500;
        int deathTimeOut = 2500;
        int deathTimer = 0;
        string id;
        Vector2 megamanInitialPosition;
        bool megamanIsDead = false;
        int millisecondsElapsed = 0;
        bool inputRecently = false;
        int inputTimeOut = 500;
        int inputTimer = 0;
        double projectionTime = 0.05d;
        SortedList<int, string> scores = new SortedList<int, string>(new IntComparer());
        int timeLimit = 400; // In seconds / timerScale
        bool timerRunning = true;
        double timerScale = 2.5;

        #endregion

        #region Properties

        public Camera Camera
        {
            get { return camera; }
        }

        public Vector2 Checkpoint
        {
            get { return checkpoint; }
            set { checkpoint = value; }
        }

        public Vector2 MegamanInitialPosition
        {
            get { return megamanInitialPosition; }
            set { megamanInitialPosition = value; }
        }

        public int Timer
        {
            get
            {
                return Math.Max(timeLimit - (int)(millisecondsElapsed * timerScale) / 1000, 0);
            }
        }

        #endregion

        #region Types

        [Serializable()]
        class IntComparer : IComparer<int>
        {
            public int Compare(int x, int y)
            {
                return y.CompareTo(x);
            }
        }

        #endregion

        #region Constructor

        public Stage(MegamanGame game, Vector2 boundary, string backgroundName, string backgroundMusicName, string id)
            : base (backgroundName, game, boundary)
        {
            this.id = id;
            backgroundMusic = game.Content.Load<Song>(backgroundMusicName);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(backgroundMusic);

            collidables.Add(megaman);
            AddSprite(megaman);

            string filename = id + "_scores.dat";
            if (File.Exists(filename))
            {
                Stream fileStream = File.OpenRead(filename);
                BinaryFormatter deserializer = new BinaryFormatter();
                scores = (SortedList<int, string>)deserializer.Deserialize(fileStream);
                fileStream.Close();
            }
        }

        #endregion

        #region Scene Overrides

        public override void Enter()
        {
            base.Enter();
            megaman.InitialPosition = megamanInitialPosition;
            megaman.CurrentStage = this;
            megaman.Reset();
            megaman.Lives = 3;
        }

        public override void Update(GameTime gameTime)
        {
            if (timerRunning)
            {
                millisecondsElapsed += gameTime.ElapsedGameTime.Milliseconds;
            }

            if (Timer == 0 && timerRunning)
            {
                megaman.Die();
                megamanIsDead = true;
                timerRunning = false;
            }

            if (megamanIsDead)
            {
                deathTimer += gameTime.ElapsedGameTime.Milliseconds;

                if (deathTimer >= deathTimeOut)
                {
                    deathTimer = 0;

                    megamanIsDead = false;

                    if (megaman.Lives > 0)
                    {
                        megaman.Lives--;
                        Reset();

                    } else
                    {
                        game.SceneFactory.GetScene("gameover").Enter();
                    }
                }
            }

            if (complete)
            {
                completeTimer += gameTime.ElapsedGameTime.Milliseconds;
                if (completeTimer >= completeTimeOut)
                {
                    megaman.Points += Timer * 6;
                    game.SceneFactory.GetScene("victory").Enter();
                }
            }

            if (inputRecently)
            {
                inputTimer += gameTime.ElapsedGameTime.Milliseconds;
                if (inputTimer >= inputTimeOut)
                {
                    inputTimer = 0;
                    inputRecently = false;
                }
            }

            base.Update(gameTime);

            detectCollisions();
            camera.Update(megaman.AABB);

            if (checkpoint != Vector2.Zero && megaman.CurrentSprite.Position.X > checkpoint.X)
            {
                megaman.InitialPosition = checkpoint;
            }

            if (megaman.CurrentPowerUpState is MegamanDeadState && !complete)
            {
                megamanIsDead = true;
                timerRunning = false;
            }
        }

        #endregion

        #region Collision Detection

        void broadSweep(Dictionary<Point, List<ICollidable>> cells, List<Point> cellsWithMotion)
        {
            foreach (ICollidable currentObject in collidables.Where(item => item.Active).ToList())
            {
                List<Point> cellsOccupied = getCellsOccupied(currentObject.AABB);

                foreach (Point currentCell in cellsOccupied)
                {
                    if (!cells.ContainsKey(currentCell))
                    {
                        cells.Add(currentCell, new List<ICollidable>());
                    }
                    cells[currentCell].Add(currentObject);

                    if (currentObject.Velocity != Vector2.Zero && !cellsWithMotion.Contains(currentCell))
                    {
                        cellsWithMotion.Add(currentCell);
                    }
                }
            }
        }

        void continuousSimulation(List<ICollidable> objects)
        {
            Rectangle boundingBoxA, boundingBoxB, newBoundingBoxA, newBoundingBoxB;
            Tuple<int, ICollidable, ICollidable> collisionData = null;
            bool collisionOccurred = false;
            int i, j, k, start = 0;
            double time;
            Vector2 velocityA, velocityB;

            continuousPass:
            for (i = 0; i < objects.Count; i++)
            {
                for (j = i + 1; j < objects.Count; j++)
                {
                    if (CollisionHelper.sweptShapeTest(objects[i], objects[j], projectionTime))
                    {
                        boundingBoxA = objects[i].AABB;
                        boundingBoxB = objects[j].AABB;
                        velocityA = objects[i].Velocity;
                        velocityB = objects[j].Velocity;

                        for (k = start; k < 100; k++)
                        {
                            time = k * (projectionTime / 100d);
                            newBoundingBoxA = boundingBoxA;
                            newBoundingBoxB = boundingBoxB;

                            newBoundingBoxA.X += (int)(velocityA.X * time);
                            newBoundingBoxA.Y += (int)(velocityA.Y * time);
                            newBoundingBoxB.X += (int)(velocityB.X * time);
                            newBoundingBoxB.Y += (int)(velocityB.Y * time);

                            if (newBoundingBoxA.Intersects(newBoundingBoxB))
                            {
                                if (!collisionOccurred || collisionData.Item1 > time)
                                {
                                    collisionData = new Tuple<int, ICollidable, ICollidable>(k, objects[i], objects[j]);
                                    collisionOccurred = true;
                                }
                                break;
                            }
                        }
                    }
                }
            }

            if (collisionOccurred)
            {
                collisionData.Item2.Collide(collisionData.Item3);
                collisionData.Item3.Collide(collisionData.Item2);

                objects.Remove(collisionData.Item2);
                objects.Remove(collisionData.Item3);

                start = collisionData.Item1 + 1;
                collisionOccurred = false;
                goto continuousPass;
            }
        }

        void detectCollisions()
        {
            Dictionary<Point, List<ICollidable>> cells = new Dictionary<Point, List<ICollidable>>();
            List<Point> cellsWithMotion = new List<Point>();

            broadSweep(cells, cellsWithMotion);
            narrowSweep(cells, cellsWithMotion);
        }

        List<Point> getCellsOccupied(Rectangle AABB)
        {
            List<Point> cells = new List<Point>();
            Rectangle currentCell = new Rectangle(AABB.X - (AABB.X % cellWidth), AABB.Y - (AABB.Y % cellHeight), cellWidth, cellHeight);

            for (; currentCell.Intersects(AABB); currentCell.X += cellWidth)
            {
                for (; currentCell.Intersects(AABB); currentCell.Y += cellHeight)
                {
                    cells.Add(new Point(currentCell.X / cellWidth, currentCell.Y / cellHeight));
                }

                currentCell.Y = AABB.Y - (AABB.Y % cellHeight);
            }

            return cells;
        }

        void narrowSweep(Dictionary<Point, List<ICollidable>> cells, List<Point> cellsWithMotion)
        {
            int i, j;
            
            foreach (Point currentCell in cellsWithMotion)
            {
                List<ICollidable> objectsInCell = cells[currentCell];

                for (i = 0; i < objectsInCell.Count; i++)
                {
                    for (j = i + 1; j < objectsInCell.Count; j++)
                    {
                        if (CollisionHelper.sweptShapeTest(objectsInCell[i], objectsInCell[j], projectionTime))
                        {
                            continuousSimulation(objectsInCell);

                            // No need to continue in this loop
                            i = objectsInCell.Count;
                            break;
                        }
                    }
                }
            }
        }

        #endregion

        #region Miscellaneous Methods

        public void AddCollidable(ICollidable collidable)
        {
            collidables.Add(collidable);
        }

        public void AddHighScore(int score, string name)
        {
            scores[score] = name;
        }

        public void Complete()
        {
            complete = true;
            timerRunning = false;

            if (scores.Count == 0 || megaman.Points > scores.ElementAt(scores.Count - 1).Key)
            {
                string name = Microsoft.VisualBasic.Interaction.InputBox("Congratulations! You've made a new high score!\nPlease enter your name below:", "New High Score!", "", -1, -1);
                scores.Add(megaman.Points, name);
            }

            Stream fileStream = File.Create(id + "_scores.dat");
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(fileStream, scores);
            fileStream.Close();
        }

        public List<Tuple<int, string>> GetHighScores(int n)
        {
            int i;
            List<Tuple<int, string>> highScores = new List<Tuple<int, string>>();

            for (i = 0; i < Math.Min(scores.Count, n); i++)
            {
                KeyValuePair<int, string> pair = scores.ElementAt(i);
                highScores.Add(new Tuple<int, string>(pair.Key, pair.Value));
            }
            
            return highScores;
        }

        public void Pause()
        {
            if (!inputRecently)
            {
                game.SceneFactory.GetScene("pause").Enter();
                inputRecently = true;
            }
        }

        public void Reset()
        {
            if (!inputRecently)
            {
                foreach (ISprite sprite in sprites)
                {
                    sprite.Reset();
                }

                millisecondsElapsed = 0;
                megamanIsDead = false;
                deathTimer = 0;
                timerRunning = true;
                complete = false;
                inputRecently = true;
            }
        }

        public void Mute()
        {
            if (!inputRecently)
            {
                MediaPlayer.IsMuted = !MediaPlayer.IsMuted;
                inputRecently = true;
            }
        }

        #endregion
    }
}
