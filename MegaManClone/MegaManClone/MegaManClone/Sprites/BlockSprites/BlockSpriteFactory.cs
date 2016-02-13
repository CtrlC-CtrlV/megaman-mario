using MegaManClone.Entities.BlockStates;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Sprites.BlockSprites
{
    class BlockSpriteFactory
    {
        #region Properties

        ContentManager content;

        #endregion

        #region Constructor

        public BlockSpriteFactory(ContentManager content)
        {
            this.content = content;
        }

        #endregion

        public Sprite GetSprite(BlockState state)
        {
            if (((int)state & 0xFF00) > 0) // Must be breaking
            {
                return new BrickBreaking(content);
            }

            switch (state)
            {
                case BlockState.Brick:
                    return new Brick(content);
                case BlockState.Floor:
                    return new Floor(content);
                case BlockState.Floor2:
                    return new Floor2(content);
                case BlockState.Hidden:
                case BlockState.Death:
                    return new Hidden(content);
                case BlockState.Pyramid:
                    return new Pyramid(content);
                case BlockState.Pyramid2:
                    return new Pyramid2(content);
                case BlockState.Question:
                    return new Question(content);
                case BlockState.Used:
                    return new Used(content);
            }
            
            //switch ((int)state & 0xFFFF)
            //{
            //    case 0x0401:
            //        return new BrickBreaking(content);
            //    case 0x0201:
            //        return new BrickColliding(content);
            //    case 0x0101:
            //        return new Brick(content);
            //    case 0x0008:
            //        return new Floor(content);
            //    case 0x0010:
            //        return new Pyramid(content);
            //    case 0x0202:
            //        return new QuestionColliding(content);
            //    case 0x0102:
            //        return new QuestionNormal(content);
            //    case 0x0204:
            //        return new UsedColliding(content);
            //    case 0x0104:
            //        return new UsedNormal(content);
            //    default:
            //        return new Pyramid(content); // Because why not?
            //}

            return new Pyramid(content);
        }
    }
}
