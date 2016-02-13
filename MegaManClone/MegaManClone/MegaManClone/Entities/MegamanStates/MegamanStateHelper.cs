using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Entities.MegamanStates
{
    class MegamanStateHelper
    {
        public static MegamanState GetState(MegamanActionState actionState, IMegamanPowerUpState powerUpState)
        {
            int state = 0x0000;

            if (actionState is MegamanCrouchingState)
            {
                state |= (int)MegamanState.Crouching;

            } else if (actionState is MegamanIdleState)
            {
                state |= (int)MegamanState.Idle;

            } else if (actionState is MegamanJumpingState)
            {
                state |= (int)MegamanState.Jumping;

            } else if (actionState is MegamanFallingState)
            {
                state |= (int)MegamanState.Falling;

            } else // Must be MegamanRunningState
            {
                state |= (int)MegamanState.Running;
            }

            if (powerUpState is MegamanDeadState)
            {
                state |= (int)MegamanState.Dead;

            } else if (powerUpState is MegamanFalconState)
            {
                state |= (int)MegamanState.Falcon;

            } else if (powerUpState is MegamanLargeState)
            {
                state |= (int)MegamanState.Large;

            } else if (powerUpState is MegamanSmallState)
            {
                state |= (int)MegamanState.Small;

            } else // Must be MegamanZeroState
            {
                state |= (int)MegamanState.Zero;
            }

            return (MegamanState)state;
        }
    }
}
