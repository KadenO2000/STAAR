using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace STAAR
{
    public class AsteroidSprite
    {
        System.Random random = new System.Random();
        private bool firstDraw = true;

        private Texture2D texture;
        private float radius;
        private float rotation;

        public Vector2 Center { get; set; }
        public Vector2 Velocity { get; set; }

        public AsteroidSprite()
        {
            radius = random.Next(10, 30);
            rotation = (float)random.NextDouble() * MathHelper.TwoPi;
            Velocity = new Vector2(random.Next(-10,10), (float)random.NextDouble() * 300);
            Center = new Vector2(random.Next(50, 680), 50);
        }

        public void LoadContent(ContentManager contentManager)
        {
            texture = contentManager.Load<Texture2D>("AsteroidBrown");
        }

        public void Update(GameTime gameTime)
        {
            Center += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Center.Y < radius * 2 || Center.Y > Constants.GAME_HEIGHT + radius || Center.X < radius * 2 || Center.X > Constants.GAME_WIDTH + radius)
            {
                radius = random.Next(10, 30);
                rotation = (float)random.NextDouble() * MathHelper.TwoPi;
                Velocity = new Vector2(random.Next(-10, 10), (float)random.NextDouble() * 300);
                Center = new Vector2(random.Next(50, 680), 50);
            }
                
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Center, null, Color.White, rotation, new Vector2(80, 80), radius / 50, SpriteEffects.None, 0);
        }
    }
}
