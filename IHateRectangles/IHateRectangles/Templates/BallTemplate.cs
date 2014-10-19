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
    public class BallTemplate : IEntityTemplate
    {
        public const String Name = "Ball";

        public Entity BuildEntity(Entity entity, EntityWorld entityWorld, params object[] args)
        {
            var configuration = EntitySystem.BlackBoard.GetEntry<Configuration>("Configuration");
            var graphicsDevice = EntitySystem.BlackBoard.GetEntry<GraphicsDevice>("GraphicsDevice");

            entity.AddComponent<TextureComponent>(new CircleComponent(configuration.BallRadius));
            entity.AddComponent(new ColorComponent(configuration.BallColor));
            entity.AddComponent(new PositionComponent((graphicsDevice.Viewport.Width - configuration.PaddleWidth) / 2,
                                                      graphicsDevice.Viewport.Height - configuration.PaddleDistanceFromGutter - (int)configuration.BallRadius * 2 - 1));
            entity.AddComponent(new VelocityComponent((Vector2) args[0]));
            entity.AddComponent(new AccelerationComponent(configuration.BallAcceleration.X, configuration.BallAcceleration.Y));
            entity.Tag = Name;

            return entity;
        }
    }
}
