using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MegaManClone.Sprites.ItemSprites;
using Microsoft.Xna.Framework;

namespace MegaManClone.Entities.MegamanStates
{
    class MegamanZeroState : IMegamanPowerUpState
    {
        Megaman megaman;

        #region Constructor

        public MegamanZeroState(Megaman megaman)
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
            megaman.CurrentPowerUpState = megaman.PowerUpStateMachine.getState(MegamanState.Large);
            megaman.StateChanged();
        }

        void IMegamanPowerUpState.Update(GameTime gameTime)
        {

        }

        void IMegamanPowerUpState.ZeroTransition()
        {

        }

        int IMegamanPowerUpState.GetArmor()
        {
            return 5;
        }

        int IMegamanPowerUpState.GetMaxHealth()
        {
            return 150;
        }

        #endregion
    }
}
