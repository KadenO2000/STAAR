using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using STAAR.Collisions;

namespace STAAR
{
    public class SpaceshipSprite
    {
        KeyboardState currentKeyboardState;
        KeyboardState priorKeyboardState;

        private Texture2D texture;
        private Vector2 position = new Vector2(Constants.GAME_WIDTH/2, 350);
        private short hitTimer = 0;


        public Color Color = Color.White;
        public BoundingRectangle Bounds;

        public SpaceshipSprite(ContentManager content)
        {
            Bounds = new BoundingRectangle(position.X, position.Y + 10, 56, 16);
            LoadContent(content);
        }

        public void LoadContent(ContentManager contentManager)
        {
            texture = contentManager.Load<Texture2D>("ship");
        }

        public void Update(GameTime gameTime)
        {
            if (Color == Color.Red) ++hitTimer;
            if(hitTimer >= 40)
            {
                Color = Color.White;
                hitTimer = 0;
            }

            priorKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            if ((currentKeyboardState.IsKeyDown(Keys.Left) ||
                currentKeyboardState.IsKeyDown(Keys.A)) && position.X > 0)
            {
                position += new Vector2(-300 * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);
            }
            if ((currentKeyboardState.IsKeyDown(Keys.Right) ||
                currentKeyboardState.IsKeyDown(Keys.D)) && position.X < Constants.GAME_WIDTH - 56)
            {
                position += new Vector2(300 * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);
            }
            if ((currentKeyboardState.IsKeyDown(Keys.Up) ||
                currentKeyboardState.IsKeyDown(Keys.W)) && position.Y > 0)
            {
                position += new Vector2(0, -300 * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            if ((currentKeyboardState.IsKeyDown(Keys.Down) ||
                currentKeyboardState.IsKeyDown(Keys.S)) && position.Y < Constants.GAME_HEIGHT - 40)
            {
                position += new Vector2(0, 300 * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            Bounds.X = position.X;
            Bounds.Y = position.Y + 10;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 pos, float scale)
        {
            if (pos != Vector2.Zero) position = pos;
            spriteBatch.Draw(texture, position, null, Color, 0, new Vector2(0,0), scale, SpriteEffects.None, 0);
        }

        public void Reset()
        {
            position = new Vector2(Constants.GAME_WIDTH / 2, 350);
        }
    }
}
