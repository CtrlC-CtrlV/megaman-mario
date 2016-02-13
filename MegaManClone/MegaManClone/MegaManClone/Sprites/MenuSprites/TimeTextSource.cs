using MegaManClone.Stages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Sprites.MenuSprites
{
    class TimeTextSource : ITextSource
    {
        #region Fields

        Stage stage;

        #endregion

        #region Constructor

        public TimeTextSource(Stage stage)
        {
            this.stage = stage;
        }

        #endregion

        #region ITextSource Implementation

        public String GetText()
        {
            return String.Format("Time Left: {0}", stage.Timer.ToString().PadLeft(3, '0'));
        }

        #endregion
    }
}
