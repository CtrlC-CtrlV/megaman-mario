using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MegaManClone;
using Microsoft.Xna.Framework;
using System.Xml.Serialization;


using MegaManClone.Entities;

namespace MegaManClone
{
    class HighScore : MegamanGame
    {
        private const string HIGH_SCORE_FILE = "\\Content\\highscores.txt";
        private bool isNewHighScore;
        private bool isDead;
        private int highScore;

        Megaman megaman;
        GraphicsDeviceManager graphics;

        [Serializable]
        public struct HighScoreData
        {
            public string[] Name;
            public int[] Score;
            public int Count;

            public HighScoreData(int count)
            {
                Name = new String[count];
                Score = new int[count];
                Count = count;
            }

        }

        #region Constructor
        public HighScore(Megaman megaman, GraphicsDeviceManager graphics)
        {
            this.megaman = megaman;
            this.graphics = graphics;
            isDead = false;

            try
            {
                FileStream fileStream = new FileStream(HIGH_SCORE_FILE, FileMode.Open, FileAccess.Read);
                BinaryReader binaryReader = new BinaryReader(fileStream);

                highScore = binaryReader.ReadInt32();

                binaryReader.Close();
                fileStream.Close();
            }
            catch (IOException)
            {
                highScore = 0;
            }
        }
        #endregion

        #region XNA Lifecycle
        protected override void Initialize()
        {
            base.Initialize();

            // Testing
            HighScoreData highScoreData = new HighScoreData(3);
            highScoreData.Name[0] = "Bobby";
            highScoreData.Score[0] = 1000;

            highScoreData.Name[1] = "Nikit";
            highScoreData.Score[1] = 988;

            highScoreData.Name[2] = "Anna";
            highScoreData.Score[2] = 900;

            WriteScore(highScoreData);

            base.Initialize();
         }
        protected override void LoadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (!isDead)
            {
                // If current players score > current high score, update
                isNewHighScore = true;
            }

            // If game is over and current player has high score
            if (isNewHighScore) // and game over?
            {
                //WriteScore();
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            if (!isDead)
            {
                // write the high score to screen
            }
        }
        #endregion

        #region Misc Methods
        private void WriteScore(HighScoreData highScores)
        {
            try
            {
                FileStream fileStream = new FileStream(HIGH_SCORE_FILE, FileMode.OpenOrCreate, FileAccess.Write);
                
                //BinaryWriter binaryWriter = new BinaryWriter(fileStream);
                //binaryWriter.Write(highScore);

                //binaryWriter.Close();

                XmlSerializer serializer = new XmlSerializer(typeof(HighScoreData));
                serializer.Serialize(fileStream, highScores);
                fileStream.Close();
            }
            catch (IOException)
            {
            }

            isNewHighScore = false;
        }

        private HighScoreData LoadScores()
        {
            HighScoreData highScoreData;

            FileStream fileStream = File.Open(HIGH_SCORE_FILE, FileMode.OpenOrCreate, FileAccess.Read);

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(HighScoreData));
            highScoreData = (HighScoreData)xmlSerializer.Deserialize(fileStream);
            fileStream.Close();
            
            return highScoreData;
        }
        #endregion


    }
}
