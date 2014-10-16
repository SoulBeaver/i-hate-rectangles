using System;
using System.Security.Principal;
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using IHateRectangles.Components;
using IHateRectangles.Templates;
using Microsoft.Xna.Framework;

namespace IHateRectangles.Systems
{
    /// <summary>
    /// A pixel-perfect collision system.
    /// </summary>
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class CollisionSystem : TagSystem
    {
        public CollisionSystem()
            : base(BallTemplate.Name)
        { }

        public override void Process(Entity entity)
        {
            var position = entity.GetComponent<PositionComponent>().Position;
            var texture = entity.GetComponent<TextureComponent>().Texture();
            var velocity = entity.GetComponent<VelocityComponent>().Velocity;

            var paddle = EntityWorld.TagManager.GetEntity(PaddleTemplate.Name);
            var paddlePosition = paddle.GetComponent<PositionComponent>().Position;
            var paddleTexture = paddle.GetComponent<TextureComponent>().Texture();

            var debug = String.Format("Ball: {0}, Paddle: {1}", position, paddlePosition);
            Console.Out.WriteLine(debug);

            var ballRectangle = new Rectangle
            {
                Width = texture.Width,
                Height = texture.Height,
                Location = new Point((int) position.X, (int) position.Y)
            };
            var paddleRectangle = new Rectangle
            {
                Width = paddleTexture.Width,
                Height = paddleTexture.Height,
                Location = new Point((int) paddlePosition.X, (int) paddlePosition.Y)
            };

            var minkowskiSum = CalculateMinkowskiSum(ballRectangle, paddleRectangle);
            if (minkowskiSum.IsColliding())
            {
                Console.Out.WriteLine("We are colliding");

                var side = minkowskiSum.CollidingSide();
                switch (side)
                {
                    case Side.Left:
                        entity.GetComponent<VelocityComponent>().Velocity = new Vector2(-velocity.X, velocity.Y);
                        break;

                    case Side.Bottom:
                        entity.GetComponent<VelocityComponent>().Velocity = new Vector2(velocity.X, -velocity.Y);
                        break;

                    case Side.Right:
                        entity.GetComponent<VelocityComponent>().Velocity = new Vector2(-velocity.X, velocity.Y);
                        break;

                    case Side.Top:
                        entity.GetComponent<VelocityComponent>().Velocity = new Vector2(velocity.X, -velocity.Y);
                        break;
                }
            }
        }

        private MinkowskiSum CalculateMinkowskiSum(Rectangle a, Rectangle b)
        {
            float mWidth = (float) 0.5*(a.Width + b.Width);
            float mHeight = (float) 0.5*(a.Height + b.Height);
            float dx = a.Center.X - b.Center.X;
            float dy = a.Center.Y - b.Center.Y;

            return new MinkowskiSum
            {
                Width = mWidth,
                Height = mHeight,
                Dx = dx,
                Dy = dy
            };
        }
    }

    enum Side
    {
        Top, Bottom, Left, Right
    }

    struct MinkowskiSum
    {
        public float Width;
        public float Height;
        public float Dx;
        public float Dy;

        public bool IsColliding()
        {
            return Math.Abs(Dx) <= Width && Math.Abs(Dy) <= Height;
        }

        public Side CollidingSide()
        {
            float wy = Width*Dy;
            float hx = Height*Dx;

            if (wy > hx)
            {
                return wy > -hx ? Side.Top : Side.Left;
            }
            else
            {
                return wy > -hx ? Side.Right : Side.Bottom;
            }
        }
    }
}
