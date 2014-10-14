using Artemis;
using Microsoft.Xna.Framework.Graphics;

namespace IHateRectangles.Components
{
    public class BackgroundComponent : ComponentPoolable
    {
        public Texture2D Texture { get; set; }

        public BackgroundComponent(Texture2D texture)
        {
            Texture = texture;
        }
    }
}
