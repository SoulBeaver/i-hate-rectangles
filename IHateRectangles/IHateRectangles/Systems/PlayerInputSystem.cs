using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;

namespace IHateRectangles.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class PlayerInputSystem : TagSystem
    {
        public PlayerInputSystem()
            : base("Player")
        { }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Process(Entity entity)
        { }
    }
}
