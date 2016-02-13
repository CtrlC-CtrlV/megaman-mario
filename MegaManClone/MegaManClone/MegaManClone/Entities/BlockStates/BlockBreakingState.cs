using MegaManClone.Sprites;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Entities.BlockStates
{
    class BlockBreakingState : IBlockState
    {
        #region Properties

        int fallSpeed = 250;
        int maxFallTime = 3000;
        int millisecondsElapsed = 0;

        #endregion

        #region Constructor

        public BlockBreakingState()
        {

        }

        #endregion

        #region IBlockState Implementation

        void IBlockState.Collide(ICollidable otherObject)
        {

        }

        Vector2 IBlockState.GetVelocity()
        {
            return new Vector2(0, fallSpeed);
        }

        void IBlockState.Update(Sprite sprite, GameTime gameTime)
        {
            if (millisecondsElapsed < maxFallTime)
            {
                millisecondsElapsed += gameTime.ElapsedGameTime.Milliseconds;
                Vector2 position = sprite.Position;
                position.Y += (float)fallSpeed * ((float)gameTime.ElapsedGameTime.Milliseconds / 1000f);
                sprite.Position = position;
            }
        }

        #endregion
    }
}
