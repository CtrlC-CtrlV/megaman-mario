using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MegaManClone.Entities.MegamanStates;

namespace MegaManClone.Sprites.MegamanSprites
{
    class MegamanSpriteFactory
    {
        #region Properties

        ContentManager content;

        #endregion

        #region Constructor

        public MegamanSpriteFactory(ContentManager content)
        {
            this.content = content;
        }

        #endregion

        public Sprite GetSprite(MegamanState state)
        {
            //TODO: Implement dictionary to keep these things in memory
            
            switch ((int)state & 0xFFFF)
            {
                case 0x0101:
                    return new SmallIdle(content);
                case 0x0102:
                    return new SmallRunning(content);
                case 0x0104:
                    return new SmallJumping(content);
                //no crouching for small MM, should we set it to falling sprite?
                case 0x0108:
                    return new SmallFalling(content);
                case 0x0110:
                    return new SmallFalling(content);
                case 0x0201:
                    return new LargeIdle(content);
                case 0x0202:
                    return new LargeRunning(content);
                case 0x0204:
                    return new LargeJumping(content);
                case 0x0208:
                    return new LargeCrouching(content);
                case 0x0210:
                    return new LargeFalling(content);
                case 0x0401:
                    return new ZeroIdle(content);
                case 0x0402:
                    return new ZeroRunning(content);
                case 0x0404:
                    return new ZeroJumping(content);
                case 0x0408:
                    return new ZeroCrouching(content);
                case 0x0410:
                    return new ZeroFalling(content);
                case 0x0801:
                    return new FalconIdle(content);
                case 0x0802:
                    return new FalconRunning(content);
                case 0x0804:
                    return new FalconJumping(content);
                case 0x0808:
                    return new FalconCrouching(content);
                case 0x0810:
                    return new FalconFalling(content);
                case 0x1001:
                case 0x1002:
                case 0x1004:
                case 0x1008:
                case 0x1010:
                    return new Dead(content);
                case 0x81:
                case 0x82:
                case 0x84:
                case 0x88:
                default:
                    return new Dead(content);
            }
            
        }
    }
}
