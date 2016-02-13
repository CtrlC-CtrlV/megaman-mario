using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Commands
{
    class QuitCommand : ICommand
    {
        MegamanGame game;
        
        public QuitCommand(MegamanGame game)
        {
            this.game = game;
        }
        
        void ICommand.Execute()
        {
            game.Exit();
        }
    }
}
