using MegaManClone.Stages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Commands
{
    class RightMenuCommand : ICommand
    {
        Menu menu;

        public RightMenuCommand(Menu menu)
        {
            this.menu = menu;
        }

        public void Execute()
        {
            menu.Right();
        }
    }
}
