using MegaManClone.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Sprites.MenuSprites
{
    class LivesTextSource : ITextSource
    {
        #region Fields

        Megaman megaman;

        #endregion

        #region Constructor

        public LivesTextSource(Megaman megaman)
        {
            this.megaman = megaman;
        }

        #endregion

        #region ITextSource Implementation

        public String GetText()
        {
            return String.Format("Lives: {0}", megaman.Lives);
        }

        #endregion
    }
}
