using Artemis;
using Microsoft.Xna.Framework;

namespace IHateRectangles.Components
{
    public class AccelerationComponent : ComponentPoolable
    {
        public Vector2 Acceleration { get; private set; }

        public AccelerationComponent(int horizontalAcceleration, int verticalAcceleration)
            : this(new Vector2(horizontalAcceleration, verticalAcceleration))
        { }

        public AccelerationComponent(Vector2 acceleration)
        {
            Acceleration = acceleration;
        }
    }
}
