using Artemis;
using Microsoft.Xna.Framework;

namespace IHateRectangles.Components
{
    public class ColorComponent : ComponentPoolable
    {
        public Color Color { get; private set; }

        public ColorComponent(Color color)
        {
            Color = color;
        }
    }
}
