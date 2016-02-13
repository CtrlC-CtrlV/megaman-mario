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
    class HiddenETank : HiddenItem
    {
        #region Fields

        readonly int pointValue = 1000;

        #endregion

        #region Constructor

        public HiddenETank(ContentManager content, Block block)
            : base (new Rectangle(0, 0, 32, 32), block, new Rectangle(0, 0, 32, 32), 150,
            content.Load<Texture2D>("Items/MM_etank"), content.Load<SoundEffect>("Sounds/mm1/MM1-GetPoints"))
        {
            
        }

        #endregion

        #region HiddenItem Implementation
        public override void Reveal(Megaman megaman)
        {
            base.Reveal(megaman);
            if (megaman.Velocity.X >= 0)
            {
                this.acceleration.X = 30;
            }
            else
            {
                this.acceleration.X = -30;
            }
        }

        protected override void collect(Megaman megaman)
        {
            megaman.Points += pointValue;
            megaman.Points += 200;
            megaman.LargeTransition();
            if (megaman.MaxHealth - megaman.Health < 5)
            {
                megaman.Health = megaman.MaxHealth;
            }
            else
            {
                megaman.Health += 5;
            }
            
        }

        #endregion
    }
}
