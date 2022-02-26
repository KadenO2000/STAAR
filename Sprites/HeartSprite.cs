using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace STAAR
{
    public class HeartSprite
    {
        private Texture2D texture;
        private int spacing;

        public HeartSprite(ContentManager content)
        {
            LoadContent(content);
        }

        public void LoadContent(ContentManager contentManager)
        {
            texture = contentManager.Load<Texture2D>("heart");
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, short lives)
        {
            spacing = 30;
            while (lives > 0)
            {
                spriteBatch.Draw(texture, new Vector2(Constants.GAME_WIDTH - spacing, 2), Color.White);
                spacing = spacing + 30;
                lives--;
            } 
        }
    }
}
