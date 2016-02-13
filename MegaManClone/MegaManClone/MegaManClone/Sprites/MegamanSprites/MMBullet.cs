using MegaManClone.Entities;
using MegaManClone.Entities.MegamanStates;
using MegaManClone.Stages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace MegaManClone.Sprites.MegamanSprites
{
    class MMBullet : Sprite
    {
        #region Fields

        SoundEffect sound;
        int Damage = 10;
        Megaman megaman;

        #endregion

        #region Constructor
        public MMBullet(ContentManager content, Rectangle mmBox, int direction, Megaman megaman)
            : base(new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32), 0, Vector2.Zero, content.Load<Texture2D>("megaman/MMBullet"))
        {
            sound = content.Load<SoundEffect>("Sounds/mm7/MMShot1");

            maxVelocity.X = 250;
            minVelocity.X = -250;
            velocity.X = 250 * direction;
            acceleration.X = 1 * direction;

            if (direction < 0)
            {
                position.X = mmBox.X - aabb.Width;
            }
            else
            {
                position.X = mmBox.X + mmBox.Width;
            }

            position.Y = mmBox.Y + (mmBox.Height / 2) - (aabb.Height / 2);

            this.megaman = megaman;
        }
        #endregion

        #region Sprite Override

        public override void BlockMovement(ICollidable otherObject)
        {
            otherObject.TakeDamage(this, Damage);
            sound.Play();
            active = false;
        }

        public override void Collide(ICollidable otherObject)
        {
            
        }

        public override void Reset()
        {
            active = false;
            position = initialPosition;
        }

        public override void TriggerFall()
        {
            
        }

        #endregion

        #region Misc Methods
        public void GiveMegamanPoints(int points)
        {
            megaman.Points += points;
        }

        #endregion
    }
}
