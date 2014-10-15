using System;
using System.Linq;
using Artemis;
using Artemis.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IHateRectangles.Components
{
    public class CircleComponent : TextureComponent
    {
        private Texture2D _texture;
        public double Radius { get; private set; }

        public CircleComponent(double radius)
        {
            Radius = radius;

            CreateCircleTexture();
        }

        private void CreateCircleTexture()
        {
            var graphicsDevice = EntitySystem.BlackBoard.GetEntry<GraphicsDevice>("GraphicsDevice");
            var paddedRadius = (int) Radius * 2 + 2;

            _texture = new Texture2D(graphicsDevice, paddedRadius, paddedRadius);
            var data = new Color[paddedRadius*paddedRadius].Select(c => Color.Transparent).ToArray();

            var angleStep = 1.0f/Radius;
            for (double angle = 0.0; angle < Math.PI*2; angle += angleStep)
            {
                int x = (int) Math.Round(Radius + Radius*Math.Cos(angle));
                int y = (int) Math.Round(Radius + Radius*Math.Sin(angle));

                data[y*paddedRadius + x + 1] = Color.White;
            }

            _texture.SetData(data);
        }

        public override Texture2D Texture()
        {
            return _texture;
        }
    }
}
