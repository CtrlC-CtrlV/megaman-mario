using MegaManClone.Entities;
using MegaManClone.Sprites;
using MegaManClone.Stages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Commands
{
    class ResetCommand : ICommand
    {
        Stage stage;

        public ResetCommand(Stage stage)
        {
            this.stage = stage;
        }

        void ICommand.Execute()
        {
            stage.Reset();
        }
    }
}
