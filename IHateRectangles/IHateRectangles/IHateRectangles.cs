#region Using Statements
using System;
using System.Collections.Generic;
using System.IO;
using Artemis;
using Artemis.System;
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
        private SpriteBatch _spriteBatch;
        private EntityWorld _universe;

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

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            var configuration = ReadConfigurationFile();

            _universe = new EntityWorld();
            EntitySystem.BlackBoard.SetEntry("Configuration", configuration);
            EntitySystem.BlackBoard.SetEntry("GraphicsDevice", GraphicsDevice);
            EntitySystem.BlackBoard.SetEntry("SpriteBatch", _spriteBatch);

            _universe.InitializeAll(processAttributes: true);

            base.Initialize();
        }

        private Configuration ReadConfigurationFile()
        {
            string configurationJson = File.ReadAllText("configuration.json");

            return JsonConvert.DeserializeObject<Configuration>(configurationJson);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
