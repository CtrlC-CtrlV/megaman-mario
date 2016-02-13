using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Stages
{
    class Camera
    {
        #region Fields

        Texture2D background;
        Rectangle backgroundFrame;
        GraphicsDevice graphics;
        Vector2 offset = Vector2.Zero;
        Vector2 position;
        float rotation = 0f;
        Vector2 stageSize;
        float zoom = 1f;

        #endregion
        
        #region Properties

        public Rectangle Frame
        {
            get 
            { 
                return new Rectangle((int)offset.X,
                (int)offset.Y,
                graphics.Viewport.Width,
                graphics.Viewport.Height);
            }
        }

        public Vector2 Offset
        {
            get { return offset; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public Vector2 StageSize
        {
            get { return stageSize; }
        }

        public Matrix Transform
        {
            get
            {
                return Matrix.CreateTranslation(new Vector3(-1 * offset, 0)) *
                    Matrix.CreateRotationZ(rotation) *
                    Matrix.CreateScale(new Vector3(zoom, zoom, 1));
            }
        }

        public float Zoom
        {
            get { return zoom; }
            set { zoom = value < 0.1f ? 0.1f : value; }
        }

        #endregion

        #region Constructor

        public Camera(Texture2D background, GraphicsDevice graphics, Vector2 stageSize)
        {
            this.background = background;
            backgroundFrame = new Rectangle(0, 0, graphics.Viewport.Width, graphics.Viewport.Height);
            this.graphics = graphics;
            this.stageSize = stageSize;
        }

        #endregion

        #region Methods

        public void DrawBackground(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background,
                new Rectangle((int)offset.X, (int)offset.Y, graphics.Viewport.Width, graphics.Viewport.Height), 
                backgroundFrame, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
        }

        public void Update(Rectangle focus)
        {
            Rectangle center = new Rectangle((int)(offset.X + graphics.Viewport.Width * 0.4),
                (int)(offset.Y + graphics.Viewport.Height * 0.4),
                (int)(graphics.Viewport.Width * 0.2),
                (int)(graphics.Viewport.Height * 0.2));
            
            if (focus.Left < center.Left)
            {
                offset.X -= center.Left - focus.Left;

            } else if (focus.Right > center.Right)
            {
                offset.X += focus.Right - center.Right;
            }

            if (focus.Top < center.Top)
            {
                offset.Y -= center.Top - focus.Top;

            } else if (focus.Bottom > center.Bottom)
            {
                offset.Y += focus.Bottom - center.Bottom;
            }

            offset.X = MathHelper.Clamp(offset.X, 0, stageSize.X - graphics.Viewport.Width);
            offset.Y = MathHelper.Clamp(offset.Y, 0, stageSize.Y - graphics.Viewport.Height);

            backgroundFrame.X = (int)(offset.X * background.Width / stageSize.X);
            backgroundFrame.Y = (int)(offset.Y * background.Height / stageSize.Y);
            backgroundFrame.Width = (int)(graphics.Viewport.Width * background.Width / stageSize.X);
            backgroundFrame.Height = (int)(graphics.Viewport.Height * background.Height / stageSize.Y);
        }

        #endregion
    }
}
