using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Entities.MegamanStates
{
    [FlagsAttribute]
    public enum MegamanState
    {
        None = 0,

        // Action States
        Idle = 0x0001,
        Running = 0x0002,
        Jumping = 0x0004,
        Crouching = 0x0008,
        Falling = 0x0010,

        // PowerUp States
        Small = 0x0100,
        Large = 0x0200,
        Zero = 0x0400,
        Falcon = 0x0800,
        Dead = 0x1000,
        Normal = 0x2000,

        // Direction States
        Left = 0x010000,
        Right = 0x020000,
    }
}
