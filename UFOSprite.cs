using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace STAAR
{
    public class UFOSprite
    {
        private Texture2D texture;

        private double animationTimer;

        private short animationFrame = 0;
        private short animationRow = 0;

        public Vector2 Position;

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("spaceship");
        }

        public void Update(GameTime gameTime)
        {
            Position += new Vector2(-1, 0) * 50 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Position.X < -1000)
            {
                Position = new Vector2(Constants.GAME_WIDTH + 100, Constants.GAME_HEIGHT / 2 - 50);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if(animationTimer > 0.3)
            {
                animationFrame++;
                if (animationFrame > 4)
                {
                    animationFrame = 0;
                    if (animationRow == 0) animationRow = 1;
                    else animationRow = 0;
                }
                if (animationRow == 1 && animationFrame == 0) animationFrame = 1;
                animationTimer -= 0.3;
            }

            var source = new Rectangle(animationFrame * 200, animationRow * 110, 200, 110);
            spriteBatch.Draw(texture, Position, source, Color.White, 0, new Vector2(0,0), 0.5f, SpriteEffects.None, 0);
        }
    }
}
