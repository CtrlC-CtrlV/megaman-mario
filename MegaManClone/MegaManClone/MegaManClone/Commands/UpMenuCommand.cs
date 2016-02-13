using MegaManClone.Stages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Commands
{
    class UpMenuCommand : ICommand
    {
        Menu menu;

        public UpMenuCommand(Menu menu)
        {
            this.menu = menu;
        }

        public void Execute()
        {
            menu.Up();
        }
    }
}
