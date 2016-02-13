using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MegaManClone.Entities;

namespace MegaManClone.Commands
{
    class DownCommand : ICommand
    {
        Megaman megaman;

        public DownCommand(Megaman megaman)
        {
            this.megaman = megaman;
        }

        void ICommand.Execute()
        {
            megaman.DownCommand();
        }
    }
}
