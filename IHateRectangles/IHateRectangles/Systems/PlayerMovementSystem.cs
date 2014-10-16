using System;
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using IHateRectangles.Components;
using IHateRectangles.Templates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IHateRectangles.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class PlayerMovementSystem : TagSystem
    {
        private KeyboardState _currentKeyboardState;
        private GraphicsDevice _graphicsDevice;
        private int _paddleSpeed;

        public PlayerMovementSystem()
            : base(PaddleTemplate.Name)
        {
            var configuration = BlackBoard.GetEntry<Configuration>("Configuration");
            _graphicsDevice = BlackBoard.GetEntry<GraphicsDevice>("GraphicsDevice");

            _paddleSpeed = configuration.PaddleSpeed;
        }

        public override void Process(Entity entity)
        {
            var positionComponent = entity.GetComponent<PositionComponent>();

            var suggestedDestination = MovePlayer(positionComponent);
            if (IsValidDestination(positionComponent.Position, suggestedDestination, entity.GetComponent<TextureComponent>() as RectangleComponent))
                positionComponent.Position = suggestedDestination;
        }

        private Vector2 MovePlayer(PositionComponent positionComponent)
        {
            _currentKeyboardState = Keyboard.GetState();

            if (_currentKeyboardState.IsKeyDown(Keys.A) || _currentKeyboardState.IsKeyDown(Keys.Left))
                return new Vector2(positionComponent.X - _paddleSpeed, positionComponent.Y);
            else if (_currentKeyboardState.IsKeyDown(Keys.D) || _currentKeyboardState.IsKeyDown(Keys.Right))
                return new Vector2(positionComponent.X + _paddleSpeed, positionComponent.Y);
            else
                return positionComponent.Position;
        }

        private bool IsValidDestination(Vector2 currentPosition, Vector2 suggestedDestination, RectangleComponent rectangleComponent)
        {
            return suggestedDestination == currentPosition ||
                   (suggestedDestination.X >= 0 && suggestedDestination.X <= _graphicsDevice.Viewport.Width - rectangleComponent.Dimensions.Width);
        }
    }
}
