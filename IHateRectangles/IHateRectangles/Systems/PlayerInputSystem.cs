using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using IHateRectangles.Components;
using IHateRectangles.Templates;
using Microsoft.Xna.Framework.Input;

namespace IHateRectangles.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class PlayerInputSystem : TagSystem
    {
        private KeyboardState _currentKeyboardState;

        public PlayerInputSystem()
            : base(PaddleTemplate.Name)
        { }

        public override void Process(Entity entity)
        {
            _currentKeyboardState = Keyboard.GetState();

            var positionComponent = entity.GetComponent<PositionComponent>();

            if (_currentKeyboardState.IsKeyDown(Keys.A) || _currentKeyboardState.IsKeyDown(Keys.Left))
                positionComponent.X -= 5;
            else if (_currentKeyboardState.IsKeyDown(Keys.D) || _currentKeyboardState.IsKeyDown(Keys.Right))
                positionComponent.X += 5;
        }
    }
}
