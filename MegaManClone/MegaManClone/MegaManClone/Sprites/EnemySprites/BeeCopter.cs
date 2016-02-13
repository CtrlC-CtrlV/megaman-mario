using MegaManClone.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Sprites.EnemySprites
{
    class BeeCopter : Sprite
    {

        #region Properties

        int cycleLength = 3000;
        new int millisecondsElapsed = 0;
        SoundEffect sound;
        int health;
        int currentHealth;
        int points;
        int Damage;

        #endregion

        #region Constructor

        public BeeCopter(ContentManager content, Vector2 position)
            : base(new Rectangle(0, 0, 128, 96), new Rectangle(0, 0, 128, 96), 89, position, content.Load<Texture2D>("enemies/mmxbeecopter"))
        {
            sound = content.Load<SoundEffect>("Sounds/mm7/EnemyRoach");

            acceleration = new Vector2(-150, 0);
            maxVelocity = new Vector2(25, 75);
            minVelocity = new Vector2(-25, -75);

            health = 60;
            Damage = 20;
            points = 250;
            currentHealth = health;
        }

        #endregion

        #region ICollidable Override

        public override void Collide(ICollidable otherObject)
        {
            if (otherObject.AABB.Bottom > AABB.Top && active)
            {
                otherObject.BlockMovement(this);
                otherObject.TakeDamage(this, Damage);
            }
        }

        public override void TakeDamage(ICollidable otherObject, int Damage)
        {
            sound.Play();
            if (otherObject is MegaManClone.Sprites.MegamanSprites.MMBullet && active)
            {
                currentHealth -= Damage;
                (otherObject as MegaManClone.Sprites.MegamanSprites.MMBullet).GiveMegamanPoints((int)(points / 6.0));

                if (currentHealth <= 0)
                {
                    active = false;
                }
            }
        }

        #endregion

        #region ISprite Override

        public override void Update(GameTime gameTime)
        {
            if (active)
            {
                millisecondsElapsed += gameTime.ElapsedGameTime.Milliseconds;
                if (millisecondsElapsed > cycleLength)
                {
                    millisecondsElapsed -= cycleLength;
                    acceleration.X *= -1;
                }
                effects = acceleration.X > 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            }
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
