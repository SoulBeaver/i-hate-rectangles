using Artemis;
using Artemis.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IHateRectangles.Components
{
    public class RectangleComponent : ComponentPoolable
    {
        public readonly Texture2D Texture;
        public readonly Rectangle Dimensions;
        public readonly Color Color;

        public RectangleComponent(Rectangle dimensions, Color color)
        {
            var graphicsDevice = EntitySystem.BlackBoard.GetEntry<GraphicsDevice>("GraphicsDevice");

            Dimensions = dimensions;
            Color = color;
            Texture = new Texture2D(graphicsDevice, 1, 1);
            Texture.SetData(new[] {color});
        }
    }
}
