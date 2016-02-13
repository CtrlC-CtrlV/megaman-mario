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
    class HiddenFalconPowerup : HiddenItem
    {
        #region Fields

        readonly int pointValue = 1000;

        #endregion

        #region Constructor

        public HiddenFalconPowerup(ContentManager content, Block block)
            : base (new Rectangle(0, 0, 32, 32), block, new Rectangle(0, 0, 32, 32), 175,
            content.Load<Texture2D>("Items/falcon_powerup"), content.Load<SoundEffect>("Sounds/mm3/ProtoWhistle"))
        {

        }

        #endregion

        #region HiddenItem Implementation
        public override void Reveal(Megaman megaman)
        {
            base.Reveal(megaman);
            if (megaman.Velocity.X < 0)
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
            megaman.FalconTransition();
            megaman.Points += pointValue;

            if (megaman.MaxHealth - megaman.Health < 10)
            {
                megaman.Health = megaman.MaxHealth;
            }
            else
            {
                megaman.Health += 10;
            }
        }

        #endregion
    }
}
