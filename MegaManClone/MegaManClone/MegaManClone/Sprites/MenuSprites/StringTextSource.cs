using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Sprites.MenuSprites
{
    class StringTextSource : ITextSource
    {
        #region Fields

        String text;

        #endregion

        #region Constructor

        public StringTextSource(String text)
        {
            this.text = text;
        }

        #endregion

        #region ITextSource Implementation

        public String GetText()
        {
            return text;
        }

        #endregion
    }
}
