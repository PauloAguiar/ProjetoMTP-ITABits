using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace Projeto_Apollo_16
{
    public class Shield : ItemClass
    {
        private const double MAX_USING_TIME = 5000;
        
        public Shield(int health, PlayerClass player, Vector2 position, ContentManager content) : base (player, position, content)
        {
            name = "shield";
            ttl = 15000;
            timeLiving = 0;
            IsUsing = true;
        }

        public override void  Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            if(timeLiving >= MAX_USING_TIME)
            {
                IsUsing = false;
                //destroy
            }
            
        }

        public override void  LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"Sprites\items\shield");
        }

        public override void LoadSound(ContentManager content)
        {
            sound = content.Load<SoundEffect>(@"Sounds\shield");
        }
    }
}
