using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Sprites.MegamanSprites
{
    class ZeroRunning : Sprite
    {
        public ZeroRunning(ContentManager content)
            : base(new Rectangle(0, 0, 32, 64), new Rectangle(0, 0, 32, 64), 100, new Vector2(), content.Load<Texture2D>("megaman/zero_running"))
        {

        }
    }
}
