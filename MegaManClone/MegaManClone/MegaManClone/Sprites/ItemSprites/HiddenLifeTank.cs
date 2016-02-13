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
    class HiddenLifeTank : HiddenItem
    {
        #region Fields

        readonly int pointValue = 200;
        new int millisecondsElapsed = 0;
        int totalMilliseconds = 225;

        #endregion

        #region Constructor

        public HiddenLifeTank(ContentManager content, Block block)
            : base (new Rectangle(0, 0, 32, 32), block, new Rectangle(0, 0, 32, 32), 100,
            content.Load<Texture2D>("Items/life_tank"), content.Load<SoundEffect>("Sounds/mm7/GetEnergy"))

        {

        }

        #endregion

        #region HiddenItem Implementation/Override
        protected override void collect(Megaman megaman)
        {
            megaman.Coins++;
            megaman.Points += pointValue;
            if (megaman.Health < megaman.MaxHealth)
            {
                megaman.Health++;
            }
        }

        #endregion

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
    }
}
