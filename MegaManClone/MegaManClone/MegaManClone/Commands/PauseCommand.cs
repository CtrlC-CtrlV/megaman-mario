using MegaManClone.Stages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Commands
{
    class PauseCommand : ICommand
    {
        #region Fields

        Stage stage;

        #endregion

        #region Constructor

        public PauseCommand(Stage stage)
        {
            this.stage = stage;
        }

        #endregion

        #region ICommand Implementation

        public void Execute()
        {
            stage.Pause();
        }

        #endregion
    }
}
