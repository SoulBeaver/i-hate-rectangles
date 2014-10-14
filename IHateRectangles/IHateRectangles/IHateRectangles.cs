#region Using Statements
using System;
using System.Collections.Generic;
using System.IO;
using Artemis;
using Artemis.System;
using IHateRectangles.Components;
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
            : base()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                IsFullScreen = false,
                PreferredBackBufferHeight = 1024,
                PreferredBackBufferWidth = 600,
                PreferredBackBufferFormat = SurfaceFormat.Color,
                PreferMultiSampling = false,
                PreferredDepthStencilFormat = DepthFormat.None
            };
            IsFixedTimeStep = false;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            _spritebatch = new SpriteBatch(GraphicsDevice);
            _configuration = ReadConfigurationFile();

            _universe = new EntityWorld();

            EntitySystem.BlackBoard.SetEntry("Configuration", _configuration);
            EntitySystem.BlackBoard.SetEntry("GraphicsDevice", GraphicsDevice);
            EntitySystem.BlackBoard.SetEntry("SpriteBatch", _spritebatch);
            EntitySystem.BlackBoard.SetEntry("ContentManager", Content);

            _universe.InitializeAll(processAttributes: true);

            CreatePaddle();

            base.Initialize();
        }

        private Configuration ReadConfigurationFile()
        {
            string configurationJson = File.ReadAllText("configuration.json");

            return JsonConvert.DeserializeObject<Configuration>(configurationJson);
        }

        private void CreatePaddle()
        {
            var paddle = _universe.CreateEntity();
            paddle.Group = "Ships";
            paddle.Tag = "Player";

            var paddleDimensions = new Rectangle
            {
                Width = _configuration.PaddleWidth,
                Height = _configuration.PaddleHeight,
                Location = new Point(0, 0)
            };
            paddle.AddComponent(new RectangleComponent(paddleDimensions, _configuration.PaddleColor));
            paddle.AddComponent(new PositionComponent((GraphicsDevice.Viewport.Width - _configuration.PaddleWidth) / 2,
                                                      GraphicsDevice.Viewport.Height - _configuration.PaddleDistanceFromGutter));
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
