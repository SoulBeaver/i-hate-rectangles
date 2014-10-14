using Artemis;
using Microsoft.Xna.Framework;

namespace IHateRectangles.Components
{
    public class PositionComponent : ComponentPoolable
    {
        public Vector2 Position { get; set; }

        public int X { get { return (int) Position.X; } }
        public int Y { get { return (int) Position.Y; } }

        public PositionComponent()
            : this(Vector2.Zero)
        { }

        public PositionComponent(int x, int y)
            : this(new Vector2(x, y))
        { }

        public PositionComponent(Vector2 position)
        {
            this.Position = position;
        }
    }
}
