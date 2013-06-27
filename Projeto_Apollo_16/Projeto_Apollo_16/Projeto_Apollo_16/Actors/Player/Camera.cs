using System;
using Microsoft.Xna.Framework;
using Input = Microsoft.Xna.Framework.Input;

namespace Projeto_Apollo_16
{
    public sealed partial class PlayerClass : ActorClass
    {
        private const float MAX_CAMERA_ZOOM = 0.8f;
        private const float MIN_CAMERA_ZOOM = 0.3f;
        private const int MAX_CAMERA_OFFSET = 300;
        private const float DELTA_ZOOM = 0.005f;
        private const float DELTA_SLIDE = 4.0f;
        private const float INITIAL_CAMERA_ZOOM = 0.55f;
        private const float CAMERA_PROPORTION = (MAX_CAMERA_ZOOM - MIN_CAMERA_ZOOM) / MAX_MAX_THROTTLE;
        private Vector2 cameraOffset;
        private bool isCameraAutomatic = true;
        private float zoom = INITIAL_CAMERA_ZOOM;
        private float actualZoom = INITIAL_CAMERA_ZOOM;
        private const float CAMERA_DELAY = (MAX_CAMERA_ZOOM - MIN_CAMERA_ZOOM) * 250;

        //to provide external access
        public float Zoom
        {
            get { return zoom; }
        }
        public Vector2 CameraPosition
        {
            get { return cameraOffset; }
        }

        void initializeCamera()
        {
            zoom = INITIAL_CAMERA_ZOOM;
            cameraOffset = Vector2.Zero;
        }

        #region zoomControl
        void ZoomIn(float z)
        {
            zoom += z;
            if (zoom > MAX_CAMERA_ZOOM) zoom = MAX_CAMERA_ZOOM;
        }
        void ZoomOut(float z)
        {
            zoom -= z;
            if (zoom < MIN_CAMERA_ZOOM) zoom = MIN_CAMERA_ZOOM;
        }
        void SetZoom(float z)
        {
            zoom = z;
            if (zoom < MIN_CAMERA_ZOOM) zoom = MIN_CAMERA_ZOOM;
            else if (zoom > MAX_CAMERA_ZOOM) zoom = MAX_CAMERA_ZOOM;
        }
        #endregion

        #region slideControl
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

        void UpdateCameraInput(GameTime gameTime)
        {
            UpdateCameraZoom(gameTime);
            UpdateCameraSlide();
        }

        void UpdateCameraZoom(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (isCameraAutomatic)
            {
                zoom = MAX_CAMERA_ZOOM - CAMERA_PROPORTION * Math.Abs(throttle);
                actualZoom += actualZoom * (zoom - actualZoom) / CAMERA_DELAY;
                SetZoom(actualZoom);

                //SetZoom(MAX_CAMERA_ZOOM - CAMERA_PROPORTION * Math.Abs(throttle));
            }
            else
            {
                if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.Q))
                {
                    ZoomIn(DELTA_ZOOM);
                }
                else if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.E))
                {
                    ZoomOut(DELTA_ZOOM);
                }
            }

            zoom = MathHelper.Clamp(zoom, MIN_CAMERA_ZOOM, MAX_CAMERA_ZOOM);
        }

        void UpdateCameraSlide()
        {
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

    }
}
