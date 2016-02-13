using MegaManClone.Stages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Sprites.MenuSprites
{
    class MenuChoice : ISprite
    {
        #region Fields

        String id;
        String destination;
        String down;
        SpriteFont font;
        MegamanGame game;
        String left;
        Vector2 position;
        String right;
        bool selected = false;
        String text;
        String up;

        #endregion

        #region Properties

        public String Down
        {
            get { return down; }
            set { down = value; }
        }

        public String Id
        {
            get { return id; }
            set { id = value; }
        }

        public String Left
        {
            get { return left; }
            set { left = value; }
        }

        public String Right
        {
            get { return right; }
            set { right = value; }
        }

        public bool Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        public String Up
        {
            get { return up; }
            set { up = value; }
        }

        #endregion

        #region Constructor

        public MenuChoice(String id, ContentManager content, String destination, MegamanGame game, Vector2 position, String text)
        {
            this.id = id;
            this.destination = destination;
            this.down = id;
            this.game = game;
            this.left = id;
            this.position = position;
            this.right = id;
            this.text = text;
            this.up = id;

            font = content.Load<SpriteFont>("Fonts/Menu_Font");
        }

        #endregion

        #region ISprite Implementation

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.DrawString(font, text, position, selected ? Color.Red : Color.White);
        }

        public void Reset()
        {
            selected = false;
        }

        public void Update(GameTime gameTime)
        {

        }

        #endregion

        #region Methods

        public void Activate()
        {
            game.SceneFactory.GetScene(destination).Enter();
        }

        #endregion
    }
}
