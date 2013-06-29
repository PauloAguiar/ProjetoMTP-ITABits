using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using ProjectMercury;
using ProjectMercury.Emitters;
using ProjectMercury.Modifiers;
using ProjectMercury.Renderers;
using Microsoft.Xna.Framework.Audio;

namespace Apollo_16_Radar
{
    
    public class GamePlayScreen : BaseGameState
    {
        public RadarDataClass data;
        public RadarImmediateData immediateData;
        Texture2D redDot;
        Texture2D backgroundTexture;
        Texture2D playerIcon;

        Renderer particleRenderer;
        ParticleEffect particleEffect;

        public TimeSpan updateRadar;
        SoundEffect radarBeep;
        public Boolean radarBeepPlaying = true;
        /* Constructor */
        public GamePlayScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
            systemRef = (SystemClass)game;
            data = new RadarDataClass();
            immediateData = new RadarImmediateData();
            particleRenderer = new SpriteBatchRenderer
            {
                GraphicsDeviceService = systemRef.graphics,
            };
            particleEffect = new ParticleEffect();
            updateRadar = new TimeSpan(0, 0, 5);
        }

        /* XNA Methods */
        public override void Initialize()
        {
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            particleRenderer.LoadContent(content);
            particleEffect = content.Load<ParticleEffect>(@"GlowingCircle");
            particleEffect.LoadContent(content);
            particleEffect.Initialise();

            backgroundTexture = content.Load<Texture2D>(@"radarBackground");
            redDot = content.Load<Texture2D>(@"red_dot_2");
            playerIcon = content.Load<Texture2D>(@"playerIcon");

            radarBeep = content.Load<SoundEffect>(@"radar");
            base.LoadContent();
        }

        int delay = 1200;
        public override void Update(GameTime gameTime)
        {
            systemRef.networkManager.ReadPackets(this);
            updateRadar += gameTime.ElapsedGameTime;
            if (updateRadar > TimeSpan.FromMilliseconds(delay - radarBeep.Duration.Milliseconds + 112) && radarBeepPlaying == false)
            {
                radarBeep.Play();
                radarBeepPlaying = true;
            }

            if (updateRadar > TimeSpan.FromMilliseconds(delay))
            {
                Vector2 position;
                foreach (EnemyClass enemy in data.enemies)
                {
                    position = CalculateRelativePosition(enemy, data.playerGlobalPosition);
                    particleEffect.Trigger(position);
                }
                updateRadar = TimeSpan.Zero;
                radarBeepPlaying = false;
            }
            particleEffect.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            systemRef.spriteBatch.Begin();
            systemRef.spriteBatch.Draw(backgroundTexture, Vector2.Zero, Color.White);

            DrawPlayer(systemRef.spriteBatch, new Vector2(systemRef.screenRectangle.Width / 2, systemRef.screenRectangle.Height / 2), immediateData.playerAngle);

            systemRef.spriteBatch.End();

            particleRenderer.RenderEffect(particleEffect);

            base.Draw(gameTime);
            
        }

        private void DrawPlayer(SpriteBatch spriteBatch, Vector2 position, float angle)
        {
            spriteBatch.Draw(playerIcon, position, null, Color.White, angle, new Vector2(playerIcon.Width/2, playerIcon.Height/2), 1.0f, SpriteEffects.None, 0);
        }

        private Vector2 CalculateRelativePosition(EnemyClass enemy, Vector2 playerPos)
        {
            int factor = 5;
            float X = systemRef.screenRectangle.Width/2;
            float Y = systemRef.screenRectangle.Height/2;
            X += (enemy.globalPosition.X - playerPos.X) / factor;
            Y += (enemy.globalPosition.Y - playerPos.Y) / factor;
            return new Vector2(X, Y);
        }
    }
}

