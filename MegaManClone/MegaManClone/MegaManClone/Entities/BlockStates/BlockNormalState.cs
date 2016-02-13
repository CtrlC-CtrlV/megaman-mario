using MegaManClone.Entities.MegamanStates;
using MegaManClone.Sprites;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Entities.BlockStates
{
    class BlockNormalState : IBlockState
    {
        #region Properties

        Block block;
        Megaman megaman;

        #endregion

        #region Constructor

        public BlockNormalState(Block block, Megaman megaman)
        {
            this.block = block;
            this.megaman = megaman;
        }

        #endregion

        #region IBlockState Implementation

        void IBlockState.Collide(ICollidable otherObject)
        {
            otherObject.BlockMovement(block);
            if (otherObject.AABB.Top >= ((ICollidable)block).AABB.Bottom)
            {
                // if (brick && MM.isSuper) then break;
                if (block.CurrentType == BlockState.Brick && !(megaman.CurrentPowerUpState is MegamanSmallState))
                {
                    block.CurrentState = block.StateMachine.GetState(BlockState.Breaking);

                } else if (block.CurrentType == BlockState.Brick || block.CurrentType == BlockState.Question || block.CurrentType == BlockState.Used)
                {
                    block.CurrentState = block.StateMachine.GetState(BlockState.Colliding);
                }
                block.StateChanged();
            }
            
        }

        Vector2 IBlockState.GetVelocity()
        {
            return Vector2.Zero;
        }

        void IBlockState.Update(Sprite sprite, GameTime gameTime)
        {
            // empty method
        }

        #endregion
    }
}
