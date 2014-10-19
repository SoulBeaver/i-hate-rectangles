using System;
using Artemis;
using Artemis.Attributes;
using Artemis.Interface;
using Artemis.System;
using IHateRectangles.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IHateRectangles.Templates
{
    [ArtemisEntityTemplate(Name)]
    class PaddleTemplate : IEntityTemplate
    {
        public const String Name = "Paddle";

        public Entity BuildEntity(Entity entity, EntityWorld entityWorld, params object[] args)
        {
            var configuration = EntitySystem.BlackBoard.GetEntry<Configuration>("Configuration");
            var graphicsDevice = EntitySystem.BlackBoard.GetEntry<GraphicsDevice>("GraphicsDevice");
            var paddleDimensions = new Rectangle
            {
                Width = configuration.PaddleWidth,
                Height = configuration.PaddleHeight,
                Location = new Point(0, 0)
            };

            entity.AddComponent<TextureComponent>(new RectangleComponent(paddleDimensions));
            entity.AddComponent(new ColorComponent(configuration.PaddleColor));
            entity.AddComponent(new PositionComponent((graphicsDevice.Viewport.Width - configuration.PaddleWidth) / 2,
                                                      graphicsDevice.Viewport.Height - configuration.PaddleDistanceFromGutter));
            entity.AddComponent(new VelocityComponent(configuration.PaddleSpeed, 0));
            entity.Tag = Name;

            return entity;
        }
    }
}
