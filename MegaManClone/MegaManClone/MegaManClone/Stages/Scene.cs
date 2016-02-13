using MegaManClone.Controllers;
using MegaManClone.Entities;
using MegaManClone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Stages
{
    class Scene
    {
        #region Fields

        protected Camera camera;
        List<IController> controllers = new List<IController>();
        protected MegamanGame game;
        protected Megaman megaman;
        Queue<ISprite> newSpriteQueue = new Queue<ISprite>();
        protected Scene previousScene;
        protected List<ISprite> sprites = new List<ISprite>();

        #endregion

        #region Constructor

        public Scene(String background, MegamanGame game, Vector2 sceneSize)
        {
            this.game = game;
            this.megaman = game.Megaman;

            camera = new Camera(game.Content.Load<Texture2D>(background), game.GraphicsDevice, sceneSize);
        }

        #endregion

        #region XNA Lifecycle

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise, null, camera.Transform);
            camera.DrawBackground(spriteBatch);
            foreach (ISprite sprite in sprites)
            {
                sprite.Draw(spriteBatch, camera);
            }
            spriteBatch.End();
        }

        public virtual void Update(GameTime gameTime)
        {
            while (newSpriteQueue.Count > 0)
            {
                sprites.Add(newSpriteQueue.Dequeue());
            }

            foreach (IController controller in controllers)
            {
                controller.Update();
            }

            foreach (ISprite sprite in sprites)
            {
                sprite.Update(gameTime);
            }
        }

        #endregion

        #region Methods

        public virtual void AddSprite(ISprite sprite)
        {
            newSpriteQueue.Enqueue(sprite);
        }

        public virtual void AddController(IController controller)
        {
            controllers.Add(controller);
        }

        public virtual void Enter()
        {
            previousScene = game.CurrentScene;
            game.CurrentScene = this;
        }

        #endregion
    }
}
