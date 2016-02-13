using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Entities.MegamanStates
{
    class MegamanDeadState : IMegamanPowerUpState
    {

        public MegamanDeadState()
        {
            
        }

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

        }

        void IMegamanPowerUpState.ZeroTransition()
        {

        }

        int IMegamanPowerUpState.GetArmor()
        {
            return 0;
        }

        int IMegamanPowerUpState.GetMaxHealth()
        {
            return 0;
        }
        #endregion
    }
}
