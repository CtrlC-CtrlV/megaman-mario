using MegaManClone.Entities.MegamanStates;
using MegaManClone.Sprites.MegamanSprites;
using MegaManClone.Stages;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Entities
{
    class Buster
    {
        #region Fields

        bool cooldown = false;
        readonly int cooldownTimeout = 75;
        int cooldownTimer = 0;
        int charge = 0;
        bool charging = false;
        Stage currentStage;
        Megaman megaman;

        #endregion

        #region Properties

        internal Stage CurrentStage
        {
            set { currentStage = value; }
        }

        #endregion

        #region Constructor

        public Buster(Megaman megaman)
        {
            this.megaman = megaman;
        }

        #endregion

        #region Methods

        public void Charge()
        {
            if (!cooldown)
            {
                charge++;
                charging = true;
            }
        }

        void fire()
        {
            MMBullet bullet = new MMBullet(megaman.Content, megaman.AABB,
                megaman.Direction == MegamanState.Left ? -1 : 1, megaman);
            currentStage.AddSprite(bullet);
            currentStage.AddCollidable(bullet);
            charge = 0;
        }

        public void Update(GameTime gameTime)
        {
            if (cooldown)
            {
                cooldownTimer += gameTime.ElapsedGameTime.Milliseconds;
                if (cooldownTimer > cooldownTimeout)
                {
                    cooldownTimer = 0;
                    cooldown = false;
                }
            } else
            {
                if (charging)
                {
                    charging = false;

                } else
                {
                    if (charge > 0)
                    {
                        fire();
                        cooldown = true;
                    }
                }
            }
        }

        #endregion
    }
}
