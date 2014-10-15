using Microsoft.Xna.Framework;

namespace IHateRectangles
{
    public struct Configuration
    {
        public Vector2 InitialBallVelocity { get; set; }
        public Vector2 BallAcceleration { get; set; }
        public double BallRadius { get; set; }
        public Color BallColor { get; set; }

        public int BlocksPerColumn { get; set; }
        public int BlocksPerRow { get; set; }
        public int BlockSpacing { get; set; }
        public int BlockWidth { get; set; }
        public int BlockHeight { get; set; }
        public int BlockDistanceFromCeiling { get; set; }
        public Color BlockColor { get; set; }

        public int PaddleWidth { get; set; }
        public int PaddleHeight { get; set; }
        public int PaddleDistanceFromGutter { get; set; }
        public Color PaddleColor { get; set; }
        public int PaddleSpeed { get; set; }
    }
}
