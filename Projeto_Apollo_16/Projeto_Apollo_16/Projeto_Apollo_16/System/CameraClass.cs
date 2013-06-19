using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Projeto_Apollo_16
{
    public class CameraClass
    {
        Vector2 position { get; set; }
        Vector2 offset { get; set; }
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

        public Vector2 Offset
        {
            get { return offset; }
            set { offset = value; }
        }
        public Matrix TransformMatrix
        {
            get
            {
                return Matrix.CreateTranslation(new Vector3(origin - position - offset, 0.0f)) * // The +origin offset makes the center of the viewport as the reference
                        Matrix.CreateTranslation(new Vector3(-origin, 0.0f)) *
                        Matrix.CreateRotationZ(rotation) * 
                        Matrix.CreateScale(Zoom) *
                        Matrix.CreateTranslation(new Vector3(origin, 0.0f));
            }
        }
    }
}
