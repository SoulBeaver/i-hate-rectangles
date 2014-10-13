using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace IHateRectangles
{
    public class Configuration
    {
        public Vector2 InitialBallVelocity { get; private set; }
        public Vector2 BallAcceleration { get; private set; }
        public int BallRadius { get; private set; }
        public Color BallColor { get; private set; }

        public int BlocksPerColumn { get; private set; }
        public int BlocksPerRow { get; private set; }
        public int BlockSpacing { get; private set; }
        public int BlockDistanceFromCeiling { get; private set; }
        public Color BlockColor { get; private set; }

        public int PaddleWidth { get; private set; }
        public int PaddleHeight { get; private set; }
        public int PaddleDistanceFromGutter { get; private set; }
        public Color PaddleColor { get; private set; }
    }
}
