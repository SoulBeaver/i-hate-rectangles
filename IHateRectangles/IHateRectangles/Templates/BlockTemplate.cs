using System;
using Artemis;
using Artemis.Attributes;
using Artemis.Interface;
using IHateRectangles.Components;
using Microsoft.Xna.Framework;

namespace IHateRectangles.Templates
{
    [ArtemisEntityTemplate(Name)]
    public class BlockTemplate : IEntityTemplate
    {
        public const String Name = "Block";
        public const String Group = "Blocks";

        public Entity BuildEntity(Entity entity, EntityWorld entityWorld, params object[] args)
        {
            var blockWidth = (int) args[0];
            var blockHeight = (int) args[1];
            var blockColor = (Color) args[2];
            var blockY = (int) args[3];
            var blockX = (int) args[4];

            var blockDimensions = new Rectangle
            {
                Width = blockWidth,
                Height = blockHeight,
                Location = new Point(0, 0)
            };

            entity.AddComponent<TextureComponent>(new RectangleComponent(blockDimensions));
            entity.AddComponent(new ColorComponent(blockColor));
            entity.AddComponent(new PositionComponent(blockX, blockY));
            entity.Group = Group;

            return entity;
        }
    }
}
