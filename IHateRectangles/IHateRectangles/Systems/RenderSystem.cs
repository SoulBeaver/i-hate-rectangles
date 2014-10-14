using System;
using Artemis;
using Artemis.System;
using Artemis.Attributes;
using Artemis.Manager;
using IHateRectangles.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IHateRectangles.Systems
{
    [ArtemisEntitySystem(ExecutionType = ExecutionType.Synchronous, GameLoopType = GameLoopType.Draw, Layer = 1)]
    public class RenderSystem : EntityProcessingSystem
    {
        private SpriteBatch _spritebatch;

        public RenderSystem()
            : base(Artemis.Aspect.All(typeof (RectangleComponent), typeof (PositionComponent)))
        { }

        public override void LoadContent()
        {
            _spritebatch = BlackBoard.GetEntry<SpriteBatch>("SpriteBatch");
        }

        public override void Process(Entity entity)
        {
            System.Diagnostics.Trace.TraceInformation("Drawing the paddle at layer 1");

            var positionComponent = entity.GetComponent<PositionComponent>();
            var rectangleComponent = entity.GetComponent<RectangleComponent>();

            var positionedRectangle = new Rectangle
            {
                Width = rectangleComponent.Dimensions.Width,
                Height = rectangleComponent.Dimensions.Height,
                Location = new Point(positionComponent.X, positionComponent.Y)
            };

            _spritebatch.Begin();
            _spritebatch.Draw(rectangleComponent.Texture, 
                              positionedRectangle,
                              rectangleComponent.Color);
            _spritebatch.End();
        }
    }
}
