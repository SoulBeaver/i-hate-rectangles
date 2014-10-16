using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using IHateRectangles.Components;
using IHateRectangles.Templates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IHateRectangles.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class BallMovementSystem : TagSystem
    {
        public BallMovementSystem()
            : base(BallTemplate.Name)
        { }

        public override void Process(Entity entity)
        {
            var graphicsDevice = BlackBoard.GetEntry<GraphicsDevice>("GraphicsDevice");

            var positionComponent = entity.GetComponent<PositionComponent>();
            var velocityComponent = entity.GetComponent<VelocityComponent>();
            var circleComponent = entity.GetComponent<TextureComponent>() as CircleComponent;

            var suggestedDestination = positionComponent.Position + velocityComponent.Velocity;
            if (suggestedDestination.X < 0)
            {
                suggestedDestination.X = 0;
                velocityComponent.Velocity = new Vector2(-velocityComponent.Velocity.X, velocityComponent.Velocity.Y);
            }
            else if (suggestedDestination.X > graphicsDevice.Viewport.Width - circleComponent.Radius*2)
            {
                suggestedDestination.X = graphicsDevice.Viewport.Width - circleComponent.Radius*2;
                velocityComponent.Velocity = new Vector2(-velocityComponent.Velocity.X, velocityComponent.Velocity.Y);
            }
            else if (suggestedDestination.Y < 0)
            {
                suggestedDestination.Y = 0;
                velocityComponent.Velocity = new Vector2(velocityComponent.Velocity.X, -velocityComponent.Velocity.Y);
            }
            else if (suggestedDestination.Y > graphicsDevice.Viewport.Height - circleComponent.Radius*2)
            {
                suggestedDestination.Y = graphicsDevice.Viewport.Height - circleComponent.Radius*2;
                velocityComponent.Velocity = new Vector2(velocityComponent.Velocity.X, -velocityComponent.Velocity.Y);
            }
            else
            {
                positionComponent.Position = suggestedDestination;
            }
        }
    }
}
