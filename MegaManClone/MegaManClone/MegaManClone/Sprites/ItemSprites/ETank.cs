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
    class ETank : Sprite
    {
        #region Properties

        Megaman megaman;
        new int millisecondsElapsed;
        SoundEffect sound;
        int totalMilliseconds = 500;

        #endregion

        #region Constructor

        public ETank(ContentManager content, Vector2 position)
            : base(new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32), 150, position, content.Load<Texture2D>("Items/MM_etank"))
        {
            sound = content.Load<SoundEffect>("Sounds/mm1/MM1-GetPoints");
        }

        #endregion

        #region ICollidable Override

        public override void Collide(ICollidable otherObject)
        {
            if (otherObject is Megaman && active)
            {
                sound.Play();

                megaman = otherObject as Megaman;
                megaman.LargeTransition();
                megaman.Points += 1000;
                megaman.Points += 200;
                if (megaman.MaxHealth - megaman.Health < 5)
                {
                    megaman.Health = megaman.MaxHealth;
                }
                else
                {
                    megaman.Health += 5;
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
