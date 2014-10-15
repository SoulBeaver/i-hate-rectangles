using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using IHateRectangles.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IHateRectangles.Systems
{
    [ArtemisEntitySystem(ExecutionType = ExecutionType.Synchronous, GameLoopType = GameLoopType.Draw, Layer = 0)]
    public class BackgroundRenderSystem : EntityProcessingSystem
    {
        private SpriteBatch _spritebatch;

        public BackgroundRenderSystem()
            : base(typeof(BackgroundComponent))
        { }

        public override void LoadContent()
        {
            _spritebatch = BlackBoard.GetEntry<SpriteBatch>("SpriteBatch");
        }

        public override void Process(Entity entity)
        {
            var backgroundComponent = entity.GetComponent<BackgroundComponent>();

            _spritebatch.Begin();
            _spritebatch.Draw(backgroundComponent.Texture, 
                              Vector2.Zero, 
                              Color.White);
            _spritebatch.End();
        }
    }
}
