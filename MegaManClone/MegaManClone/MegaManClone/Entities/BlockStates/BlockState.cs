using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Entities.BlockStates
{
    [Flags]
    public enum BlockState
    {
        // Block type
        Brick = 0x01,
        Death = 0x40,
        Floor = 0x08,
        Floor2 = 0x12,
        Hidden = 0x20,
        Pyramid = 0x10,
        Pyramid2 = 0x14,
        Question = 0x02,
        Used = 0x04,

        // Block action
        Normal = 0x0100,
        Colliding = 0x0200,
        Breaking = 0x0400
    }
}
