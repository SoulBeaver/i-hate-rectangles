#region Using Statements
using System;
using System.Collections.Generic;
using System.IO;
using Artemis;
using Artemis.System;
using IHateRectangles.Components;
using IHateRectangles.Templates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Newtonsoft.Json;

#endregion

namespace IHateRectangles
{
    public class IHateRectangles : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spritebatch;
        private EntityWorld _universe;
        private Configuration _configuration;

        public IHateRectangles()
        {
            _configuration = ReadConfigurationFile();

            _graphics = new GraphicsDeviceManager(this)
            {
                IsFullScreen = false,
                PreferredBackBufferWidth = _configuration.ScreenWidth,
                PreferredBackBufferHeight = _configuration.ScreenHeight
            };
            IsFixedTimeStep = false;

            Content.RootDirectory = "Content";
        }

        private Configuration ReadConfigurationFile()
        {
            string configurationJson = File.ReadAllText("configuration.json");

            return JsonConvert.DeserializeObject<Configuration>(configurationJson);
        }

        protected override void Initialize()
        {
            _spritebatch = new SpriteBatch(GraphicsDevice);

            _universe = new EntityWorld();

            EntitySystem.BlackBoard.SetEntry("Configuration", _configuration);
            EntitySystem.BlackBoard.SetEntry("GraphicsDevice", GraphicsDevice);
            EntitySystem.BlackBoard.SetEntry("SpriteBatch", _spritebatch);
            EntitySystem.BlackBoard.SetEntry("ContentManager", Content);

            _universe.InitializeAll(processAttributes: true);
            _universe.CreateEntityFromTemplate(PaddleTemplate.Name);
            _universe.CreateEntityFromTemplate(BallTemplate.Name, _configuration.InitialBallVelocity);
            CreateBlocks();

            base.Initialize();
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
                    var blockY = startingRowY + (blockSpacing*row + row*blockHeight);
                    var blockX = column*blockWidth + (column*blockSpacing) + blockSpacing/2;

                    _universe.CreateEntityFromTemplate(BlockTemplate.Name, 
                                                       blockWidth, 
                                                       blockHeight, 
                                                       blockColor, 
                                                       blockY, 
                                                       blockX);
                }
            }
        }

        protected override void LoadContent()
        {
            _spritebatch = new SpriteBatch(GraphicsDevice);

            CreateBackground();
        }

        private void CreateBackground()
        {
            var background = _universe.CreateEntity();
            background.AddComponent(new BackgroundComponent(Content.Load<Texture2D>(@"Textures\Background")));
        }

        protected override void UnloadContent()
        { }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _universe.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spritebatch.Begin();
            _universe.Draw();
            _spritebatch.End();
        }
    }
}
