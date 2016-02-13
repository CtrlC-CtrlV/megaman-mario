using MegaManClone.Sprites;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Entities.MegamanStates
{
    class MegamanRunningState : MegamanActionState
    {
        #region Properties

        int runAcceleration = 800;

        #endregion

        #region Constructor

        public MegamanRunningState(Megaman megaman)
            : base (megaman)
        {
            
        }

        #endregion

        #region MegamanActionState Overrides

        public override void DownCommand()
        {
            nextActionState = ActionState.Crouching;
        }

        public override void Enter()
        {
            base.Enter();

            Vector2 acceleration = megaman.CurrentSprite.Acceleration;
            acceleration.X = (megaman.Direction == MegamanState.Left ? -1 : 1) * runAcceleration;
            megaman.CurrentSprite.Acceleration = acceleration;
        }

        public override void LeftCommand()
        {
            if (megaman.Direction == MegamanState.Left
                && nextActionState != ActionState.Jumping
                && nextActionState != ActionState.Crouching)
            {
                nextActionState = ActionState.Running;
            }
        }

        public override void RightCommand()
        {
            if (megaman.Direction == MegamanState.Right
                && nextActionState != ActionState.Jumping
                && nextActionState != ActionState.Crouching)
            {
                nextActionState = ActionState.Running;
            }
        }

        public override void UpCommand()
        {
            nextActionState = ActionState.Jumping;
        }

        #endregion
    }
}
