using MegaManClone.Sprites;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Entities.MegamanStates
{
    class MegamanCrouchingState : MegamanActionState
    {
        #region Constructor

        public MegamanCrouchingState(Megaman megaman)
            : base (megaman)
        {

        }

        #endregion

        #region MegamanActionState Overrides

        public override void BlockMovement(ICollidable otherObject)
        {
            megaman.ActionStateMachine.GetActionState(ActionState.Idle).Enter();
            Vector2 position = megaman.CurrentSprite.Position;
            position.Y = otherObject.AABB.Top - ((ICollidable)megaman).AABB.Height;
            megaman.CurrentSprite.Position = position;
        }

        public override void DownCommand()
        {
            nextActionState = ActionState.Crouching;
        }

        public override void Enter()
        {
            base.Enter();

            megaman.CurrentSprite.Acceleration = Vector2.Zero;
            megaman.CurrentSprite.Velocity = Vector2.Zero;
        }

        #endregion
    }
}
