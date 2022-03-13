using System;
using Microsoft.Xna.Framework;
using STAAR.StateManagement;
using STAAR.Particles;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace STAAR.Screens
{
    public class LevelScreen : GameScreen
    {
        private ContentManager content;

        private SpaceshipSprite spaceshipSprite;
        private AsteroidSprite[] asteroids;
        private HeartSprite hearts;

        private ExplosionParticleSystem explosion;

        private Texture2D star;
        private Texture2D smallerStar;

        private SoundEffect spaceShipHit;

        private bool shaking;
        private float shakeTime = 0;

        private uint score;
        private uint highScore = 0;
        private short Lives = 3;

        public override void Activate()
        {
            if (content == null) content = new ContentManager(ScreenManager.Game.Services, "Content");

            spaceshipSprite = new SpaceshipSprite(content);
            asteroids = new AsteroidSprite[15];
            for (int i = 0; i < asteroids.Length; i++) asteroids[i] = new AsteroidSprite(content);
            hearts = new HeartSprite(content);

            explosion = new ExplosionParticleSystem(ScreenManager.Game, 20);
            ScreenManager.Game.Components.Add(explosion);

            star = content.Load<Texture2D>("star_small");
            smallerStar = content.Load<Texture2D>("star_tiny");

            spaceShipHit = content.Load<SoundEffect>("ExplosionSound");
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            spaceshipSprite.Update(gameTime);
            foreach (var asteroid in asteroids)
            {
                asteroid.Update(gameTime);
                if (asteroid.Bounds.CollidesWith(spaceshipSprite.Bounds))
                {
                    explosion.PlaceExplosion(asteroid.Center);
                    asteroid.Center = new Vector2(1000, 1000);
                    spaceshipSprite.Color = Color.Red;
                    Lives--;
                    spaceShipHit.Play();
                }
            }
            if (Lives <= 0)
            {
                shaking = true;
                if (score > highScore) highScore = score;
                score = 0;
                Lives = 3;
                spaceshipSprite.Reset();
                foreach (var asteroid in asteroids) asteroid.SetNewAsteroid();
            }
            score = score + 10;
        }

        public override void Draw(GameTime gameTime)
        {
            var spriteBatch = ScreenManager.SpriteBatch;

            Matrix shakeTransform = Matrix.Identity;
            if (shaking)
            {
                shakeTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                shakeTransform = Matrix.CreateTranslation(10 * MathF.Sin(shakeTime), 10 * MathF.Cos(shakeTime), 0);
                if (shakeTime > 500)
                {
                    shaking = false;
                    shakeTime = 0;
                }

            }

            spriteBatch.Begin(transformMatrix: shakeTransform);

            DrawStars(spriteBatch);

            spaceshipSprite.Draw(gameTime, spriteBatch, Vector2.Zero, 0.1f);
            foreach (var asteroid in asteroids) asteroid.Draw(gameTime, spriteBatch);

            ScreenManager.Font.Draw(gameTime, spriteBatch, score.ToString(), new Vector2(2, 2), Vector2.Zero, 1);
            string highScoreString = "High Score: " + highScore.ToString();
            Vector2 length = ScreenManager.Font.MeasureString(highScoreString);
            ScreenManager.Font.Draw(gameTime, spriteBatch, highScoreString, new Vector2(Constants.GAME_WIDTH / 2 - length.X / 2, 2), Vector2.Zero, 0.8f);
            hearts.Draw(gameTime, spriteBatch, Lives);

            spriteBatch.End();
        }

        private void DrawStars(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(star, new Vector2(30, 150), Color.White);
            spriteBatch.Draw(star, new Vector2(120, 300), Color.White);
            spriteBatch.Draw(star, new Vector2(400, 500), Color.White);
            spriteBatch.Draw(star, new Vector2(384, 50), Color.White);
            spriteBatch.Draw(star, new Vector2(86, 25), Color.White);
            spriteBatch.Draw(star, new Vector2(600, 426), Color.White);
            spriteBatch.Draw(star, new Vector2(350, 230), Color.White);
            spriteBatch.Draw(star, new Vector2(700, 179), Color.White);
            spriteBatch.Draw(smallerStar, new Vector2(320, 123), null, Color.White, 0, new Vector2(0,0), 0.5f, SpriteEffects.None, 0);
            spriteBatch.Draw(smallerStar, new Vector2(532, 150), null, Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
            spriteBatch.Draw(smallerStar, new Vector2(621, 320), null, Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
            spriteBatch.Draw(smallerStar, new Vector2(720, 24), null, Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
        }
    }
}
