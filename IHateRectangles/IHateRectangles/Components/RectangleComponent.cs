using System.Linq;
using Artemis;
using Artemis.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IHateRectangles.Components
{
    public class RectangleComponent : TextureComponent
    {
        private Texture2D _texture;
        public Rectangle Dimensions { get; private set; }

        public RectangleComponent(Rectangle dimensions)
        {
            Dimensions = dimensions;

            CreateRectangleTexture();
        }

        private void CreateRectangleTexture()
        {
            var graphicsDevice = EntitySystem.BlackBoard.GetEntry<GraphicsDevice>("GraphicsDevice");

            _texture = new Texture2D(graphicsDevice, Dimensions.Width, Dimensions.Height);
            var data = new Color[Dimensions.Width * Dimensions.Height].Select(c => Color.White).ToArray();
            _texture.SetData(data);
        }

        public override Texture2D Texture()
        {
            return _texture;
        }
    }
}
