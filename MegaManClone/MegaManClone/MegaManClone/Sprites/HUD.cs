using MegaManClone.Entities;
using MegaManClone.Sprites.ItemSprites;
using MegaManClone.Sprites.MegamanSprites;
using MegaManClone.Stages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaManClone.Sprites
{
    class HUD : ISprite
    {
        #region Fields

        Vector2 characterPosition = new Vector2(16, 114);
        Texture2D characterTexture;
        Vector2 emptyHealthbarPosition = new Vector2(12, 12);
        Rectangle emptyHealthbarRect = new Rectangle(0, 0, 32, 128);
        Texture2D emptyHealthbarTexture;
        SpriteFont font;
        Vector2 fullHealthbarPosition = new Vector2(12, 18);
        Rectangle fullHealthbarRect = new Rectangle(0, 0, 32, 128);
        Texture2D fullHealthbarTexture;
        readonly int healthbarTopBorder = 6;
        readonly int healthbarSize = 94;
        Vector2 lifePosition = new Vector2(228, 12);
        Vector2 lifeTextPosition = new Vector2(258, 12);
        Vector2 lifeTankPosition = new Vector2(148, 16);
        Vector2 lifeTankScale = new Vector2(0.75f, 0.5f);
        Vector2 lifeTankTextPosition = new Vector2(173, 12);
        Texture2D lifeTankTexture;
        Megaman megaman;
        Vector2 scorePosition = new Vector2(50, 12);
        Stage stage;
        Vector2 timePosition = new Vector2(750, 12);

        #endregion

        #region Constructor

        public HUD(ContentManager content, Megaman megaman, Stage stage)
        {
            characterTexture = content.Load<Texture2D>("items/megaman_helmet");
            font = content.Load<SpriteFont>("Fonts/HUD_Font");
            fullHealthbarTexture = content.Load<Texture2D>("HUD/full_health");
            lifeTankTexture = content.Load<Texture2D>("items/life_tank");
            emptyHealthbarTexture = content.Load<Texture2D>("HUD/no_health");

            this.megaman = megaman;
            this.stage = stage;
        }

        #endregion

        #region ISprite Implementation

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            string coins = String.Format("X {0}", megaman.Coins.ToString().PadLeft(2, '0'));
            string lives = String.Format("X {0}", megaman.Lives);
            string score = String.Format("{0}", megaman.Points.ToString().PadLeft(8, '0'));
            string time = String.Format("{0}", stage.Timer);
            string health = String.Format("Health: {0}", megaman.MMHealth.ToString());

            spriteBatch.Draw(characterTexture, camera.Offset + characterPosition, new Rectangle(0, 0, 32, 32), Color.White, 0, Vector2.Zero, 0.75f, SpriteEffects.None, 0);
            spriteBatch.Draw(characterTexture, camera.Offset + lifePosition, new Rectangle(0, 0, 32, 32), Color.White, 0, Vector2.Zero, 0.75f, SpriteEffects.None, 0);
            spriteBatch.Draw(lifeTankTexture, camera.Offset + lifeTankPosition, new Rectangle(0, 0, 32, 32), Color.White, 0, Vector2.Zero, lifeTankScale, SpriteEffects.None, 0);
            
            spriteBatch.Draw(emptyHealthbarTexture, camera.Offset + emptyHealthbarPosition, emptyHealthbarRect, Color.White);
            spriteBatch.Draw(fullHealthbarTexture, camera.Offset + fullHealthbarPosition, fullHealthbarRect, Color.White);

            spriteBatch.DrawString(font, coins, camera.Offset + lifeTankTextPosition, Color.White);
            spriteBatch.DrawString(font, lives, camera.Offset + lifeTextPosition, Color.White);
            spriteBatch.DrawString(font, score, camera.Offset + scorePosition, Color.White);
            spriteBatch.DrawString(font, time, camera.Offset + timePosition, Color.White);

            spriteBatch.Draw(characterTexture, camera.Offset + characterPosition, new Rectangle(0, 0, 32, 32), Color.White, 0, Vector2.Zero, 0.75f, SpriteEffects.None, 0);
        }

        public void Reset()
        {

        }

        public void Update(GameTime gameTime)
        {
            emptyHealthbarRect.Height = healthbarTopBorder + (int)(healthbarSize * (1 - (float)megaman.Health / (float)megaman.MaxHealth));
            fullHealthbarPosition.Y = emptyHealthbarPosition.Y + emptyHealthbarRect.Height;
            fullHealthbarRect.Y = emptyHealthbarRect.Bottom + 1;
            fullHealthbarRect.Height = fullHealthbarTexture.Height - emptyHealthbarRect.Height;
        }

        #endregion
    }
}
