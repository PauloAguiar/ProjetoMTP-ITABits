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
        protected Texture2D aceBar;
        protected Texture2D fuelBar;
        protected Texture2D speedometer;
        protected Texture2D arrow;
        public double throttle { get; set; }
        public double speed { get; set; }
        public double auxspeed;
        public double angle { get; set; }
        public double aceleration;
        public Vector2 velocity { get; set; }
        protected float fuel = 100.0f;
        protected int power = 0;
        //Color acebar_color = new Color();
        Color fuelbar_color = new Color() ;

        public PilotClass(ContentManager content)
        {
            throttle = 0;
            speed = 0;
            auxspeed = 0;
            angle = 0;
            aceleration = 0;
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
            aceBar = content.Load<Texture2D>(@"Sprites\lifeBar");
            fuelBar = content.Load<Texture2D>(@"Sprites\fuelBar");
            speedometer = content.Load<Texture2D>(@"Sprites\speedometer");
            arrow = content.Load<Texture2D>(@"Sprites\Arrow");
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
            aceleration = (speed - auxspeed) / gameTime.ElapsedGameTime.TotalSeconds;
            auxspeed = speed;

            if (power < 100) power += 1;
            //if (power < 50)
            //    acebar_color = Color.Lerp(Color.Green, Color.Yellow, 1.0f);
            //else
            //    acebar_color = Color.Lerp(Color.Yellow, Color.Red, 1.0f);

            if (fuel >= 50)
                fuelbar_color = Color.Lerp(Color.Green, Color.Yellow, 1.0f - fuel/100.0f);
            else
                fuelbar_color = Color.Lerp(Color.Yellow, Color.Red,1.0f - 2*fuel/100.0f);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle race = new Rectangle(0,0,power*aceBar.Width/100,aceBar.Height);
            
            Rectangle rfuel = new Rectangle(0, (int)((fuelBar.Height) * (100.0f-fuel) / 100.0f), fuelBar.Width, (int)((fuelBar.Height) * (fuel) / 100.0f));

            
            spriteBatch.DrawString(spriteFont, "VELOCITY :" + ((int)(1000*speed)).ToString(), new Vector2(245,220+speedometer.Height), Color.Red);
            spriteBatch.DrawString(spriteFont, "ACELERATION :" + aceleration.ToString(), new Vector2(645, 220 + speedometer.Height), Color.Red);
            spriteBatch.DrawString(spriteFont, "POWER :"+ power.ToString() +"%", new Vector2(Globals.SCREEN_RESOLUTION_WIDTH - 400.0f, 50.0f), Color.Red);
            spriteBatch.DrawString(spriteFont, "FUEL :" + fuel, new Vector2(20, 220 + speedometer.Height), Color.Red);
            spriteBatch.Draw(aceBar,new Vector2(Globals.SCREEN_RESOLUTION_WIDTH - 400.0f,0.0f),race,Color.Red);
            spriteBatch.Draw(fuelBar, new Vector2(0.0f, 100 + (fuelBar.Height) * (100.0f - fuel) / 100.0f), rfuel, fuelbar_color);
            spriteBatch.Draw(speedometer, new Vector2(200.0f, 200.0f),null, Color.White);
            spriteBatch.Draw(speedometer, new Vector2(600.0f, 200.0f), null, Color.White);
            spriteBatch.Draw(arrow, new Vector2(200.0f+speedometer.Width/2, 200.0f+speedometer.Height/2), null, Color.White, -2.0f+2.0f*(float)Math.Abs(speed), new Vector2(arrow.Width / 2, arrow.Height), 1.0f, new SpriteEffects(), 0.0f);
            spriteBatch.Draw(arrow, new Vector2(600.0f + speedometer.Width / 2, 200.0f + speedometer.Height / 2), null, Color.White, (float)Math.Abs(100*aceleration), new Vector2(arrow.Width / 2, arrow.Height), 1.0f, new SpriteEffects(), 0.0f);

            
        }

    }
}
