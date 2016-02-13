using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MegaManClone.Sprites.ItemSprites;
using Microsoft.Xna.Framework;

namespace MegaManClone.Entities.MegamanStates
{
    class MegamanLargeState : IMegamanPowerUpState
    {
        Megaman megaman;

        #region Constructor

        public MegamanLargeState(Megaman megaman)
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

        }

        void IMegamanPowerUpState.TakeDamage()
        {
            megaman.CurrentPowerUpState = megaman.PowerUpStateMachine.getState(MegamanState.Small);
            megaman.StateChanged();
        }

        void IMegamanPowerUpState.Update(GameTime gameTime)
        {

        }

        void IMegamanPowerUpState.ZeroTransition()
        {
            megaman.CurrentPowerUpState = megaman.PowerUpStateMachine.getState(MegamanState.Zero);
            megaman.StateChanged();
        }


        int IMegamanPowerUpState.GetArmor()
        {
            return 0;
        }

        int IMegamanPowerUpState.GetMaxHealth()
        {
            return 150;
        }
        #endregion
    }
}
