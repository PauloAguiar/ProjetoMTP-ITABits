using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace Projeto_Apollo_16
{
    public class CircularProjectile : ProjectileClass
    {
        private const float SPEED = 2f;
        private const int RADIUS = 400;
        private Vector2 centralPosition;
        private Vector2 direction;
        float period = 6.28f*RADIUS/SPEED;
        PlayerClass player;
        public static int ammo = 20;
        
        public CircularProjectile(Vector2 initialPosition, ContentManager content, PlayerClass player)
            : base(initialPosition, content)
        {
            ttl = 4000;
            globalPosition = initialPosition + new Vector2(RADIUS, 0);
            centralPosition = initialPosition;
            this.player = player;
        }

        

        public override void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"Sprites\Shoots\bullet");
        }

        public override void LoadFont(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>(@"Fonts\ActorInfo");
        }

        public override void LoadSound(ContentManager content)
        {
            sound = content.Load<SoundEffect>(@"Sounds\Pow");
        }

        public override void  Update(GameTime gameTime)
        {
 	        //movimento circular
            double dt = gameTime.ElapsedGameTime.TotalMilliseconds;

            //o som tem que tocar a cada período
            period += (float)dt;
            if (period >= 2 * 3.14 * RADIUS / SPEED)
            {
                sound.Play();
                period = 0;
            }
            
            centralPosition = player.GlobalPosition;
            Vector2 radial = globalPosition - centralPosition;
            Vector2 tangent = new Vector2(-radial.Y, radial.X);
            tangent.Normalize();
            direction = tangent;

            globalPosition += tangent * SPEED * (float)dt;

            radial = globalPosition - centralPosition;
            radial.Normalize();
            radial *= RADIUS;

            globalPosition = centralPosition + radial;
            globalPosition += player.Direction * player.Speed * (float)dt;
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, GlobalPosition, texture.Bounds, Color.White, (float)-Math.Atan2(direction.X, direction.Y)-(float)Math.PI/2, new Vector2(texture.Width / 2, texture.Height / 2), 1.0f, SpriteEffects.None, Globals.PLAYER_LAYER);
        }
    }
}
