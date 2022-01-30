using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace STAAR
{
    public class SpaceshipSprite
    {
        private Texture2D texture;

        private Vector2 position = new Vector2(560, 350);

        public void LoadContent(ContentManager contentManager)
        {
            texture = contentManager.Load<Texture2D>("ship");
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0, new Vector2(80, 80), 0.2f, SpriteEffects.None, 0);
        }
    }
}
