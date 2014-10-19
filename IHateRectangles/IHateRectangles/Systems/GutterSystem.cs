using System;
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
    [ArtemisEntitySystem(ExecutionType = ExecutionType.Synchronous, GameLoopType = GameLoopType.Update, Layer = 2)]
    public class GutterSystem : TagSystem
    {
        public GutterSystem()
            : base(BallTemplate.Name)
        { }

        public override void Process(Entity ball)
        {
            var viewport = BlackBoard.GetEntry<GraphicsDevice>("GraphicsDevice").Viewport;
            var positionComponent = ball.GetComponent<PositionComponent>();

            if (positionComponent.Y > viewport.Height)
            {
                ball.GetComponent<VelocityComponent>().Velocity = Vector2.Zero;
                ball.GetComponent<AccelerationComponent>().Acceleration = Vector2.Zero;

                Console.Out.WriteLine("The game has ended.");
            }
        }
    }
}
