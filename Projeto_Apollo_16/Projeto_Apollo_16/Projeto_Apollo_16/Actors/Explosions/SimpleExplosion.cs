using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace Projeto_Apollo_16
{
    public class SimpleExplosion : ExplosionClass
    {
        public SimpleExplosion (Vector2 position, ContentManager content) : base(position, content)
        {
            Lifetime = 100;
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
            sound = content.Load<SoundEffect>(@"Sounds\BUM");
        }

    }
}
