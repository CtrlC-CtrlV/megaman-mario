using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MegaManClone.Sprites.ItemSprites;
using Microsoft.Xna.Framework;

namespace MegaManClone.Entities.MegamanStates
{
    class MegamanSmallState : IMegamanPowerUpState
    {
        Megaman megaman;

        #region Constructor

        public MegamanSmallState(Megaman megaman)
        {
            this.megaman = megaman;
        }

        #endregion

        #region IMegamanPowerUpState Implementation

        void IMegamanPowerUpState.FalconTransition()
        {
            megaman.CurrentPowerUpState = megaman.PowerUpStateMachine.getState(MegamanState.Falcon);
            megaman.StateChanged();
        }

        void IMegamanPowerUpState.LargeTransition()
        {
            megaman.CurrentPowerUpState = megaman.PowerUpStateMachine.getState(MegamanState.Large);
            megaman.StateChanged();
        }

        void IMegamanPowerUpState.TakeDamage()
        {
            if (megaman.Health <= 0)
            {
                megaman.Die();
            }
        }

        void IMegamanPowerUpState.Update(GameTime gameTime)
        {

        }

        void IMegamanPowerUpState.ZeroTransition()
        {
            megaman.CurrentPowerUpState = megaman.PowerUpStateMachine.getState(MegamanState.Large);
            megaman.StateChanged();
        }

        int IMegamanPowerUpState.GetArmor()
        {
            return 0;
        }

        int IMegamanPowerUpState.GetMaxHealth()
        {
            return 100;
        }

        #endregion
    }
}
