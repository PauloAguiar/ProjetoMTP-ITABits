using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace Projeto_Apollo_16
{
    public class Health : ItemClass
    {
        //private int health;

        public Health(int health, PlayerClass player, Vector2 position, ContentManager content) : base (player, position, content)
        {
            //this.health = health;
            name = "life";
            ttl = 20000;
        }

        public override void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"Sprites\items\health");
        }

        public override void LoadSound(ContentManager content)
        {
            sound = content.Load<SoundEffect>(@"Sounds\shield");
        }

    }
}
