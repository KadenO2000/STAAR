using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace STAAR
{
    public class SpaceSpriteFont
    {
        private SpriteFont space;

        public void LoadContent(ContentManager content)
        {
            space = content.Load<SpriteFont>("space");
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, string s, Vector2 position, Vector2 origin, float scale)
        {
            spriteBatch.DrawString(space, s, position, Color.Yellow, 0, origin, scale, SpriteEffects.None, 0);
        }
    }
}
