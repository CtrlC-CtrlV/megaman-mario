using MegaManClone.Sprites;
using MegaManClone.Stages;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Entities.BlockStates
{
    class BlockCollidingState : IBlockState
    {
        #region Properties

        Block block;
        int bumpSpeed = 100;
        int collisionDuration = 150;
        Vector2 initialPosition;
        Megaman megaman;
        int millisecondsElapsed = 0;
        bool newCollision = true;

        #endregion

        #region Constructor

        public BlockCollidingState(Block block, Megaman megaman)
        {
            this.block = block;
            this.megaman = megaman;
            initialPosition = new Vector2();
        }

        #endregion

        #region IBlockState Implementation

        void IBlockState.Collide(ICollidable otherObject)
        {
            if (CollisionHelper.GetCollisionSide(block, otherObject) == CollisionSide.Top)
            {
                otherObject.TakeDamage(otherObject, 0);
            }
        }

        Vector2 IBlockState.GetVelocity()
        {
            return new Vector2(0, (millisecondsElapsed <= collisionDuration / 2 ? -1 : 1) * bumpSpeed);
        }

        void IBlockState.Update(Sprite sprite, GameTime gameTime)
        {
            Vector2 position = sprite.Position;
            
            if (newCollision)
            {
                initialPosition.X = position.X;
                initialPosition.Y = position.Y;
                millisecondsElapsed = 0;
                newCollision = false;
            }
            
            millisecondsElapsed += gameTime.ElapsedGameTime.Milliseconds;

            // Simulate a bump from below
            if (millisecondsElapsed <= collisionDuration / 2)
            {
                position.Y -= bumpSpeed * ((float)millisecondsElapsed / 1000f);

            } else
            {
                position.Y += bumpSpeed * ((float)(millisecondsElapsed - collisionDuration / 2) / 1000f);
                position.Y = MathHelper.Clamp(position.Y, initialPosition.Y - 32, initialPosition.Y);
            }

            sprite.Position = position;
            
            if (millisecondsElapsed > collisionDuration)
            {
                sprite.Position = initialPosition;
                block.CurrentState = block.StateMachine.GetState(BlockState.Normal);

                if (block.CurrentType == BlockState.Brick || block.CurrentType == BlockState.Question)
                {
                    if (block.ItemIndex < block.Items.Count)
                    {
                        block.Items[block.ItemIndex++].Reveal(megaman);
                    }

                    if (block.ItemIndex >= block.Items.Count)
                    {
                        block.CurrentType = BlockState.Used;
                    }
                }
                block.StateChanged();
                newCollision = true;
            }
        }

        #endregion
    }
}
