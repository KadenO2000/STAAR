using System;
using Microsoft.Xna.Framework;
using STAAR.StateManagement;
using STAAR.Particles;
using STAAR.Sprites;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace STAAR.Screens
{
    public class LevelScreen : GameScreen
    {
        private ContentManager content;

        KeyboardState currentKeyboardState;
        KeyboardState priorKeyboardState;

        private SpaceshipSprite spaceshipSprite;
        private AsteroidSprite[] asteroids;
        private HeartSprite hearts;
        private List<LaserSprite> lasers = new List<LaserSprite>();

        private ExplosionParticleSystem explosion;

        private Texture2D star;
        private Texture2D smallerStar;

        private SoundEffect spaceShipHit;
        private SoundEffect laserShot;
        private SoundEffect asteroidHit;

        private bool shaking;
        private float shakeTime = 0;

        protected uint score;
        private short Lives = 3;
        protected short laserShots;
        protected short numDestroyed;

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
            laserShot = content.Load<SoundEffect>("Laser_Shoot");
            asteroidHit = content.Load<SoundEffect>("AsteroidExplosion");

        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            priorKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

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
                foreach(LaserSprite shot in lasers)
                {
                    if (asteroid.Bounds.CollidesWith(shot.Bounds))
                    {
                        explosion.PlaceExplosion(asteroid.Center);
                        asteroid.Center = new Vector2(1000, 1000);
                        shot.Position.X = 1000;
                        score += 1000;
                        ++numDestroyed;
                        asteroidHit.Play();
                    }
                }
            }

            if(currentKeyboardState.IsKeyDown(Keys.Space) && priorKeyboardState.IsKeyUp(Keys.Space))
            {
                if (laserShots > 0)
                {
                    lasers.Add(new LaserSprite(content, spaceshipSprite));
                    laserShots--;
                    laserShot.Play();
                }
                else; //play empty sound
            }
            foreach(LaserSprite shot in lasers)
            {
                shot.Update(gameTime);
            }
            if (Lives <= 0)
            {
                LoadingScreen.Load(ScreenManager, true, new LoseScreen());
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

            foreach(LaserSprite shot in lasers)
            {
                shot.Draw(spriteBatch);
            }

            spaceshipSprite.Draw(gameTime, spriteBatch, Vector2.Zero, 0.1f);
            foreach (var asteroid in asteroids) asteroid.Draw(gameTime, spriteBatch);

            ScreenManager.Font.Draw(gameTime, spriteBatch, score.ToString(), new Vector2(2, 2), Vector2.Zero, 1);
            hearts.Draw(gameTime, spriteBatch, Lives);

            string shotsText = "Shots Left: " + laserShots;
            ScreenManager.Font.Draw(gameTime, spriteBatch, shotsText, new Vector2(2, Constants.GAME_HEIGHT -20), new Vector2(0, 0), (float)0.5);

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
