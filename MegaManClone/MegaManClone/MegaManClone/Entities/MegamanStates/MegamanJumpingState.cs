using MegaManClone.Sprites;
using MegaManClone.Stages;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Entities.MegamanStates
{
    class MegamanJumpingState : MegamanActionState
    {
        #region Properties

        readonly int jumpAcceleration = 1700;
        readonly int jumpVelocity = -400;
        readonly int maxJumpTime = 250;
        int millisecondsElapsed = 0;
        bool moving = false;
        readonly int runAcceleration = 800;

        #endregion

        #region Constructor

        public MegamanJumpingState(Megaman megaman)
            : base (megaman)
        {

        }

        #endregion

        #region MegamanActionState Overrides

        public override void BlockMovement(ICollidable otherObject)
        {
            if (CollisionHelper.GetCollisionSide(megaman, otherObject) == CollisionSide.Top)
            {
                megaman.ActionStateMachine.GetActionState(ActionState.Falling).Enter();
            }

            base.BlockMovement(otherObject);
        }

        public override void Enter()
        {
            base.Enter();
            setMovement();
        }

        public override void LeftCommand()
        {
            megaman.Direction = MegamanState.Left;
            moving = true;
        }

        public override void RightCommand()
        {
            megaman.Direction = MegamanState.Right;
            moving = true;
        }

        public override void TriggerFall()
        {
            
        }

        public override void UpCommand()
        {
            if (millisecondsElapsed < maxJumpTime)
            {
                setMovement();
                nextActionState = ActionState.Jumping;
            }
        }

        public override void Update(GameTime gameTime)
        {

            millisecondsElapsed += gameTime.ElapsedGameTime.Milliseconds;

            if (megaman.CurrentSprite.Velocity.Y >= 0)
            {
                megaman.ActionStateMachine.GetActionState(ActionState.Falling).Enter();
                millisecondsElapsed = 0;
                nextActionState = ActionState.Idle;
            }

            if (moving)
            {
                Vector2 acceleration = megaman.CurrentSprite.Acceleration;
                acceleration.X = (megaman.Direction == MegamanState.Left ? -1 : 1) * runAcceleration;
                megaman.CurrentSprite.Acceleration = acceleration;
                moving = false;
            }
        }

        #endregion

        #region Miscellaneous Methods

        void setMovement()
        {
            Vector2 velocity = megaman.CurrentSprite.Velocity;
            Vector2 acceleration = megaman.CurrentSprite.Acceleration;

            velocity.Y = jumpVelocity;
            acceleration.Y = jumpAcceleration;

            megaman.CurrentSprite.Velocity = velocity;
            megaman.CurrentSprite.Acceleration = acceleration;
        }

        #endregion
    }
}
