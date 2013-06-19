using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Projeto_Apollo_16
{
    public class Health : ItemClass
    {
        private int heath;

        public Health(int health, PlayerClass player, Vector2 position, ContentManager content) : base (player, position, content)
        {
            this.heath = health;
            name = "life";
            ttl = 1000;
        }

        //não precisa de update e draw porque são iguais ao da classe pai

    }
}
