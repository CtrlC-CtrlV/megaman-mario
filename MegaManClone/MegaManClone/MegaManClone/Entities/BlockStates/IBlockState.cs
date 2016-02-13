using MegaManClone.Sprites;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Entities.BlockStates
{
    interface IBlockState
    {
        void Collide(ICollidable otherObject);
        Vector2 GetVelocity();
        void Update(Sprite sprite, GameTime gameTime);
    }
}
