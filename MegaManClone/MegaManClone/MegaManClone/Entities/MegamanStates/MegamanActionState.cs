using MegaManClone.Stages;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Entities.MegamanStates
{
    class MegamanActionState
    {
        #region Properties

        protected readonly int collisionMargin = 6;
        protected ActionState defaultNextActionState = ActionState.Idle;
        protected Megaman megaman;
        protected MegamanActionState previousActionState;
        protected ActionState nextActionState = ActionState.Idle;

        #endregion

        #region Constructor

        public MegamanActionState(Megaman megaman)
        {
            this.megaman = megaman;
        }

        public MegamanActionState(Megaman megaman, ActionState defaultNextActionState)
            : this (megaman)
        {
            this.defaultNextActionState = defaultNextActionState;
            nextActionState = defaultNextActionState;
        }

        #endregion

        #region Methods

        public virtual void BlockMovement(ICollidable otherObject)
        {
            Rectangle megamanAABB = megaman.AABB;
            Rectangle otherAABB = otherObject.AABB;

            Vector2 position = megaman.CurrentSprite.Position;
            Vector2 velocity = megaman.CurrentSprite.Velocity;
            Vector2 acceleration = megaman.CurrentSprite.Acceleration;

            switch (CollisionHelper.GetCollisionSide(megaman, otherObject))
            {
                case CollisionSide.Bottom:
                    position.Y = otherAABB.Top - megamanAABB.Height;
                    velocity.Y = 0;
                    acceleration.Y = 0;
                    break;
                case CollisionSide.Left:
                    position.X = otherAABB.Right;
                    velocity.X = 0;
                    acceleration.X = 0;
                    break;
                case CollisionSide.Right:
                    position.X = otherAABB.Left - megamanAABB.Width;
                    velocity.X = 0;
                    acceleration.X = 0;
                    break;
                case CollisionSide.Top:
                    position.Y = otherAABB.Bottom;
                    velocity.Y = 0;
                    acceleration.Y = 0;
                    break;
            }

            megaman.CurrentSprite.Position = position;
            megaman.CurrentSprite.Velocity = velocity;
            megaman.CurrentSprite.Acceleration = acceleration;
        }

        public virtual void DownCommand()
        {

        }

        public virtual void Enter()
        {
            if (this != megaman.CurrentActionState)
            {
                megaman.PreviousActionState = megaman.CurrentActionState;
                megaman.CurrentActionState = this;
                megaman.StateChanged();
            }
        }

        public virtual void LeftCommand()
        {

        }

        public virtual void RightCommand()
        {

        }

        public virtual void TriggerFall()
        {
            megaman.ActionStateMachine.GetActionState(ActionState.Falling).Enter();
        }

        public virtual void UpCommand()
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            megaman.ActionStateMachine.GetActionState(nextActionState).Enter();
            nextActionState = defaultNextActionState;
        }

        #endregion
    }
}
