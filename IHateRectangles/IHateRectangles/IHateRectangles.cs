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
        private ScreenManager _screenManager;

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

            EntitySystem.BlackBoard.SetEntry("Configuration", _configuration);
            EntitySystem.BlackBoard.SetEntry("GraphicsDevice", GraphicsDevice);
            EntitySystem.BlackBoard.SetEntry("SpriteBatch", _spritebatch);
            EntitySystem.BlackBoard.SetEntry("ContentManager", Content);

            _screenManager = new ScreenManager();
            _screenManager.SetScreen(new MenuScreen());

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spritebatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        { }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _screenManager.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _screenManager.Draw();
        }
    }
}
