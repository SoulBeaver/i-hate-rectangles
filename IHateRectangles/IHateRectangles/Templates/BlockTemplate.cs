using System;
using Artemis;
using Artemis.Attributes;
using Artemis.System;
using IHateRectangles.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IHateRectangles.Templates
{
    [ArtemisEntityTemplate(Name)]
    public class BlockTemplate
    {
        public const String Name = "Block";

        public Entity BuildEntity(Entity entity, EntityWorld entityWorld, params object[] args)
        {
            var configuration = EntitySystem.BlackBoard.GetEntry<Configuration>("Configuration");
            var blockDimensions = new Rectangle
            {
                Width = configuration.BlockWidth,
                Height = configuration.BlockHeight,
                Location = new Point(0, 0)
            };

            entity.AddComponent<TextureComponent>(new RectangleComponent(blockDimensions));
            entity.AddComponent(new ColorComponent(configuration.BlockColor));
            entity.AddComponent(new PositionComponent());
            entity.Group = "Blocks";

            return entity;
        }
    }
}
