using MegaManClone.Sprites;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Entities.MegamanStates
{
    interface IMegamanActionState
    {
        void BlockMovement(ICollidable otherObject);
        void DownCommand();
        Vector2 GetVelocity();
        void LeftCommand();
        void RightCommand();
        void UpCommand();
        void UpdatePosition(Sprite sprite, GameTime gameTime);
    }
}
