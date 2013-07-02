using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace Projeto_Apollo_16
{
    public class Fuel : ItemClass
    {
        //private int fuel;

        public Fuel(int health, PlayerClass player, Vector2 position, ContentManager content)
            : base(player, position, content)
        {
            //this.fuel = fuel;
            name = "fuel";
            ttl = 20000;
        }

        public override void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"Sprites\items\fuel");
        }

        public override void LoadSound(ContentManager content)
        {
            sound = content.Load<SoundEffect>(@"Sounds\fuel");
        }
    }
}
