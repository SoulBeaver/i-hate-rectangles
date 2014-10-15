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
            : base()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                IsFullScreen = false,
                PreferredBackBufferHeight = 600,
                PreferredBackBufferWidth = 400,
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
            _universe.CreateEntityFromTemplate(PaddleTemplate.Name);
            _universe.CreateEntityFromTemplate(BallTemplate.Name);

            base.Initialize();
        }

        private Configuration ReadConfigurationFile()
        {
            string configurationJson = File.ReadAllText("configuration.json");

            return JsonConvert.DeserializeObject<Configuration>(configurationJson);
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
