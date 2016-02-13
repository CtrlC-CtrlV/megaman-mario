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
    class LifeTank : Sprite
    {
        #region Properties

        Megaman megaman;
        new int millisecondsElapsed = 0;
        int revealSpeed = -150;
        SoundEffect sound;
        int totalMilliseconds = 500;

        #endregion

        #region Constructor

        public LifeTank(ContentManager content, Vector2 position) 
            : base (new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32), 100, position, content.Load<Texture2D>("Items/life_tank"))
        {
            sound = content.Load<SoundEffect>("Sounds/mm7/GetEnergy");
        }

        #endregion

        #region ICollidable Override

        public override void Collide(ICollidable otherObject)
        {
            if (otherObject is Megaman && active)
            {
                sound.Play();
                  
                megaman = otherObject as Megaman;
                megaman.Coins++;
                megaman.Points += 200;
                if (megaman.Health < megaman.MaxHealth)
                {
                    megaman.Health++;
                }
                active = false;
            }
        }

        #endregion
    }
}
