using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Entities.BlockStates
{
    class BlockStateMachine
    {
        #region Properties

        Block block;
        Megaman megaman;

        #endregion

        #region Constructor

        public BlockStateMachine(Block block, Megaman megaman)
        {
            this.block = block;
            this.megaman = megaman;
        }

        #endregion

        public IBlockState GetState(BlockState state)
        {
            switch (state)
            {
                case BlockState.Breaking:
                    return new BlockBreakingState();
                case BlockState.Colliding:
                    return new BlockCollidingState(block, megaman);
                case BlockState.Normal:
                default:
                    return new BlockNormalState(block, megaman);
            }
        }
    }
}
