using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Sprites.MegamanSprites
{
    class LargeIdle : Sprite
    {
        public LargeIdle(ContentManager content)
            : base(new Rectangle(0, 0, 32, 64), new Rectangle(0, 0, 32, 64), 250, new Vector2(), content.Load<Texture2D>("megaman/normal_idle"))
        {

        }
    }
}
