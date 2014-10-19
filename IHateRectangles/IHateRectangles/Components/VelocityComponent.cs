using Artemis;
using Microsoft.Xna.Framework;

namespace IHateRectangles.Components
{
    public class VelocityComponent : ComponentPoolable
    {
        public Vector2 Velocity { get; set; }

        public VelocityComponent(int horizontalSpeed, int verticalSpeed)
            : this(new Vector2(horizontalSpeed, verticalSpeed))
        { }

        public VelocityComponent(Vector2 velocity)
        {
            Velocity = velocity;
        }
    }
}
