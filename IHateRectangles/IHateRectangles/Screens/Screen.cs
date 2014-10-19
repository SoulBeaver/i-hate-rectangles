namespace IHateRectangles.Screens
{
    public delegate void ScreenFinished(Screen nextScreen);

    public interface Screen
    {
        event ScreenFinished OnScreenFinished;
        
        void Initialize();
        void Update();
        void Draw();
        void Destroy();
    }
}
