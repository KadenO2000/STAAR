using System;
using Microsoft.Xna.Framework;
using STAAR.StateManagement;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;


namespace STAAR.Screens
{
    public class TitleScreen : GameScreen
    {
        private ContentManager content;

        private SpaceshipSprite spaceshipSprite;
        private AsteroidSprite[] asteroids;
        private UFOSprite ufo;

        public TitleScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0);
            TransitionOffTime = TimeSpan.FromSeconds(0);
        }

        public override void Activate()
        {
            if (content == null) content = new ContentManager(ScreenManager.Game.Services, "Content");
            spaceshipSprite = new SpaceshipSprite(content);
            asteroids = new AsteroidSprite[15];
            for (int i = 0; i < asteroids.Length; i++) asteroids[i] = new AsteroidSprite(content);
            ufo = new UFOSprite(content)
            {
                Position = new Vector2(Constants.GAME_WIDTH, Constants.GAME_HEIGHT / 2 - 50)
            };
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            foreach (var asteroid in asteroids) asteroid.Update(gameTime);
            ufo.Update(gameTime);
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            var keyboard = input.CurrentKeyboardState;
            if (keyboard.IsKeyDown(Keys.Enter))
            {
                LoadingScreen.Load(ScreenManager, true, new LevelScreen());
            }
        }

        public override void Draw(GameTime gameTime)
        {
            var spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin();

            foreach (var asteroid in asteroids) asteroid.Draw(gameTime, spriteBatch);
            spaceshipSprite.Draw(gameTime, spriteBatch, new Vector2(550, 350), 0.2f);
            ufo.Draw(gameTime, spriteBatch);

            string title = "Spaceship Trained to\nAnnihilate Asteroids\nRigorously (STAAR):\nThe Game";
            ScreenManager.Font.Draw(gameTime, spriteBatch, title, new Vector2(10, 2), new Vector2(0, 0), 1f);
            ScreenManager.Font.Draw(gameTime, spriteBatch, "Enter to Start Game", new Vector2(10, Constants.GAME_HEIGHT - 50), new Vector2(0, 0), 0.75f);

            spriteBatch.End();
        }
    }
}
