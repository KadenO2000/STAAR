﻿using System;
using Microsoft.Xna.Framework;
using STAAR.StateManagement;

namespace STAAR.Screens
{
    public class LoadingScreen : GameScreen
    {
        private readonly bool _loadingIsSlow;
        private bool _otherScreensAreGone;
        private readonly GameScreen[] _screensToLoad;

        private LoadingScreen(ScreenManager screenManager, bool loadingIsSlow, GameScreen[] screensToLoad)
        {
            _loadingIsSlow = loadingIsSlow;
            _screensToLoad = screensToLoad;

            TransitionOnTime = TimeSpan.FromSeconds(0.5);
        }

        public static void Load(ScreenManager screenManager, bool loadingIsSlow, params GameScreen[] screensToLoad)
        {
            foreach (var screen in screenManager.GetScreens()) screen.ExitScreen();

            var loadingScreen = new LoadingScreen(screenManager, loadingIsSlow, screensToLoad);

            screenManager.AddScreen(loadingScreen);
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            if (_otherScreensAreGone)
            {
                ScreenManager.RemoveScreen(this);

                foreach (var screen in _screensToLoad)
                {
                    if (screen != null)
                        ScreenManager.AddScreen(screen);
                }
                ScreenManager.Game.ResetElapsedTime();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (ScreenState == ScreenState.Active && ScreenManager.GetScreens().Length == 1)
                _otherScreensAreGone = true;

            if (_loadingIsSlow)
            {
                var spriteBatch = ScreenManager.SpriteBatch;
                var font = ScreenManager.Font;

                const string message = "Loading...";

                var viewport = ScreenManager.GraphicsDevice.Viewport;
                var viewportSize = new Vector2(viewport.Width, viewport.Height);
                var textSize = font.MeasureString(message);
                var textPosition = (viewportSize - textSize) / 2;

                var color = Color.White * TransitionAlpha;

                spriteBatch.Begin();
                font.Draw(gameTime, spriteBatch, message, textPosition, new Vector2(0,0), 1);
                spriteBatch.End();
            }
        }
    }
}
