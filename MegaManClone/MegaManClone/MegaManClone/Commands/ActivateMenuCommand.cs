using MegaManClone.Stages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Commands
{
    class ActivateMenuCommand : ICommand
    {
        Menu menu;

        public ActivateMenuCommand(Menu menu)
        {
            this.menu = menu;
        }

        public void Execute()
        {
            menu.Activate();
        }
    }
}
