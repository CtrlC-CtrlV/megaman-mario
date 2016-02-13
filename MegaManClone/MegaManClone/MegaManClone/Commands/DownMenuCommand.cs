using MegaManClone.Stages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Commands
{
    class DownMenuCommand : ICommand
    {
        Menu menu;

        public DownMenuCommand(Menu menu)
        {
            this.menu = menu;
        }

        public void Execute()
        {
            menu.Down();
        }
    }
}
