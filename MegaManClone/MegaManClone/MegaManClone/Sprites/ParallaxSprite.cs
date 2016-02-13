using MegaManClone.Stages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Sprites
{
    class ParallaxSprite : Sprite
    {
        #region Properties

        Camera camera;
        Vector2 cameraRange;
        Vector2 drift = Vector2.Zero;
        Vector2 maxDrift;

        #endregion

        #region Constructor

        public ParallaxSprite(Camera camera, Rectangle frame, float layerDepth, int millisecondsPerFrame, Vector2 position, Texture2D texture)
            : base (frame, frame, millisecondsPerFrame, position, texture)
        {
            this.camera = camera;
            this.layerDepth = layerDepth;

            Vector2 cameraSize = new Vector2(camera.Frame.Width, camera.Frame.Height);
            Vector2 stageSize = camera.StageSize;

            // Quadratic relationship between layerDepth and apparent "distance"
            cameraRange = (stageSize - cameraSize);
            maxDrift = cameraRange * (float)Math.Pow(layerDepth, 2);
        }

        #endregion

        #region Sprite Overrides

        public override void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            position += drift;
            base.Draw(spriteBatch, camera);
            Position -= drift;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            drift = maxDrift * (camera.Offset / cameraRange);

            if (cameraRange.X == 0)
            {
                drift.X = 0;
            }

            if (cameraRange.Y == 0)
            {
                drift.Y = 0;
            }
        }

        #endregion
    }
}
