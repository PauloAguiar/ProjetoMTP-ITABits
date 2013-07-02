using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Projeto_Apollo_16
{
    public class AmmoHoming : ItemClass
    {

        public AmmoHoming(int health, PlayerClass player, Vector2 position, ContentManager content)
            : base(player, position, content)
        {
            name = "ammo_homing";
            ttl = 20000;
        }

        public override void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"Sprites\items\ammo_homing");
        }


    }
}