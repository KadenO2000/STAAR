using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using STAAR.Collisions;


namespace STAAR
{
    public class AsteroidSprite
    {
        System.Random random = new System.Random();

        private Texture2D texture;
        private float radius;
        private float rotation;

        public Vector2 Center { get; set; }
        public Vector2 Velocity { get; set; }
        public BoundingCircle Bounds;

        public AsteroidSprite(ContentManager content)
        {
            SetNewAsteroid();
            Bounds = new BoundingCircle(Center, radius);
            LoadContent(content);
        }

        public void LoadContent(ContentManager contentManager)
        {
            texture = contentManager.Load<Texture2D>("AsteroidBrown");
        }

        public void Update(GameTime gameTime)
        {
            Center += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Center.Y > Constants.GAME_HEIGHT + radius || Center.X > Constants.GAME_WIDTH + radius)
            {
                SetNewAsteroid();
            }
            Bounds.Center = Center;
            Bounds.Radius = radius;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Center, null, Color.White, rotation, new Vector2(80, 80), radius / 50, SpriteEffects.None, 0);
        }

        public void SetNewAsteroid()
        {
            radius = random.Next(10, 30);
            rotation = (float)random.NextDouble() * MathHelper.TwoPi;
            Velocity = new Vector2(random.Next(-20, 20), (float)(random.NextDouble() + 0.3) * 300);
            Center = new Vector2(random.Next((int)radius, Constants.GAME_WIDTH - (int)radius), 0);
        }
    }
}
