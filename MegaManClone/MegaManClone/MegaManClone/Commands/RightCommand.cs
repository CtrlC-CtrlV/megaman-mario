using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MegaManClone.Entities;

namespace MegaManClone.Commands
{
    class RightCommand : ICommand
    {
        Megaman megaman;

        public RightCommand(Megaman megaman)
        {
            this.megaman = megaman;
        }

        void ICommand.Execute()
        {
            megaman.RightCommand();
        }
    }
}
