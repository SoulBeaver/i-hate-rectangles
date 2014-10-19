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
    [ArtemisEntitySystem(ExecutionType = ExecutionType.Synchronous, GameLoopType = GameLoopType.Update)]
    public class PlayerMovementSystem : TagSystem
    {
        private KeyboardState _currentKeyboardState;
        private Viewport _viewport;

        public PlayerMovementSystem()
            : base(PaddleTemplate.Name)
        {
            _viewport = BlackBoard.GetEntry<GraphicsDevice>("GraphicsDevice").Viewport;
        }

        public override void Process(Entity paddle)
        {
            var suggestedDestination = MovePlayer(paddle);

            var positionComponent = paddle.GetComponent<PositionComponent>();
            if (IsValidDestination(positionComponent.Position, suggestedDestination, paddle.GetComponent<TextureComponent>() as RectangleComponent))
                positionComponent.Position = suggestedDestination;
        }

        private Vector2 MovePlayer(Entity paddle)
        {
            var positionComponent = paddle.GetComponent<PositionComponent>();
            var paddleSpeed = paddle.GetComponent<VelocityComponent>().Velocity;
            _currentKeyboardState = Keyboard.GetState();

            if (_currentKeyboardState.IsKeyDown(Keys.A) || _currentKeyboardState.IsKeyDown(Keys.Left))
                return new Vector2(positionComponent.X - paddleSpeed.X, positionComponent.Y);
            else if (_currentKeyboardState.IsKeyDown(Keys.D) || _currentKeyboardState.IsKeyDown(Keys.Right))
                return new Vector2(positionComponent.X + paddleSpeed.X, positionComponent.Y);
            else
                return positionComponent.Position;
        }

        private bool IsValidDestination(Vector2 currentPosition, Vector2 suggestedDestination, RectangleComponent rectangleComponent)
        {
            return suggestedDestination == currentPosition ||
                   (suggestedDestination.X >= 0 && suggestedDestination.X <= _viewport.Width - rectangleComponent.Dimensions.Width);
        }
    }
}
