
using MegaManClone.Entities;
using MegaManClone.Sprites.BlockSprites;
using MegaManClone.Stages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Sprites
{
    abstract class Sprite : ISprite, ICollidable
    {
        #region Fields

        protected Rectangle     aabb;
        protected Vector2       acceleration = Vector2.Zero;
        protected Boolean       active = true;
        readonly  Vector2       dampeningFactor = new Vector2(0.75f, 0.9f);
        protected SpriteEffects effects = SpriteEffects.None;
        protected int           fallAcceleration = 1200;
        protected Rectangle     frame;
        protected Vector2       initialPosition;
        protected float         layerDepth = 0.001f;
        protected Vector2       maxVelocity = new Vector2(200, 1000);
        protected int           millisecondsElapsed = 0;
        protected int           millisecondsPerFrame;
        protected Vector2       minVelocity = new Vector2(-200, -1000);
        protected Vector2       position;
                  float         rotation = 0;
                  float         scale = 1;
        protected Texture2D     texture;
        protected Vector2       velocity = Vector2.Zero;
        protected Boolean       visible = true;

        #endregion

        #region Properties

        public Rectangle AABB
        {
            get
            {
                Rectangle currentAABB = aabb;

                // Center AABB over sprite frame
                currentAABB.X = (int)(position.X + (frame.Width - currentAABB.Width) / 2);
                currentAABB.Y = (int)(position.Y + (frame.Height - currentAABB.Height) / 2);

                return currentAABB;
            }
        }

        public Vector2 Acceleration
        {
            get { return acceleration; }
            set { acceleration = value; }
        }

        public Boolean Active
        {
            get { return active; }
            set { active = value; }
        }

        public SpriteEffects Effects
        {
            get { return effects; }
            set { effects = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        #endregion

        #region Constructor
        
        public Sprite(Rectangle aabb, Rectangle frame, int millisecondsPerFrame, Vector2 position, Texture2D texture)
        {
            this.aabb = aabb;
            this.frame = frame;
            this.millisecondsPerFrame = millisecondsPerFrame;
            this.initialPosition = position;
            this.position = position;
            this.texture = texture;
        }

        #endregion

        #region ICollidable Implementation

        public virtual void BlockMovement(ICollidable otherObject)
        {
            Rectangle otherAABB = otherObject.AABB;

            switch (CollisionHelper.GetCollisionSide(this, otherObject))
            {
                case CollisionSide.Bottom:
                    position.Y = otherAABB.Top - AABB.Height;
                    velocity.Y = 0;
                    acceleration.Y = 0;
                    break;
                case CollisionSide.Left:
                    position.X = otherAABB.Right;
                    velocity.X = 0;
                    acceleration.X = 0;
                    break;
                case CollisionSide.Right:
                    position.X = otherAABB.Left - AABB.Width;
                    velocity.X = 0;
                    acceleration.X = 0;
                    break;
                case CollisionSide.Top:
                    position.Y = otherAABB.Bottom;
                    velocity.Y = 0;
                    acceleration.Y = 0;
                    break;
            }
        }

        public virtual void Collide(ICollidable otherObject)
        {
            
        }

        public virtual void TakeDamage(ICollidable otherObject, int Damage)
        {
            
        }

        public virtual void TriggerFall()
        {
            acceleration.Y = fallAcceleration;
        }

        #endregion

        #region ISprite Implementation

        public virtual void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            visible = AABB.Intersects(camera.Frame);
            if (active && visible)
            {
                Vector2 tempPosition = new Vector2(position.X, position.Y);
                spriteBatch.Draw(texture, tempPosition, frame, Color.White, rotation, Vector2.Zero, scale, effects, layerDepth);
#if DEBUG
                drawHitbox(spriteBatch, 2, Color.Red, tempPosition);
#endif
            }
            
        }

        public virtual void Reset()
        {
            active = true;
            position = initialPosition;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (active && visible)
            {
                if (millisecondsPerFrame > 0)
                {
                    millisecondsElapsed += gameTime.ElapsedGameTime.Milliseconds;
                    if (millisecondsElapsed > millisecondsPerFrame)
                    {
                        millisecondsElapsed -= millisecondsPerFrame;
                        frame.X += frame.Width;

                        if (frame.X >= texture.Width)
                        {
                            frame.X = 0;
                        }
                    }
                }

                velocity += acceleration * (float)gameTime.ElapsedGameTime.Milliseconds / 1000f;
                velocity.X = MathHelper.Clamp(velocity.X, minVelocity.X, maxVelocity.X);
                velocity.Y = MathHelper.Clamp(velocity.Y, minVelocity.Y, maxVelocity.Y);
                position += velocity * (float)gameTime.ElapsedGameTime.Milliseconds / 1000f;

                if (acceleration.X == 0)
                {
                    velocity.X *= dampeningFactor.X;
                }

                if (acceleration.Y == 0)
                {
                    velocity.Y *= dampeningFactor.Y;
                }
            }
        }

        #endregion

        #region Miscellaneous Methods

        void drawHitbox(SpriteBatch spriteBatch, int border, Color color, Vector2 position)
        {
            int xPosition = (int)(position.X);
            int yPosition = (int)(position.Y);
            
            spriteBatch.Draw(texture, new Rectangle(xPosition, yPosition, aabb.Width, border), color);
            spriteBatch.Draw(texture, new Rectangle(xPosition, yPosition, border, aabb.Height), color);
            spriteBatch.Draw(texture, new Rectangle(xPosition + aabb.Width - border, yPosition, border, aabb.Height), color);
            spriteBatch.Draw(texture, new Rectangle(xPosition, yPosition + aabb.Height - border, aabb.Width, border), color);
        }

        #endregion
    }
}
