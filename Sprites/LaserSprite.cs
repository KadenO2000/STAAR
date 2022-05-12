using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using STAAR.Collisions;

namespace STAAR.Sprites
{
    public class LaserSprite
    {
        private Texture2D texture;
        public Vector2 Position;
        public BoundingRectangle Bounds;

        public LaserSprite(ContentManager content, SpaceshipSprite spaceship)
        {
            Position.X = spaceship.Position.X + 17;
            Position.Y = spaceship.Position.Y+20;
            Bounds = new BoundingRectangle(spaceship.Position.X, spaceship.Position.Y, 20, 50);
            LoadContent(content);
        }

        private void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("laser");
        }

        public void Update(GameTime gametime)
        {
            Position += new Vector2(0, -300 * (float)gametime.ElapsedGameTime.TotalSeconds);
            Bounds.X = Position.X;
            Bounds.Y = Position.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, null, Color.White, -MathHelper.PiOver2, new Vector2(0,0), new Vector2((float)0.2,(float)0.2), SpriteEffects.None, 0);
        }
    }
}
