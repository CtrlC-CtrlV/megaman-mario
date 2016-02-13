using MegaManClone.Stages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Sprites.Background
{
    class Building3 : ParallaxSprite
    {
        public Building3(Camera camera, ContentManager content, float layerDepth, Vector2 position)
            : base(camera, new Rectangle(0, 0, 48, 216), layerDepth, 0, position, content.Load<Texture2D>("Parallax/thirdClosestSkyscraper"))
        {

        }
    }
}
