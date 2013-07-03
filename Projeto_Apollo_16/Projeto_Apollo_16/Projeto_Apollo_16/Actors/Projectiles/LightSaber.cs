using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace Projeto_Apollo_16
{
    public class LightSaber : ProjectileClass
    {
        private const float INITIAL_ANGLE = - MathHelper.Pi / 2;
        private const float OMEGA = - 0.01f;
        private const float RADIUS = 50.0f;
        private Vector2 centralPosition;
        private Vector2 direction;
        public float Angle { get; private set; }
        PlayerClass player;


        public LightSaber(Vector2 initialPosition, ContentManager content, PlayerClass player)
            : base(initialPosition, content)
        {
            ttl = 250;
            globalPosition = initialPosition + RADIUS * MathFunctions.AngleToVector(player.Angle);
            Angle = INITIAL_ANGLE + player.Angle;
            centralPosition = initialPosition;
            this.player = player;
 
        }

        public override void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"Sprites\Shoots\lightsaber");
        }

        public override void LoadFont(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>(@"Fonts\ActorInfo");
        }

        public override void LoadSound(ContentManager content)
        {
            sound = content.Load<SoundEffect>(@"Sounds\Pow");
        }

        public override void Update(GameTime gameTime)
        {
            //movimento do centro do sabre ao longo do arco de circunferência
            double dt = gameTime.ElapsedGameTime.TotalMilliseconds;

            centralPosition = player.GlobalPosition;
            Vector2 radial = globalPosition - centralPosition;
            Vector2 tangent = new Vector2(-radial.Y, radial.X);
            tangent.Normalize();
            direction = tangent;

            //globalPosition += tangent * OMEGA * RADIUS * (float)dt;
            Angle += OMEGA * (float)dt;

            radial = globalPosition - centralPosition;
            radial.Normalize();
            radial *= RADIUS;

            globalPosition = centralPosition + radial;
            globalPosition += player.Direction * player.Speed * (float)dt;
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, GlobalPosition, texture.Bounds, Color.White, Angle, Vector2.Zero, 1.0f, SpriteEffects.None, Globals.PLAYER_LAYER);
        }

    }
}

