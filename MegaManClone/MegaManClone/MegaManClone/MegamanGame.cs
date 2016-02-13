using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using MegaManClone.Sprites;
using MegaManClone.Entities;
using MegaManClone.Commands;
using MegaManClone.Controllers;
using MegaManClone.Stages;
using MegaManClone.Entities.BlockStates;
using MegaManClone.Sprites.EnemySprites;
using MegaManClone.Sprites.ItemSprites;
using MegaManClone.Sprites.Background;
using System.Runtime.InteropServices;

namespace MegaManClone
{
    [ComVisible(false)]
    public class MegamanGame : Game
    {
        #region Fields

        Scene currentScene;
        GraphicsDeviceManager graphics;
        internal Megaman Megaman;
        SpriteBatch spriteBatch;
        StageFactory sceneFactory;
        HighScore highscore;

        #endregion

        #region Properties

        internal Scene CurrentScene
        {
            get { return currentScene; }
            set { currentScene = value; }
        }

        internal StageFactory SceneFactory
        {
            get { return sceneFactory; }
        }

        #endregion

        #region Constructor

        public MegamanGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        #endregion

        #region XNA Lifecycle

        protected override void Initialize()
        {
            Megaman = new Megaman(Content);
            sceneFactory = new StageFactory(this);
            sceneFactory.GetScene("start").Enter();

            highscore = new HighScore(Megaman, graphics);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            CurrentScene.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            CurrentScene.Draw(spriteBatch);

            base.Draw(gameTime);
        }

        #endregion`
    }
}
