using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MegaManClone.Commands;

namespace MegaManClone.Controllers
{
    class GamePadController : IController
    {
        Dictionary<Buttons, ICommand> commands;

        public GamePadController()
        {
            commands = new Dictionary<Buttons, ICommand>();
        }

        public void AddCommand(Buttons button, ICommand command)
        {
            commands[button] = command;
        }

        void IController.Update()
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

            if (gamePadState.IsConnected)
            {
                foreach (Buttons button in commands.Keys)
                {
                    if (gamePadState.IsButtonDown(button))
                    {
                        commands[button].Execute();
                    }
                }
            }
        }
    }
}
