using MegaManClone.Sprites;
using MegaManClone.Stages;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Entities.MegamanStates
{
    class MegamanFallingState : MegamanActionState
    {
        #region Properties

        readonly int fallAcceleration = 1200;
        bool moving = false;
        readonly int runAcceleration = 800;

        #endregion

        #region Constructor

        public MegamanFallingState(Megaman megaman)
            : base (megaman, ActionState.Falling)
        {
            
        }

        #endregion

        #region MegamanActionState Overrides

        public override void BlockMovement(ICollidable otherObject)
        {
            CollisionSide collisionSide = CollisionHelper.GetCollisionSide(megaman, otherObject);

            if (collisionSide == CollisionSide.Bottom)
            {
                megaman.PreviousActionState.Enter();
            }
            
            base.BlockMovement(otherObject);
        }

        public override void Enter()
        {
            if (this != megaman.CurrentActionState)
            {
                if (!(megaman.CurrentActionState is MegamanJumpingState))
                {
                    megaman.PreviousActionState = megaman.CurrentActionState;
                }
                megaman.CurrentActionState = this;
                megaman.StateChanged();
            }

            Vector2 acceleration = megaman.CurrentSprite.Acceleration;
            acceleration.Y = fallAcceleration;
            megaman.CurrentSprite.Acceleration = acceleration;
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

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (moving)
            {
                Vector2 acceleration = megaman.CurrentSprite.Acceleration;
                acceleration.X = (megaman.Direction == MegamanState.Left ? -1 : 1) * runAcceleration;
                megaman.CurrentSprite.Acceleration = acceleration;
                moving = false;
            }
        }

        #endregion
    }
}
