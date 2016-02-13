using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MegaManClone.Stages;
using MegaManClone.Entities;

namespace MegaManClone.Commands
{
    class BusterCommand : ICommand
    {
        Megaman megaman;

        public BusterCommand(Megaman megaman)
        {
            this.megaman = megaman;
        }

        void ICommand.Execute()
        {
            megaman.ChargeBuster();
        }
    }
}
