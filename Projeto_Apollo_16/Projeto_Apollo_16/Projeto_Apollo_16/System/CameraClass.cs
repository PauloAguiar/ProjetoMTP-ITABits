using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Projeto_Apollo_16
{
    public class CameraClass
    {

        Vector2 position { get; set; }
        float rotation { get; set; }
        float zoom { get; set; }
        Vector2 origin;

        public CameraClass(Viewport viewport)
        {
            zoom = 1.0f;
            origin = new Vector2(viewport.Width / 2, viewport.Height / 2);
            position = Vector2.Zero;
        }

        public float Zoom
        {
            get { return zoom; }
            set { zoom = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public void LookAt(Vector2 position)
        {
            this.position = position - origin - this.position;
        }

        public Matrix TransformMatrix
        {
            get
            {
                return  Matrix.CreateTranslation(new Vector3(-position+origin, 0.0f)) * // The +origin offset makes the center of the viewport as the reference
                        Matrix.CreateTranslation(new Vector3(-origin, 0.0f)) *
                        Matrix.CreateRotationZ(rotation) * 
                        Matrix.CreateScale(Zoom) *
                        Matrix.CreateTranslation(new Vector3(origin, 0.0f));
            }
        }
    }
}
