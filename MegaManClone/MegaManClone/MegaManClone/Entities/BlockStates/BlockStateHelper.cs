using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Entities.BlockStates
{
    class BlockStateHelper
    {
        public static BlockState GetState(BlockState type, IBlockState state)
        {
            if (state is BlockBreakingState)
            {
                type = (BlockState)((int)type | (int)BlockState.Breaking);
            }

            return type;
        }

        public static BlockState GetState(string state)
        {
            if (state == "question")
            {
                return BlockState.Question;

            } else if (state == "brick")
            {
                return BlockState.Brick;

            } else if (state == "used")
            {
                return BlockState.Used;

            } else if (state == "pyramid")
            {
                return BlockState.Pyramid;

            } else if (state == "pyramid2")
            {
                return BlockState.Pyramid2;

            } else if (state == "hidden")
            {
                return BlockState.Hidden;

            } else if (state == "floor")
            {
                return BlockState.Floor;

            } else if (state == "floor2")
            {
                return BlockState.Floor2;
            }
            else // state == "death"
            {
                return BlockState.Death;
            }
        }
    }
}
