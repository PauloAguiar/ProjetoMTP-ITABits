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

        public double throttle { get; set; }
        public double speed { get; set; }
        public double angle { get; set; }
        public Vector2 velocity { get; set; }

        public PilotClass()
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
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(spriteFont, "Th:" + throttle.ToString() + " Spd:" + speed.ToString() + " ang:" + angle.ToString() + " vel:" + velocity.ToString() , new Vector2(0, 100), Color.Red);
        }
    }
}
