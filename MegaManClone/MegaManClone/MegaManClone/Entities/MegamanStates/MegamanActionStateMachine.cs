using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Entities.MegamanStates
{
    class MegamanActionStateMachine
    {
        Megaman megaman;
        Dictionary<ActionState, MegamanActionState> states = new Dictionary<ActionState, MegamanActionState>();

        public MegamanActionStateMachine(Megaman megaman)
        {
            this.megaman = megaman;
        }

        public MegamanActionState GetActionState(ActionState state)
        {
            switch (state)
            {
                case ActionState.Crouching:
                    if (!states.ContainsKey(state))
                    {
                        states.Add(state, new MegamanCrouchingState(megaman));
                    }
                    break;
                case ActionState.Falling:
                    if (!states.ContainsKey(state))
                    {
                        states.Add(state, new MegamanFallingState(megaman));
                    }
                    break;
                case ActionState.Idle:
                    if (!states.ContainsKey(state))
                    {
                        states.Add(state, new MegamanIdleState(megaman));
                    }
                    break;
                case ActionState.Jumping:
                    if (!states.ContainsKey(state))
                    {
                        states.Add(state, new MegamanJumpingState(megaman));
                    }
                    break;
                case ActionState.Running:
                    if (!states.ContainsKey(state))
                    {
                        states.Add(state, new MegamanRunningState(megaman));
                    }
                    break;
                default:
                    return new MegamanActionState(megaman);
            }

            return states[state];
        }
    }
}
