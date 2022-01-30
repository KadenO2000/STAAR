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

        private SpaceSpriteFont spaceFont;

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
            asteroids = new AsteroidSprite[4];
            for(int i = 0; i < 4; i++)
            {
                asteroids[i] = new AsteroidSprite();
            }
            ufo = new UFOSprite()
            {
                Position = new Vector2(Constants.GAME_WIDTH, Constants.GAME_HEIGHT / 2 - 50)
            };

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            spaceFont.LoadContent(Content);
            spaceshipSprite.LoadContent(Content);
            foreach (var asteroid in asteroids) asteroid.LoadContent(Content);
            ufo.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                Exit();

            foreach (var asteroid in asteroids) asteroid.Update(gameTime);
            ufo.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            string title = "Spaceship Trained to\nAnnihilate Asteroids\nRigorously (STAAR):\nThe Game";

            _spriteBatch.Begin();
            foreach (var asteroid in asteroids) asteroid.Draw(gameTime, _spriteBatch);
            spaceshipSprite.Draw(gameTime, _spriteBatch); ;
            spaceFont.Draw(gameTime, _spriteBatch, title, new Vector2(10, 2), new Vector2(0, 0), 1f);
            spaceFont.Draw(gameTime, _spriteBatch, "Enter to Exit Game", new Vector2(10, Constants.GAME_HEIGHT - 50), new Vector2(0, 0), 0.75f);
            ufo.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
