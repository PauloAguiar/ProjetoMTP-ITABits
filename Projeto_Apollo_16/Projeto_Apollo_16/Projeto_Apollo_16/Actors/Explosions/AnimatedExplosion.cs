using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace Projeto_Apollo_16
{
    public class AnimatedExplosion : ExplosionClass
    {
        const float MAX_VOLUME = 1.0f;
        const float MIN_VOLUME = 0.0f;

         int i, j;
         const double DELAY = 0.005;
         double time;
         int x;
         public AnimatedExplosion(Vector2 playerPosition, Vector2 enemyPosition, ContentManager content)
             : base(playerPosition, enemyPosition, content)
        {
            lifeTime = 25;
            i = j = 0;
            time = 0;
            x = 1;
        }

        public override void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"Sprites\Explosions\explosionsheet");
        }


        public override void LoadFont(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>(@"Fonts\ActorInfo");
        }

        public override void LoadSound(ContentManager content)
        { 
            sound = content.Load<SoundEffect>(@"Sounds\explosion3");
        }

        float GetVolume()
        {
            volume = 1 / distance * 1000;
            volume = MathHelper.Clamp(volume, MIN_VOLUME, MAX_VOLUME);
            return volume;
        }

        public override void Update(GameTime gameTime)
        {
            time += gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);

            if (x == 1)
                sound.Play(volume, 0, 0);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle r = new Rectangle (i,j,64,64);
            spriteBatch.Draw(texture, globalPosition, r, Color.White);
            if (time > DELAY * x)
            {
                i = (i + 64) % 320;
                j = (x / 4) * 64;
                x++;
            }

        }
    }
}
