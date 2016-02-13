using MegaManClone.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Sprites.ItemSprites
{
    class ItemSpriteFactory
    {
        #region Properties

        ContentManager content;

        #endregion

        #region Constructor

        public ItemSpriteFactory(ContentManager content)
        {
            this.content = content;
        }

        #endregion

        public Sprite GetItem(String itemType, Vector2 position)
        {
            if (itemType == "etank")
            {
                return new ETank(content, position);

            } else if (itemType == "falconpowerup")
            {
                return new FalconPowerup(content, position);

            } else if (itemType == "lifetank")
            {
                return new LifeTank(content, position);

            } else if (itemType == "megamanhelmet")
            {
                return new MegamanHelmet(content, position);

            } else if (itemType == "flagpole")
            {
                return new Flagpole(content, position);

            }
             else // itemType == "zerohelmet"
            {
                return new ZeroHelmet(content, position);
            }
        }

        public HiddenItem GetHiddenItem(String itemType, Block block)
        {
            if (itemType == "etank")
            {
                return new HiddenETank(content, block);

            } else if (itemType == "falconpowerup")
            {
                return new HiddenFalconPowerup(content, block);

            } else if (itemType == "lifetank")
            {
                return new HiddenLifeTank(content, block);

            } else if (itemType == "megamanhelmet")
            {
                return new HiddenMegamanHelmet(content, block);

            } else // itemType == "zerohelmet"
            {
                return new HiddenZeroHelmet(content, block);
            }
        }
    }
}
