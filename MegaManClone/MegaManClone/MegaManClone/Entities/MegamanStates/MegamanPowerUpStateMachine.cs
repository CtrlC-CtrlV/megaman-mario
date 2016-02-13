using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Entities.MegamanStates
{
    class MegamanPowerUpStateMachine
    {
        Megaman megaman;
        Dictionary<MegamanState, IMegamanPowerUpState> states;

        public MegamanPowerUpStateMachine(Megaman megaman)
        {
            this.megaman = megaman;
            states = new Dictionary<MegamanState, IMegamanPowerUpState>();
        }

        public IMegamanPowerUpState getState(MegamanState state)
        {
            MegamanState powerUpState = (MegamanState)((int)state & 0xFF00);

            switch (powerUpState)
            {
                case MegamanState.Small:
                    if (!states.ContainsKey(MegamanState.Small))
                    {
                        states.Add(MegamanState.Small, new MegamanSmallState(megaman));
                    }
                    return states[MegamanState.Small];
                case MegamanState.Large:
                    if (!states.ContainsKey(MegamanState.Large))
                    {
                        states.Add(MegamanState.Large, new MegamanLargeState(megaman));
                    }
                    return states[MegamanState.Large];
                case MegamanState.Zero:
                    if (!states.ContainsKey(MegamanState.Zero))
                    {
                        states.Add(MegamanState.Zero, new MegamanZeroState(megaman));
                    }
                    return states[MegamanState.Zero];
                case MegamanState.Falcon:
                    if (!states.ContainsKey(MegamanState.Falcon))
                    {
                        states.Add(MegamanState.Falcon, new MegamanFalconState(megaman));
                    }
                    return states[MegamanState.Falcon];
                case MegamanState.Dead:
                default:
                    // TODO: This isn't very safe. What's a better way? Exception?
                    if (!states.ContainsKey(MegamanState.Dead))
                    {
                        states.Add(MegamanState.Dead, new MegamanDeadState());
                    }
                    return states[MegamanState.Dead];
            }
        }
    }
}
