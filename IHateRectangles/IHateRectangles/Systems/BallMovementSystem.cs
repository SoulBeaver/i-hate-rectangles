using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using IHateRectangles.Templates;

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

        }
    }
}
