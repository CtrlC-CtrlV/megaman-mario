﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Sprites.BlockSprites
{
    class Pyramid2 : Sprite
    {
        public Pyramid2(ContentManager content)
            : base(new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32), 0, Vector2.Zero, content.Load<Texture2D>("blocks/1-2Pyramid"))
        {

        }
    }
}