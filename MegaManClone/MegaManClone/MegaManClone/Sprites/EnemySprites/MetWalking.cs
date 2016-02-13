using MegaManClone.Entities;
using MegaManClone.Stages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace MegaManClone.Sprites.EnemySprites
{
    class MetWalking : Sprite
    {
        #region Properties

        SoundEffect sound;
        int health;
        int currentHealth;
        int points; 
        int Damage;
        
        #endregion

        #region Constructor

        public MetWalking(ContentManager content, Vector2 position)
            : base (new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32), 175, position, content.Load<Texture2D>("enemies/met_walking"))
        {
            sound = content.Load<SoundEffect>("Sounds/mm7/EnemyHit1");

            acceleration = new Vector2(-150, 0);
            maxVelocity = new Vector2(25, 75);
            minVelocity = new Vector2(-25, -75);

            health = 30;
            Damage = 10;
            points = 100;
            currentHealth = health;
        }

        #endregion

        #region ICollidable Override

        public override void BlockMovement(ICollidable otherObject)
        {
            float newAcceleration = acceleration.X * -1;
            base.BlockMovement(otherObject);
            if (acceleration.X == 0)
            {
                acceleration.X = newAcceleration;
            }
        }

        public override void Collide(ICollidable otherObject)
        {
            CollisionSide collisionSide = CollisionHelper.GetCollisionSide(this, otherObject);
            if (collisionSide != CollisionSide.Top &&
                collisionSide != CollisionSide.None && active)
            {
                otherObject.BlockMovement(this);
                otherObject.TakeDamage(this, Damage);
            }
        }

        public override void TakeDamage(ICollidable otherObject, int Damage)
        {
            sound.Play();
            if (otherObject is Megaman && active)
            {
                (otherObject as Megaman).Points += points;
                currentHealth = 0;
            }
            else if (otherObject is MegaManClone.Sprites.MegamanSprites.MMBullet && active)
            {
                currentHealth -= Damage;
                (otherObject as MegaManClone.Sprites.MegamanSprites.MMBullet).GiveMegamanPoints((int)(points / 3.0));
            }

            if (currentHealth <= 0)
            {
                active = false;
            }
        }

        #endregion

        #region ISprite Override

        public override void Update(GameTime gameTime)
        {
            effects = acceleration.X > 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            base.Update(gameTime);
        }

        public override void Reset()
        {
            base.Reset();
            currentHealth = health;
        }

        #endregion
    }
}
