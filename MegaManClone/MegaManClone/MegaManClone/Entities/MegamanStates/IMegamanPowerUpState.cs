using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Entities.MegamanStates
{
    interface IMegamanPowerUpState
    {

        void FalconTransition();
        void LargeTransition();
        void TakeDamage();
        void Update(GameTime gameTime);
        void ZeroTransition();
        int GetMaxHealth();
        int GetArmor();
    }
}
