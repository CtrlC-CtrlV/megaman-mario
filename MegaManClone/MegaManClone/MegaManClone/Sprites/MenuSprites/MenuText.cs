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
    class MenuText : ISprite
    {
        #region Fields

        SpriteFont font;
        Vector2 position;
        ITextSource textSource;

        #endregion

        #region Constructor

        public MenuText(ContentManager content, Vector2 position, ITextSource textSource)
        {
            this.position = position;
            this.textSource = textSource;

            font = content.Load<SpriteFont>("Fonts/Menu_Font");
        }

        public MenuText(ContentManager content, Vector2 position, String text)
            : this(content, position, new StringTextSource(text))
        {
            
        }

        #endregion

        #region ISprite Implementation

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.DrawString(font, textSource.GetText(), position, Color.White);
        }

        public void Reset()
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        #endregion
    }
}
