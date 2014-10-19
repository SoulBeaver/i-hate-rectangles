using System;
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using Artemis.Utils;
using IHateRectangles.Components;
using IHateRectangles.Templates;
using Microsoft.Xna.Framework;

namespace IHateRectangles.Systems
{
    [ArtemisEntitySystem(ExecutionType = ExecutionType.Synchronous, GameLoopType = GameLoopType.Update, Layer = 1)]
    public class CollisionSystem : TagSystem
    {
        public CollisionSystem()
            : base(BallTemplate.Name)
        { }

        public override void Process(Entity entity)
        {
            CalculateCollisionWithPaddle(entity);
            CalculateCollisionWithBlocks(entity);
        }

        private void CalculateCollisionWithBlocks(Entity ball)
        {
            var position = ball.GetComponent<PositionComponent>().Position;
            var texture = ball.GetComponent<TextureComponent>().Texture();
            
            var ballRectangle = new Rectangle
            {
                Width = texture.Width,
                Height = texture.Height,
                Location = new Point((int)position.X, (int)position.Y)
            };

            Bag<Entity> blocks = EntityWorld.GroupManager.GetEntities(BlockTemplate.Group);
            for (var blockIndex = 0; blockIndex < blocks.Count; blockIndex++)
            {
                var block = blocks.Get(blockIndex);

                var blockPosition = block.GetComponent<PositionComponent>().Position;
                var blockTexture = block.GetComponent<TextureComponent>().Texture();
                var blockRectangle = new Rectangle
                {
                    Width = blockTexture.Width,
                    Height = blockTexture.Height,
                    Location = new Point((int) blockPosition.X, (int) blockPosition.Y)
                };

                var collisionDetector = CalculateMinkowskiSum(ballRectangle, blockRectangle);
                if (collisionDetector.IsColliding())
                {
                    var velocityComponent = ball.GetComponent<VelocityComponent>();
                    var velocity = ball.GetComponent<VelocityComponent>().Velocity;
                    var accelerationComponent = ball.GetComponent<AccelerationComponent>();

                    velocityComponent.Velocity = velocity.X < 0 ? new Vector2(velocity.X - accelerationComponent.Acceleration.X, velocity.Y) 
                                                                : new Vector2(velocity.X + accelerationComponent.Acceleration.X, velocity.Y);
                    velocityComponent.Velocity = velocity.Y < 0 ? new Vector2(velocity.X, velocity.Y - accelerationComponent.Acceleration.Y)
                                                                : new Vector2(velocity.X, velocity.Y + accelerationComponent.Acceleration.Y);
                    velocity = velocityComponent.Velocity;

                    var side = collisionDetector.CollidingSide();
                    switch (side)
                    {
                        case Side.Left:
                        case Side.Right:
                            velocityComponent.Velocity = new Vector2(-velocity.X, velocity.Y);
                            ball.Refresh();
                            break;

                        case Side.Bottom:
                        case Side.Top:
                            velocityComponent.Velocity = new Vector2(velocity.X, -velocity.Y);
                            ball.Refresh();
                            break;
                    }

                    // "Soft" delete a block because the block.Delete method is currently bugged.
                    block.GetComponent<PositionComponent>().Position = new Vector2(-500, -500);
                }
            }
        }

        private void CalculateCollisionWithPaddle(Entity ball)
        {
            var position = ball.GetComponent<PositionComponent>().Position;
            var texture = ball.GetComponent<TextureComponent>().Texture();
            var velocity = ball.GetComponent<VelocityComponent>().Velocity;

            var paddle = EntityWorld.TagManager.GetEntity(PaddleTemplate.Name);
            var paddlePosition = paddle.GetComponent<PositionComponent>().Position;
            var paddleTexture = paddle.GetComponent<TextureComponent>().Texture();

            var ballRectangle = new Rectangle
            {
                Width = texture.Width,
                Height = texture.Height,
                Location = new Point((int)position.X, (int)position.Y)
            };
            var paddleRectangle = new Rectangle
            {
                Width = paddleTexture.Width,
                Height = paddleTexture.Height,
                Location = new Point((int)paddlePosition.X, (int)paddlePosition.Y)
            };

            var collisionDetector = CalculateMinkowskiSum(ballRectangle, paddleRectangle);

            if (collisionDetector.IsColliding())
            {
                var side = collisionDetector.CollidingSide();
                switch (side)
                {
                    case Side.Left:
                    case Side.Right:
                        ball.GetComponent<VelocityComponent>().Velocity = new Vector2(-velocity.X, velocity.Y);
                        break;

                    case Side.Bottom:
                    case Side.Top:
                        ball.GetComponent<VelocityComponent>().Velocity = new Vector2(velocity.X, -velocity.Y);
                        break;
                }
            }
        }

        private MinkowskiSum CalculateMinkowskiSum(Rectangle a, Rectangle b)
        {
            return new MinkowskiSum
            {
                Width = (float) 0.5*(a.Width + b.Width),
                Height = (float) 0.5*(a.Height + b.Height),
                Dx = a.Center.X - b.Center.X,
                Dy = a.Center.Y - b.Center.Y
            };
        }
    }

    internal enum Side { Top, Bottom, Left, Right }

    internal struct MinkowskiSum
    {
        public float Width, Height;
        public float Dx, Dy;

        public bool IsColliding() { return Math.Abs(Dx) <= Width && Math.Abs(Dy) <= Height; }

        public Side CollidingSide()
        {
            float wy = Width*Dy;
            float hx = Height*Dx;

            return (wy > hx) ? 
                        wy > -hx ? Side.Top : Side.Left :
                        wy > -hx ? Side.Right : Side.Bottom;
        }
    }
}
