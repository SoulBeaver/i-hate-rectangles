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
            : base(Artemis.Aspect.All(typeof (TextureComponent)))
        { }

        public override void LoadContent()
        {
            _spritebatch = BlackBoard.GetEntry<SpriteBatch>("SpriteBatch");
        }

        public override void Process(Entity entity)
        {
            var positionComponent = entity.GetComponent<PositionComponent>();
            var textureComponent = entity.GetComponent<TextureComponent>();
            var colorComponent = entity.GetComponent<ColorComponent>();

            _spritebatch.Begin();
            _spritebatch.Draw(textureComponent.Texture(), 
                              positionComponent.Position, 
                              colorComponent.Color);
            _spritebatch.End();
        }
    }
}
