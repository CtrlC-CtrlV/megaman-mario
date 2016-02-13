using MegaManClone.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Sprites.ItemSprites
{
    class ZeroHelmet : Sprite
    {
        #region Properties

        Megaman megaman;
        new int millisecondsElapsed = 0;
        int revealSpeed = -150;
        SoundEffect sound;
        int totalMilliseconds = 500;

        #endregion

        #region Constructor

        public ZeroHelmet(ContentManager content, Vector2 position)
            : base (new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32), 210, position, content.Load<Texture2D>("Items/Zero_Helmet"))
        {
            sound = content.Load<SoundEffect>("Sounds/mm7/GetSpecial");
        }

        #endregion

        #region ICollidable Override

        public override void Collide(ICollidable otherObject)
        {
            if (otherObject is Megaman && active)
            {
                sound.Play();

                megaman = otherObject as Megaman;

                megaman.ZeroTransition();
                megaman.Points += 1000;

                if (megaman.MaxHealth - megaman.Health < 10)
                {
                    megaman.Health = megaman.MaxHealth;
                }
                else
                {
                    megaman.Health += 10;
                }


                active = false;
            }
        }

        #endregion

        #region ISprite Override

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (megaman != null)
            {
                millisecondsElapsed += gameTime.ElapsedGameTime.Milliseconds;

                if (millisecondsElapsed >= totalMilliseconds)
                {
                    Collide(megaman);
                }
            }
        }

        #endregion
    }
}
