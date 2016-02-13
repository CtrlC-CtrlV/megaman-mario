﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MegaManClone.Entities;

namespace MegaManClone.Commands
{
    class LeftCommand : ICommand
    {
        Megaman megaman;

        public LeftCommand(Megaman megaman)
        {
            this.megaman = megaman;
        }

        void ICommand.Execute()
        {
            megaman.LeftCommand();
        }
    }
}
