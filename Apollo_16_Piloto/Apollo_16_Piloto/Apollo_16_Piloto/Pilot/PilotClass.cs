using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Lidgren.Network;

namespace Apollo_16_Piloto
{
    public class PilotClass
    {
        protected SpriteFont spriteFont;
        protected Texture2D lifeBar;
        public double throttle { get; set; }
        public double speed { get; set; }
        public double angle { get; set; }
        public Vector2 velocity { get; set; }
        int i =400;

        public PilotClass(ContentManager content)
        {
            throttle = 0;
            speed = 0;
            angle = 0;
            velocity = Vector2.Zero;
        }

        public SpriteFont SpriteFont
        {
            get { return spriteFont; }
        }

        public void LoadFont(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>(@"Fonts\ControlFont");
        }

        public void LoadTextures(ContentManager content) 
        {
            lifeBar = content.Load<Texture2D>(@"Screens\lifeBar");
        }

        public void HandlePilotData(NetIncomingMessage msg)
        {
            var data = new PilotDataClass(msg);

            this.throttle = data._throttle;
            this.speed = data._speed;
            this.angle = data._angle;
            this.velocity = data._velocity;
        }

        public void Update(GameTime gameTime)
        {
            //atualiza o i dependendo do life i = 4*life;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            Rectangle r = new Rectangle(0,0,i-100,20);
            spriteBatch.DrawString(spriteFont, "Th:" + throttle.ToString() + " Spd:" + speed.ToString() + " ang:" + angle.ToString() + " vel:" + velocity.ToString() , new Vector2(0, 100), Color.Red);
            if ( lifeBar != null )    
                spriteBatch.Draw(lifeBar,new Vector2(500.0f,500.0f),r,Color.White);
        }
    }
}
