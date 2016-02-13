using MegaManClone.Stages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Commands
{
    class LeftMenuCommand : ICommand
    {
        Menu menu;

        public LeftMenuCommand(Menu menu)
        {
            this.menu = menu;
        }

        public void Execute()
        {
            menu.Left();
        }
    }
}
