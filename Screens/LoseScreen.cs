using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using STAAR.StateManagement;

namespace STAAR.Screens
{
    public class LoseScreen : GameScreen
    {
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter)) LoadingScreen.Load(ScreenManager, true, new Level1());
        }

        public override void Draw(GameTime gameTime)
        {

            var spriteBatch = ScreenManager.SpriteBatch;
            var font = ScreenManager.Font;

            const string message = "You Lose!";
            const string restartMessage = "Press Enter to Play Again!";

            var viewport = ScreenManager.GraphicsDevice.Viewport;
            var viewportSize = new Vector2(viewport.Width, viewport.Height);
            var textSize = font.MeasureString(message);
            var restartTextSize = font.MeasureString(restartMessage);
            var textPosition = (viewportSize - textSize) / 2;

            var color = Color.White * TransitionAlpha;

            spriteBatch.Begin();
            font.Draw(gameTime, spriteBatch, message, textPosition, new Vector2(0, 0), 1);
            font.Draw(gameTime, spriteBatch, restartMessage, new Vector2(textPosition.X - 60, textPosition.Y + 50), new Vector2(0, 0), (float)0.5);
            spriteBatch.End();

        }
    }
}
