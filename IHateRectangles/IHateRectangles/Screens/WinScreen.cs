using Artemis.System;
using IHateRectangles.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IHateRectangles
{
    public class WinScreen : Screen
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _menuFont;
        private ContentManager _contentManager;

        public event ScreenFinished OnScreenFinished;

        public void Initialize()
        {
            _contentManager = EntitySystem.BlackBoard.GetEntry<ContentManager>("ContentManager");

            _menuFont = _contentManager.Load<SpriteFont>(@"Fonts\Hud");
            _spriteBatch = EntitySystem.BlackBoard.GetEntry<SpriteBatch>("SpriteBatch");
        }

        public void Update()
        {
            if (Keyboard.GetState().GetPressedKeys().Length > 0)
                if (OnScreenFinished != null)
                    OnScreenFinished(new MenuScreen());
        }

        public void Draw()
        {
            var viewport = EntitySystem.BlackBoard.GetEntry<GraphicsDevice>("GraphicsDevice").Viewport;

            _spriteBatch.Begin();
            _spriteBatch.DrawString(_menuFont,
                                    "You win, yay!",
                                    new Vector2(viewport.Width / 2 - 100, viewport.Height / 2 - 200),
                                    Color.White);
            _spriteBatch.DrawString(_menuFont,
                                    "Press any key to return to the Menu or Escape to quit.",
                                    new Vector2(viewport.Width / 2 - 250, viewport.Height / 2 - 100),
                                    Color.White);
            _spriteBatch.End();
        }

        public void Destroy()
        { }
    }
}
