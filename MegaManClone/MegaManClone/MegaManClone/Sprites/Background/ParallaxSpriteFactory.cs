using MegaManClone.Stages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Sprites.Background
{
    class ParallaxSpriteFactory
    {
        #region Fields

        Camera camera;
        ContentManager content;

        #endregion

        public ParallaxSpriteFactory(Camera camera, ContentManager content)
        {
            this.camera = camera;
            this.content = content;
        }

        public ParallaxSprite GetParallaxSprite(String type, float layerDepth, Vector2 position)
        {
            if (type == "building1")
            {
                return new Building1(camera, content, layerDepth, position);

            } else if (type == "building2")
            {
                return new Building2(camera, content, layerDepth, position);

            } else if (type == "building3")
            {
                return new Building3(camera, content, layerDepth, position);

            } else if (type == "building4")
            {
                return new Building4(camera, content, layerDepth, position);

            } else //type == city
            {
                return new City(camera, content, layerDepth, position);
            }
        }
    }
}
