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
    class SmallJumping : Sprite
    {
        public SmallJumping(ContentManager content)
            : base(new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32), 175, new Vector2(), content.Load<Texture2D>("megaman/8bit_jumping"))
        {

        }
    }
}
