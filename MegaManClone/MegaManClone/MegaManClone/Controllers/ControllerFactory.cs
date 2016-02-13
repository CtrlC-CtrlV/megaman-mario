using MegaManClone.Commands;
using MegaManClone.Entities;
using MegaManClone.Stages;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Controllers
{
    class ControllerFactory
    {
        #region Fields

        Dictionary<Tuple<Keys, Buttons>, ICommand> bindings;
        Scene scene;
        MegamanGame game;
        Megaman megaman;

        #endregion

        #region Constructor

        public ControllerFactory(Scene scene, MegamanGame game, Megaman megaman)
        {
            this.scene = scene;
            this.game = game;
            this.megaman = megaman;
            bindings = new Dictionary<Tuple<Keys, Buttons>, ICommand>();

            Reset();
        }

        #endregion

        #region Methods

        public GamePadController GetGamePadController()
        {
            GamePadController controller = new GamePadController();

            foreach (KeyValuePair<Tuple<Keys, Buttons>, ICommand> binding in bindings)
            {
                controller.AddCommand(binding.Key.Item2, binding.Value);
            }

            return controller;
        }

        public KeyboardController GetKeyboardController()
        {
            KeyboardController controller = new KeyboardController();

            foreach (KeyValuePair<Tuple<Keys, Buttons>, ICommand> binding in bindings)
            {
                controller.AddCommand(binding.Key.Item1, binding.Value);
            }

            return controller;
        }

        public void Reset()
        {
            bindings.Clear();

            if (scene is Stage)
            {
                Stage stage = scene as Stage;
                bindings.Add(new Tuple<Keys, Buttons>(Keys.W, Buttons.A), new UpCommand(megaman));
                bindings.Add(new Tuple<Keys, Buttons>(Keys.Up, Buttons.A), new UpCommand(megaman));
                bindings.Add(new Tuple<Keys, Buttons>(Keys.A, Buttons.LeftThumbstickLeft), new LeftCommand(megaman));
                bindings.Add(new Tuple<Keys, Buttons>(Keys.Left, Buttons.LeftThumbstickLeft), new LeftCommand(megaman));
                bindings.Add(new Tuple<Keys, Buttons>(Keys.S, Buttons.LeftThumbstickDown), new DownCommand(megaman));
                bindings.Add(new Tuple<Keys, Buttons>(Keys.Down, Buttons.LeftThumbstickDown), new DownCommand(megaman));
                bindings.Add(new Tuple<Keys, Buttons>(Keys.D, Buttons.LeftThumbstickRight), new RightCommand(megaman));
                bindings.Add(new Tuple<Keys, Buttons>(Keys.Right, Buttons.LeftThumbstickRight), new RightCommand(megaman));
                bindings.Add(new Tuple<Keys, Buttons>(Keys.M, Buttons.Back), new MuteCommand(stage));
                bindings.Add(new Tuple<Keys, Buttons>(Keys.Escape, Buttons.Start), new PauseCommand(stage));
                bindings.Add(new Tuple<Keys, Buttons>(Keys.R, Buttons.LeftShoulder), new ResetCommand(stage));
                bindings.Add(new Tuple<Keys, Buttons>(Keys.Q, Buttons.BigButton), new QuitCommand(game));
                bindings.Add(new Tuple<Keys, Buttons>(Keys.Space, Buttons.B), new BusterCommand(megaman));

            } else if (scene is Menu)
            {
                Menu menu = scene as Menu;
                bindings.Add(new Tuple<Keys, Buttons>(Keys.Enter, Buttons.A), new ActivateMenuCommand(menu));
                bindings.Add(new Tuple<Keys, Buttons>(Keys.Escape, Buttons.B), new BackMenuCommand(menu));
                bindings.Add(new Tuple<Keys, Buttons>(Keys.Down, Buttons.LeftThumbstickDown), new DownMenuCommand(menu));
                bindings.Add(new Tuple<Keys, Buttons>(Keys.Left, Buttons.LeftThumbstickLeft), new LeftMenuCommand(menu));
                bindings.Add(new Tuple<Keys, Buttons>(Keys.Right, Buttons.LeftThumbstickRight), new RightMenuCommand(menu));
                bindings.Add(new Tuple<Keys, Buttons>(Keys.Up, Buttons.LeftThumbstickUp), new UpMenuCommand(menu));
                bindings.Add(new Tuple<Keys, Buttons>(Keys.Q, Buttons.BigButton), new QuitCommand(game));
            }
        }

        public void SetBinding(Keys key, Buttons button, ICommand command)
        {
            bindings[new Tuple<Keys, Buttons>(key, button)] = command;
        }

        #endregion
    }
}
