using Artemis;
using Microsoft.Xna.Framework;

namespace IHateRectangles.Components
{
    public class VelocityComponent : ComponentPoolable
    {
        public Vector2 Velocity { get; set; }

        public VelocityComponent(Vector2 velocity)
        {
            Velocity = velocity;
        }
    }
}
