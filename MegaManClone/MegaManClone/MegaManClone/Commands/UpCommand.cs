using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MegaManClone.Entities;

namespace MegaManClone.Commands
{
    class UpCommand : ICommand
    {
        Megaman megaman;

        public UpCommand(Megaman megaman)
        {
            this.megaman = megaman;
        }

        void ICommand.Execute()
        {
            megaman.UpCommand();
        }
    }
}
