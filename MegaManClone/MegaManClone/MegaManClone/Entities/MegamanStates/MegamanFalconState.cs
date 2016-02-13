using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Entities.MegamanStates
{
    class MegamanFalconState : IMegamanPowerUpState
    {
        #region Properties

        int millisecondsElapsed = 0;
        Megaman megaman;
        readonly int totalFalconStateLength = 13000;

        #endregion

        #region Constructor

        public MegamanFalconState(Megaman megaman)
        {
            this.megaman = megaman;
        }

        #endregion

        #region IMegamanPowerUpState Implementation

        void IMegamanPowerUpState.FalconTransition()
        {

        }

        void IMegamanPowerUpState.LargeTransition()
        {

        }

        void IMegamanPowerUpState.TakeDamage()
        {

        }

        void IMegamanPowerUpState.Update(GameTime gameTime)
        {
            millisecondsElapsed += gameTime.ElapsedGameTime.Milliseconds;

            if (millisecondsElapsed >= totalFalconStateLength)
            {
                millisecondsElapsed = 0;
                MegamanState megamanState = MegamanStateHelper.GetState(megaman.CurrentActionState, megaman.PreviousPowerUpState);
                megaman.CurrentPowerUpState = megaman.PowerUpStateMachine.getState(megamanState);
                megaman.StateChanged();
            }
        }

        void IMegamanPowerUpState.ZeroTransition()
        {

        }

        int IMegamanPowerUpState.GetArmor()
        {
            return 1000;
        }

        int IMegamanPowerUpState.GetMaxHealth()
        {
            return 150;
        }

        #endregion
    }
}
