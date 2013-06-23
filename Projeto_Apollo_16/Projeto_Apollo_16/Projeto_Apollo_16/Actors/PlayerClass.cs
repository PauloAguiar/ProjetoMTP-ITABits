using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Input = Microsoft.Xna.Framework.Input;
using SlimDX.DirectInput;

namespace Projeto_Apollo_16
{
    public sealed class PlayerClass : ActorClass
    {
   
        #region constants
        //Keyboard Control
        private const float DELTA_THETA = (float)Math.PI / 400;
        private const float EPSILON_SPEED = 0.1f;
        private const float EPSILON_THROTTLE = 0.0001f;
        private const float DELTA_THROTTLE_UP = 0.0001f;
        private const float DELTA_THROTTLE_DOWN = 0.000005f;
        
        //Camera Control
        private const float MAX_CAMERA_ZOOM = 0.4f;
        private const float MIN_CAMERA_ZOOM = 0.1f;
        private const int MAX_CAMERA_OFFSET = 300;
        private const float DELTA_ZOOM = 0.005f;
        private const float DELTA_SLIDE = 4.0f;
        private const float INITIAL_CAMERA_ZOOM = 0.7f;

        //Joystick Control
        private const float MAX_SPEED = 2.0f;
        private const float MIN_SPEED = -0.75f;
        private const float CAMERA_PROPORTION = (MAX_CAMERA_ZOOM - MIN_CAMERA_ZOOM) / MAX_MAX_THROTTLE;
        private const float MAX_SIDE_ACCELERATION = 0.007f;
        private const float MAX_SIDE_SPEED = 0.03f;
        private const float MAX_MAX_THROTTLE = 0.2f;
        private const float MIN_MIN_THROTTLE = -0.1f;

        
        const float maxAngle = (float)MathHelper.PiOver4;
        private float cameraZoom;
        private Vector2 cameraOffset;
        #endregion

        private const double dTheta = Math.PI / 600;
        public float throttle { get; private set; }
        public float Speed { get; private set; }
        public float Angle { get; private set; }
        public Vector2 Velocity { get; private set; }

        float sideAcceleration;
        public Vector2 SideVelocity { get; private set; }
        public float SideSpeed { get; private set; }

        //dependem do throttle (alavanca do joystick)
        private float maxThrottle = 0.4f;
        private float minThrottle = -0.2f;

        //dpende do joystickRange
        float maxRotationZ;
        
        //Items and stuff
        public int Life { get; private set; }
        const int maxLife = 300;
        const int minLife = 0;

        public float Fuel { get; private set; }
        const int maxFuel = 100;
        const int minFuel = 0;


        //items: shield, health
        public int[] inventory = new int[2];
        //0 -> shield
        //1 -> health

        public enum Bullets 
        { 
            linear = 0,
            circular,
            homing,
        }
        public Bullets bullets = Bullets.linear;
        public const int numberBullets = 3;

        JoystickState joystickState = new JoystickState();
        public int joystickRange = 400;

        Texture2D turboTexture;
        Vector2 turboPosition;
        Vector2 turboBackPosition;
        float turboAngle;

        Texture2D naveRight;
        Texture2D gun;
        Vector2 gunPosition;
        public JoystickState state { get; set; }

        public PlayerClass(Vector2 position, ContentManager content)
        {
            globalPosition = position;
            Speed = 0;
            throttle = 0;
            Speed = 0.001f;
            Angle = 0;
            Velocity = Vector2.Zero;
            cameraZoom = INITIAL_CAMERA_ZOOM;
            cameraOffset = Vector2.Zero;
            Life = 100;
            Fuel = 100;
            
            this.LoadFont(content);
            this.LoadTexture(content);
        }

        #region cameraControl
        public float Zoom
        {
            get { return cameraZoom; }
        }

        public Vector2 CameraPosition
        {
            get { return cameraOffset; }
        }

        void ZoomIn(float z)
        {
            cameraZoom += z;
            if (cameraZoom > MAX_CAMERA_ZOOM) cameraZoom = MAX_CAMERA_ZOOM;
        }

        void ZoomOut(float z)
        {
            cameraZoom -= z;
            if (cameraZoom < MIN_CAMERA_ZOOM) cameraZoom = MIN_CAMERA_ZOOM;
        }

