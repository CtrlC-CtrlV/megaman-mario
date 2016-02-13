using MegaManClone.Stages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Commands
{
    class MuteCommand : ICommand
    {
        Stage stage;

        public MuteCommand(Stage stage)
        {
            this.stage = stage;
        }

        public void Execute()
        {
            stage.Mute();
        }
    }
}
