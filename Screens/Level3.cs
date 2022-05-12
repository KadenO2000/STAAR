using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace STAAR.Screens
{
    public class Level3 : LevelScreen
    {
        public Level3()
        {
            laserShots = short.MaxValue;
            numDestroyed = 0;
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (numDestroyed >= 50)
            {
                LoadingScreen.Load(ScreenManager, true, new Level4());
            }
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            var spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin();
            string goal = "Annihilate 50 Asteroids!";
            Vector2 length = ScreenManager.Font.MeasureString(goal);
            ScreenManager.Font.Draw(gameTime, spriteBatch, goal, new Vector2(Constants.GAME_WIDTH / 2 - length.X /3, 2), Vector2.Zero, 0.8f);
            spriteBatch.End();
            base.Draw(gameTime);

        }
    }
}
