using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace Projeto_Apollo_16
{
    public class SimpleExplosion : ExplosionClass
    {
        const float MAX_VOLUME = 1.0f;
        const float MIN_VOLUME = 0.0f;

        public SimpleExplosion (Vector2 playerPosition, Vector2 enemyPosition, ContentManager content) : base(playerPosition, enemyPosition, content)
        {
            lifeTime = 100;
            sound.Play(GetVolume(), 0, 0);
        }

        float GetVolume()
        {
            volume = 1 / distance * 1000;
            volume = MathHelper.Clamp(volume, MIN_VOLUME, MAX_VOLUME);
            return volume;
        }

        public override void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"Sprites\Explosions\explosion");
        }

        public override void LoadFont(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>(@"Fonts\ActorInfo");
        }

        public override void LoadSound(ContentManager content)
        { 
            sound = content.Load<SoundEffect>(@"Sounds\explosion2");
        }

    }
}
