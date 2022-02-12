using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace STAAR
{
    public class STAAR : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private SpaceshipSprite spaceshipSprite;
        private AsteroidSprite[] asteroids;
        private UFOSprite ufo;
        private HeartSprite hearts;

        private Texture2D star;
        private Texture2D smallerStar;

        private SpaceSpriteFont spaceFont;

        private uint score;
        private uint highScore = 0; 
        private short Lives = 3;

        public STAAR()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            spaceFont = new SpaceSpriteFont();
            spaceshipSprite = new SpaceshipSprite();
            asteroids = new AsteroidSprite[15];
            for(int i = 0; i < asteroids.Length; i++)
            {
                asteroids[i] = new AsteroidSprite();
            }
            ufo = new UFOSprite()
            {
                Position = new Vector2(Constants.GAME_WIDTH, Constants.GAME_HEIGHT / 2 - 50)
            };
            hearts = new HeartSprite();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            spaceFont.LoadContent(Content);
            spaceshipSprite.LoadContent(Content);
            foreach (var asteroid in asteroids) asteroid.LoadContent(Content);
            ufo.LoadContent(Content);
            hearts.LoadContent(Content);
            star = Content.Load<Texture2D>("star_small");
            smallerStar = Content.Load<Texture2D>("star_tiny");

        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            spaceshipSprite.Update(gameTime);
            foreach (var asteroid in asteroids)
            {
                asteroid.Update(gameTime);
                if (asteroid.Bounds.CollidesWith(spaceshipSprite.Bounds))
                {
                    asteroid.Center = new Vector2(1000, 1000);
                    spaceshipSprite.Color = Color.Red;
                    Lives--;
                } 
            }
            if(Lives <= 0)
            {
                if (score > highScore) highScore = score;
                score = 0;
                Lives = 3;
                spaceshipSprite.Reset();
                foreach (var asteroid in asteroids) asteroid.SetNewAsteroid();
            }
            score = score + 10;
            //ufo.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //string title = "Spaceship Trained to\nAnnihilate Asteroids\nRigorously (STAAR):\nThe Game";

            _spriteBatch.Begin();
            DrawStars();

            foreach (var asteroid in asteroids) asteroid.Draw(gameTime, _spriteBatch);
            spaceshipSprite.Draw(gameTime, _spriteBatch);

            //spaceFont.Draw(gameTime, _spriteBatch, title, new Vector2(10, 2), new Vector2(0, 0), 1f);
            //spaceFont.Draw(gameTime, _spriteBatch, "Enter to Exit Game", new Vector2(10, Constants.GAME_HEIGHT - 50), new Vector2(0, 0), 0.75f);

            spaceFont.Draw(gameTime, _spriteBatch, score.ToString(), new Vector2(2, 2), Vector2.Zero, 1);
            string highScoreString = "High Score: " + highScore.ToString();
            Vector2 length = spaceFont.MeasureString(highScoreString);
            spaceFont.Draw(gameTime, _spriteBatch, highScoreString, new Vector2(Constants.GAME_WIDTH/2 - length.X/2, 2), Vector2.Zero, 0.8f);
            hearts.Draw(gameTime, _spriteBatch, Lives);

            //ufo.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawStars()
        {
            _spriteBatch.Draw(star, new Vector2(30, 150), Color.White);
            _spriteBatch.Draw(star, new Vector2(120, 300), Color.White);
            _spriteBatch.Draw(star, new Vector2(400, 500), Color.White);
            _spriteBatch.Draw(star, new Vector2(384, 50), Color.White);
            _spriteBatch.Draw(star, new Vector2(86, 25), Color.White);
            _spriteBatch.Draw(star, new Vector2(600, 426), Color.White);
            _spriteBatch.Draw(star, new Vector2(350, 230), Color.White);
            _spriteBatch.Draw(star, new Vector2(700, 179), Color.White);
            _spriteBatch.Draw(smallerStar, new Vector2(320, 123), null, Color.White, 0, new Vector2(0,0), 0.5f, SpriteEffects.None, 0);
            _spriteBatch.Draw(smallerStar, new Vector2(532, 150), null, Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
            _spriteBatch.Draw(smallerStar, new Vector2(621, 320), null, Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
            _spriteBatch.Draw(smallerStar, new Vector2(720, 24), null, Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
        }
    }
}
