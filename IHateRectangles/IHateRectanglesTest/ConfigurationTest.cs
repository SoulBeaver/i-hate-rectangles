using System;
using System.IO;
using System.Text;
using IHateRectangles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace IHateRectanglesTest
{
    [TestClass]
    public class ConfigurationTest
    {
        private readonly Configuration configuration = new Configuration
        {
            InitialBallVelocity = new Vector2(1, 2),
            BallAcceleration = new Vector2(10, 10),
            BallRadius = 2,
            BallColor = Color.Black,

            BlocksPerColumn = 5,
            BlocksPerRow = 5,
            BlockSpacing = 20,
            BlockWidth = 100,
            BlockHeight = 20,
            BlockDistanceFromCeiling = 100,
            BlockColor = Color.Black,

            PaddleWidth = 100,
            PaddleHeight = 20,
            PaddleDistanceFromGutter = 100,
            PaddleColor = Color.Black
        };

        private readonly String json =
            "{\"InitialBallVelocity\":{\"X\":1.0,\"Y\":2.0},\"BallAcceleration\":{\"X\":10.0,\"Y\":10.0},\"BallRadius\":2.0,\"BallColor\":{\"B\":0,\"G\":0,\"R\":0,\"A\":255},\"BlocksPerColumn\":5,\"BlocksPerRow\":5,\"BlockSpacing\":20,\"BlockWidth\":100,\"BlockHeight\":20,\"BlockDistanceFromCeiling\":100,\"BlockColor\":{\"B\":0,\"G\":0,\"R\":0,\"A\":255},\"PaddleWidth\":100,\"PaddleHeight\":20,\"PaddleDistanceFromGutter\":100,\"PaddleColor\":{\"B\":0,\"G\":0,\"R\":0,\"A\":255}}";

        [TestMethod]
        public void SerializeConfiguration_ReturnsJson()
        {
            var actual = JsonConvert.SerializeObject(configuration);

            Assert.AreEqual(actual, json);
        }

        [TestMethod]
        public void DeserializeConfiguration_PopulatesConfiguration()
        {
            var actual = JsonConvert.DeserializeObject<Configuration>(json);

            Assert.AreEqual(actual, configuration);
        }
    }
}
