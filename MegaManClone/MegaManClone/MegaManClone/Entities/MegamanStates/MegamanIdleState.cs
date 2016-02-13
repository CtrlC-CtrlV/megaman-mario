using MegaManClone.Sprites;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Entities.MegamanStates
{
    class MegamanIdleState : MegamanActionState
    {
        #region Constructor

        public MegamanIdleState(Megaman megaman)
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

            megaman.CurrentSprite.Acceleration = Vector2.Zero;
        }

        public override void LeftCommand()
        {
            if (megaman.Direction == MegamanState.Left)
            {
                nextActionState = ActionState.Running;

            } else
            {
                megaman.Direction = MegamanState.Left;
                megaman.StateChanged();
            }
        }

        public override void RightCommand()
        {
            if (megaman.Direction == MegamanState.Right)
            {
                nextActionState = ActionState.Running;

            } else
            {
                megaman.Direction = MegamanState.Right;
                megaman.StateChanged();
            }
        }

        public override void UpCommand()
        {
            nextActionState = ActionState.Jumping;
        }

        #endregion
    }
}
