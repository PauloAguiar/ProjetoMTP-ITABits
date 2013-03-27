using Microsoft.Xna.Framework;

namespace Projeto_Apollo_16
{
    public abstract class ActorClass
    {
        protected Point sector;
        protected Vector2 globalPosition;

        public Vector2 GlobalPosition
        {
            get { return globalPosition; }
        }
    }
}