        void SetZoom(float z)
        {
            cameraZoom = z;
            if (cameraZoom < MIN_CAMERA_ZOOM) cameraZoom = MIN_CAMERA_ZOOM;
            else if (cameraZoom > MAX_CAMERA_ZOOM) cameraZoom = MAX_CAMERA_ZOOM;
        }

        void SlideTop(float a)
        {
            cameraOffset.Y -= a;
            if (cameraOffset.Y < -MAX_CAMERA_OFFSET) cameraOffset.Y = -MAX_CAMERA_OFFSET;
        }
        void SlideDown(float a)
        {
            cameraOffset.Y += a;
            if (cameraOffset.Y > MAX_CAMERA_OFFSET) cameraOffset.Y = MAX_CAMERA_OFFSET;
        }
        void SlideLeft(float a)
        {
            cameraOffset.X -= a;
            if (cameraOffset.X < -MAX_CAMERA_OFFSET) cameraOffset.X = -MAX_CAMERA_OFFSET;
        }
        void SlideRight(float a)
        {
            cameraOffset.X += a;
            if (cameraOffset.X > MAX_CAMERA_OFFSET) cameraOffset.X = MAX_CAMERA_OFFSET;
        }

        #endregion

        #region loadContent
        public override void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"Sprites\Nave\nave02");
            turboTexture = content.Load<Texture2D>(@"Sprites\Nave\hadouken");
            naveRight = content.Load<Texture2D>(@"Sprites\Nave\naveright");
            gun = content.Load<Texture2D>(@"Sprites\Nave\gun");
        }

        public override void LoadFont(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>(@"Fonts\ActorInfo");
        }
        #endregion

        #region update


        public override void Update(GameTime gameTime)
        {

        }

        public void Update(GameTime gameTime, JoystickState joystickState, int joystickRange)
        {
            double dt = gameTime.ElapsedGameTime.TotalMilliseconds;

            this.joystickState = joystickState;
            this.joystickRange = joystickRange;
            maxRotationZ = joystickRange / 25.0f;

            UpdateInput(gameTime);

            UpdatePosition(dt);

            while (inventory[1] > 0)
            {
                Life += 10;
                inventory[1]--;
            }

            Life = (int)MathHelper.Clamp(Life, minLife, maxLife);

            Fuel -= Math.Abs(throttle) * (float)dt;
            Fuel = MathHelper.Clamp(Fuel, minFuel, maxFuel);

        }

        private void UpdatePosition(double dt)
        {
            throttle = MathHelper.Clamp(throttle, minThrottle, maxThrottle);
            Speed += throttle;
            Speed = MathHelper.Clamp(Speed, MIN_SPEED, MAX_SPEED);

            Velocity = MathFunctions.AngleToVector(Angle);

            Velocity.Normalize();
            SideVelocity = new Vector2(-Velocity.Y, Velocity.X);
            
            turboPosition =  - texture.Height/2*Velocity;
            turboBackPosition =  texture.Height / 2 * Velocity;
            gunPosition = 0 * Velocity;

            turboAngle = Angle + MathHelper.PiOver2;

            sideAcceleration *= Math.Abs(throttle);
            
            sideAcceleration = MathHelper.Clamp(sideAcceleration, -MAX_SIDE_ACCELERATION, MAX_SIDE_ACCELERATION);
            SideSpeed += sideAcceleration * (float)dt;
            SideSpeed = MathHelper.Clamp(SideSpeed, -MAX_SIDE_SPEED, MAX_SIDE_SPEED);
            SideVelocity *= SideSpeed * (float)dt +0.5f * sideAcceleration * (float)(dt * dt);
            SideSpeed /= 2.0f;

            Velocity = Velocity * Speed;
            /*if (Math.Abs(Speed) <= epsilonSpeed)
            {
                Velocity = Vector2.Zero;
            }*/
            
            globalPosition += Velocity * (float)dt;
            globalPosition += SideVelocity * (float)dt;
            turboPosition += globalPosition;
            turboBackPosition += globalPosition;
            gunPosition += globalPosition;
        }
        
        private void UpdateInput(GameTime gameTime)
        {
            UpdateCameraInput();

            UpdatePositionInput();

            if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.D0) && inventory[0] > 0)
            {
                Life += 40;
                inventory[0]--;
            }
        }

        private void UpdatePositionInput()
        {
            maxThrottle = MAX_MAX_THROTTLE * (-joystickState.Z + joystickRange) / joystickRange;
            minThrottle = MIN_MIN_THROTTLE * (-joystickState.Z + joystickRange) / joystickRange;


            velocityInput();
            angleInput();

            if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.R))
            {
                globalPosition = new Vector2(0);
            }


            sideAcceleration = joystickState.X * MAX_SIDE_ACCELERATION / joystickRange;

        }

        private void angleInput()
        {
            
            Angle += 1/100.0f * 1/4.0f*(joystickState.RotationZ * maxAngle / maxRotationZ)/(float)MathHelper.TwoPi;


            if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.Left))
            {
                Angle -= DELTA_THETA;
                if (Angle < -MathHelper.TwoPi)
                {
                    Angle += MathHelper.TwoPi;
                }
            }
            else if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.Right))
            {
                Angle += DELTA_THETA;
                if (Angle > MathHelper.TwoPi)
                {
                    Angle -= MathHelper.TwoPi;
                }

            }
            
        }

        private void velocityInput()
        {
            if (joystickState.Y < 0)
            {
                throttle = -joystickState.Y * maxThrottle / joystickRange;
            }
            else if (joystickState.Y > 0)
            {
                throttle = joystickState.Y * minThrottle / joystickRange;
            }

            
            if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.Up))
            {
                throttle += DELTA_THROTTLE_UP;
            }
            else if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.Down))
            {
                throttle -= DELTA_THROTTLE_DOWN;
            }
            else
            {
                throttle *= 1.0f / 2;
                if (Math.Abs(throttle) <= EPSILON_THROTTLE)
                {
                    throttle = 0;
                }
            }
            
        }

        private void UpdateCameraInput()
        {
            SetZoom(MAX_CAMERA_ZOOM - CAMERA_PROPORTION * Math.Abs(throttle));

            
            if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.Q))
            {
                ZoomIn(DELTA_ZOOM);
            }
            else if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.E))
            {
                ZoomOut(DELTA_ZOOM);
            }

            if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.W))
            {
                SlideTop(DELTA_SLIDE);
            }
            else if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.A))
            {
                SlideLeft(DELTA_SLIDE);
            }
            else if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.S))
            {
                SlideDown(DELTA_SLIDE);
            }
            else if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.D))
            {
                SlideRight(DELTA_SLIDE);
            }
            
        }
        #endregion

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (SideSpeed == 0)
            {
                spriteBatch.Draw(texture, GlobalPosition, null, Color.White, (float)Angle, new Vector2(texture.Width / 2, texture.Height / 2), 1.0f, SpriteEffects.None, Globals.PLAYER_LAYER);
            }
            else if (SideSpeed < 0)
            {
                spriteBatch.Draw(naveRight, GlobalPosition, null, Color.White, (float)Angle, new Vector2(texture.Width / 2, texture.Height / 2), 1.0f, SpriteEffects.None, Globals.PLAYER_LAYER);
            }
            else
            {
                spriteBatch.Draw(naveRight, GlobalPosition, null, Color.White, (float)Angle, new Vector2(texture.Width / 2, texture.Height / 2), 1.0f, SpriteEffects.FlipHorizontally, Globals.PLAYER_LAYER);
            }
            
            if (Speed > 0)
            {
                spriteBatch.Draw(turboTexture, turboPosition, null, Color.White, (float)turboAngle, new Vector2(turboTexture.Width / 2, turboTexture.Height / 2), 1.0f, SpriteEffects.None, Globals.PLAYER_LAYER);
            }
            else if (Speed < 0)
            {
                spriteBatch.Draw(turboTexture, turboBackPosition, null, Color.White, (float)turboAngle, new Vector2(turboTexture.Width / 2, turboTexture.Height / 2), 1.0f, SpriteEffects.None, Globals.PLAYER_LAYER);
            }

            spriteBatch.Draw(gun, gunPosition, null, Color.White, (float)Angle, new Vector2(gun.Width / 2, gun.Height / 2), 1.0f, SpriteEffects.None, Globals.PLAYER_LAYER-0.1f);


            spriteBatch.DrawString(spriteFont, "Life = " + Life.ToString(), globalPosition, Color.White);
            spriteBatch.DrawString(spriteFont, "Fuel = " + Fuel.ToString(), globalPosition+new Vector2(0, 50), Color.White);
            
            
        }

    }
}
