using System.Linq;
using Artemis;
using Artemis.System;
using IHateRectangles.Components;
using IHateRectangles.Screens;
using IHateRectangles.Templates;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace IHateRectangles
{
    public class GameScreen : Screen
    {
        private EntityWorld _universe;
        private Configuration _configuration;
        private ContentManager _contentManager;
        private GraphicsDevice _graphicsDevice;

        public event ScreenFinished OnScreenFinished;

        public void Initialize()
        {
            _configuration = EntitySystem.BlackBoard.GetEntry<Configuration>("Configuration");
            _contentManager = EntitySystem.BlackBoard.GetEntry<ContentManager>("ContentManager");
            _graphicsDevice = EntitySystem.BlackBoard.GetEntry<GraphicsDevice>("GraphicsDevice");

            _universe = new EntityWorld();
            _universe.InitializeAll(processAttributes: true);
            _universe.CreateEntityFromTemplate(PaddleTemplate.Name);
            _universe.CreateEntityFromTemplate(BallTemplate.Name, _configuration.InitialBallVelocity);
            
            CreateBlocks();
            CreateBackground();
        }

        private void CreateBlocks()
        {
            var blocksPerRow = _configuration.BlocksPerRow;
            var blocksPerColumn = _configuration.BlocksPerColumn;
            var blockSpacing = _configuration.BlockSpacing;
            var blockColor = _configuration.BlockColor;
            var startingRowY = _configuration.BlockDistanceFromCeiling;
            var blockWidth = _configuration.BlockWidth;
            var blockHeight = _configuration.BlockHeight;

            for (int row = 0; row < blocksPerColumn; ++row)
            {
                for (int column = 0; column < blocksPerRow; ++column)
                {
                    var blockY = startingRowY + (blockSpacing * row + row * blockHeight);
                    var blockX = column * blockWidth + (column * blockSpacing) + blockSpacing / 2;

                    _universe.CreateEntityFromTemplate(BlockTemplate.Name,
                                                       blockWidth,
                                                       blockHeight,
                                                       blockColor,
                                                       blockY,
                                                       blockX);
                }
            }
        }

        private void CreateBackground()
        {
            var background = _universe.CreateEntity();
            background.AddComponent(new BackgroundComponent(_contentManager.Load<Texture2D>(@"Textures\Background")));
        }

        public void Update()
        {
            _universe.Update();

            var ball = _universe.TagManager.GetEntity(BallTemplate.Name);
            if (ball.GetComponent<PositionComponent>().Y > _graphicsDevice.Viewport.Height)
                if (OnScreenFinished != null)
                    OnScreenFinished(new LoseScreen());

            var blocks = _universe.GroupManager.GetEntities(BlockTemplate.Group);
            if (blocks.All(block => block.GetComponent<PositionComponent>().Y == -500))
                if (OnScreenFinished != null)
                    OnScreenFinished(new WinScreen());
        }

        public void Draw()
        {
            _universe.Draw();
        }

        public void Destroy()
        {
            
        }
    }
}
