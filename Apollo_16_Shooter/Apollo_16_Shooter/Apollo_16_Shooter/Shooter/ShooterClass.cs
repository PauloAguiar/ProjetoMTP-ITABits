using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Lidgren.Network;

namespace Apollo_16_Shooter
{
    public class ShooterClass
    {
        public enum Bullets
        {
            linear = 0,
            circular,
            homing,
            area,
            lightSaber,
        }
        public static int NUMBER_TYPE_BULLETS = Enum.GetNames(typeof(Bullets)).Length;

        List<int> ammo = new List<int>(NUMBER_TYPE_BULLETS);
        protected SpriteFont spriteFont;

        public ShooterClass(ContentManager content)
        {

        }

        public SpriteFont SpriteFont
        {
            get { return spriteFont; }
        }

        public void LoadFont(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>(@"Fonts\control");
        }

        public void LoadTextures(ContentManager content) 
        {
            
        }

        public void HandleShooterData(NetIncomingMessage msg)
        {
            var data = new ShooterDataClass(msg);

            this.ammo = data._ammo;
        }

        public void Update(GameTime gameTime)
        {

        }


        public void Draw(SpriteBatch spriteBatch)
        {
            
        }

    }
}
