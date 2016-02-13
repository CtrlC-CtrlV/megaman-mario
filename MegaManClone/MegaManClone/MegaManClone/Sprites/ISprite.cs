using MegaManClone.Stages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Sprites
{
    interface ISprite
    {
        void Draw(SpriteBatch spriteBatch, Camera camera);
        void Reset();
        void Update(GameTime gameTime);
    }
}
