using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace STAAR.Screens
{
    public class Level1 : LevelScreen
    {
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (score >= 15000)
            {
                LoadingScreen.Load(ScreenManager, true, new Level2());
            }
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            var spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin();
            string goal = "Reach 15000 points!";
            Vector2 length = ScreenManager.Font.MeasureString(goal);
            ScreenManager.Font.Draw(gameTime, spriteBatch, goal, new Vector2(Constants.GAME_WIDTH / 2 - length.X / 2, 2), Vector2.Zero, 0.8f);
            spriteBatch.End();
            base.Draw(gameTime);

        }
    }
}
