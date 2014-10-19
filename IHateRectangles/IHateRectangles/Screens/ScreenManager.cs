using IHateRectangles.Screens;

namespace IHateRectangles
{
    public class ScreenManager
    {
        private Screen _current;

        public void Update()
        {
            _current.Update();
        }

        public void Draw()
        {
            _current.Draw();
        }

        public void SetScreen(Screen next)
        {
            if (_current != null)
                _current.Destroy();
            
            _current = next;
            _current.Initialize();
            _current.OnScreenFinished += OnScreenFinished;
        }

        private void OnScreenFinished(Screen next)
        {
            _current.OnScreenFinished -= OnScreenFinished;

            SetScreen(next);
        }
    }
}
