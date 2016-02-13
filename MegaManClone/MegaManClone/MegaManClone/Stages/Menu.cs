using MegaManClone.Sprites.MenuSprites;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Stages
{
    class Menu : Scene
    {
        #region Fields

        Boolean canGoBack;
        Dictionary<String, MenuChoice> choices = new Dictionary<String, MenuChoice>();
        String currentChoice;
        String initialChoice;
        static bool inputRecently = false;
        readonly int inputTimeOut = 200;
        int inputTimer = 0;

        #endregion

        #region Constructor

        public Menu(String background, Boolean canGoBack, MegamanGame game, String initialChoice)
            : base (background, game, new Vector2(game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height))
        {
            this.canGoBack = canGoBack;
            currentChoice = initialChoice;
            this.initialChoice = initialChoice;
        }

        #endregion

        #region Methods

        public void Activate()
        {
            if (!inputRecently)
            {
                choices[currentChoice].Activate();
                inputRecently = true;
            }
        }

        public void AddChoice(MenuChoice choice)
        {
            choices[choice.Id] = choice;
            choice.Selected = choice.Id == initialChoice;
        }

        public void Back()
        {
            if (!inputRecently)
            {
                if (canGoBack)
                {
                    game.CurrentScene = previousScene;
                }
                inputRecently = true;
            }
        }

        public void Down()
        {
            if (!inputRecently)
            {
                selectChoice(choices[currentChoice].Down);
                inputRecently = true;
            }
        }

        public void Left()
        {
            if (!inputRecently)
            {
                selectChoice(choices[currentChoice].Left);
                inputRecently = true;
            }
        }

        public void Right()
        {
            if (!inputRecently)
            {
                selectChoice(choices[currentChoice].Right);
                inputRecently = true;
            }
        }

        public void Up()
        {
            if (!inputRecently)
            {
                selectChoice(choices[currentChoice].Up);
                inputRecently = true;
            }
        }

        #endregion

        #region Private Methods

        void selectChoice(String choice)
        {
            choices[currentChoice].Selected = false;
            currentChoice = choice;
            choices[currentChoice].Selected = true;
        }

        #endregion

        #region Scene Override

        public override void Update(GameTime gameTime)
        {
            if (inputRecently)
            {
                inputTimer += gameTime.ElapsedGameTime.Milliseconds;
                if (inputTimer >= inputTimeOut)
                {
                    inputRecently = false;
                    inputTimer = 0;
                }
            }
            base.Update(gameTime);
        }

        #endregion
    }
}
