﻿using System;
using Microsoft.Xna.Framework;
using Input = Microsoft.Xna.Framework.Input;

namespace Projeto_Apollo_16
{
    public sealed partial class PlayerClass : ActorClass
    {
        private const float MAX_CAMERA_ZOOM = 0.4f;
        private const float MIN_CAMERA_ZOOM = 0.1f;
        private const int MAX_CAMERA_OFFSET = 300;
        private const float DELTA_ZOOM = 0.005f;
        private const float DELTA_SLIDE = 4.0f;
        private const float INITIAL_CAMERA_ZOOM = 0.7f;

        private float cameraZoom;
        private Vector2 cameraOffset;

        void initializeCamera()
        {
            cameraZoom = INITIAL_CAMERA_ZOOM;
            cameraOffset = Vector2.Zero;
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

        void UpdateCameraInput()
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
    }
}