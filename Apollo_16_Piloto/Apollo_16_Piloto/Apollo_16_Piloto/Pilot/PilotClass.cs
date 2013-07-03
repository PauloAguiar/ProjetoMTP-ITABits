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
        protected Texture2D fuelBar;
        protected Texture2D speedometer;
        protected Texture2D arrow;
        public double throttle { get; set; }
        public double speed { get; set; }
        public double angle { get; set; }
        public Vector2 velocity { get; set; }
        protected int life = 150;
        protected float fuel = 100.0f;
        Color lifebar_color = new Color();
        Color fuelbar_color = new Color() ;

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
            //lifeBar = content.Load<Texture2D>(@"Sprites\lifeBar");
            fuelBar = content.Load<Texture2D>(@"Sprites\fuelBar");
            //speedometer = content.Load<Texture2D>(@"Sprites\speedometer");
            //arrow = content.Load<Texture2D>(@"Sprites\Arrow");
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
            if (fuel > 0) fuel = fuel - 0.1f;
            if ( life >= 100 )
                lifebar_color = Color.Blue;
            else if ( life >= 80 )
                lifebar_color = Color.Green;
            else if ( life >= 65 )
                lifebar_color = Color.Yellow;
            else if ( life >= 50 )
                lifebar_color = Color.Orange;
            else
                lifebar_color = Color.Red;

            if (fuel >= 50)
                fuelbar_color = Color.Lerp(Color.Green, Color.Yellow, 1.0f - fuel/100.0f);
            else
                fuelbar_color = Color.Lerp(Color.Yellow, Color.Red,1.0f - fuel/100.0f);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Rectangle rlife = new Rectangle(0,0,life*lifeBar.Width/150,lifeBar.Height);
            //Rectangle rfuel = new Rectangle((int)(fuelBar.Width/2.0f + 500.0f),500 - (int)fuel/2, fuelBar.Width, (int)fuel*fuelBar.Height/100);
            Rectangle rfuel = new Rectangle(0, (int)((fuelBar.Height) * (fuel) / 100.0f), fuelBar.Width, (int)((fuelBar.Height) * (fuel) / 100.0f));
            
            
            spriteBatch.DrawString(spriteFont, "Th:" + throttle.ToString() + " Spd:" + speed.ToString() + " ang:" + angle.ToString() + " vel:" + velocity.ToString() , new Vector2(0, 100), Color.Red);
            //spriteBatch.Draw(lifeBar,new Vector2(Globals.SCREEN_RESOLUTION_WIDTH-lifebar.Width,0.0f),rlife,lifebar_color);
            spriteBatch.Draw(fuelBar, new Vector2(0.0f, 0.0f),rfuel, fuelbar_color);
            //spriteBatch.Draw(speedometer, new Vector2(100.0f, 100.0f),null, Color.White);
            //spriteBatch.Draw(speedometer, new Vector2(400.0f, 100.0f), null, Color.White);
            //spriteBatch.Draw(arrow, new Vector2(100.0f+speedometer.Width/2, 100.0f+speedometer.Height/2), null, Color.White, (float)Math.Abs(speed), new Vector2(arrow.Width / 2, arrow.Height), 1.0f, new SpriteEffects(), 0.0f);
            //spriteBatch.Draw(arrow, new Vector2(400.0f + speedometer.Width / 2, 100.0f + speedometer.Height / 2), null, Color.White, (float)Math.Abs(speed), new Vector2(arrow.Width / 2, arrow.Height), 1.0f, new SpriteEffects(), 0.0f);
            
        }
    }
}
