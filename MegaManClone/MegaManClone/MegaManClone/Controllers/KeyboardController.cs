using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MegaManClone.Commands;


namespace MegaManClone.Controllers
{
    class KeyboardController : IController
    {
        Dictionary<Keys, ICommand> commands;
        
        public KeyboardController()
        {
            commands = new Dictionary<Keys,ICommand>();
        }

        public void AddCommand(Keys key, ICommand command)
        {
            commands[key] = command;
        }
        
        void IController.Update()
        {
            KeyboardState newState = Keyboard.GetState();

            foreach (Keys key in newState.GetPressedKeys())
            {
                if (commands.ContainsKey(key))
                {
                    commands[key].Execute();
                }
            }
        }
    }
}
