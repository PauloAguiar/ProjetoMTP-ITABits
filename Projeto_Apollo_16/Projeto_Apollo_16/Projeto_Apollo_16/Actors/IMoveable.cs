using Microsoft.Xna.Framework;

namespace Projeto_Apollo_16
{
    interface IMoveable
    {
        double Speed { get; }
        double Angle { get; }
        Vector2 Velocity { get;}

    }
}
