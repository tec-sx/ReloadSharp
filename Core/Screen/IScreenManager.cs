namespace Core.Screen
{
    public interface IScreenManager
    {
        ScreenBase ActiveScreen { get; set; }
        ScreenBase MoveToNextScreen();
        ScreenBase MoveToPrevScreen();
        
        void Update(double deltaTime);
        void Render(double deltaTime);
        ScreenBase CreateScreen<T>() where T : ScreenBase, new();
    }
}