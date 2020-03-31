namespace Core.Screen
{
    public interface IScreenManager
    {
        ScreenBase CurrentScreen { get; set; }
        ScreenBase MoveToNextScreen();
        ScreenBase MoveToPrevScreen();
        void Update();
        ScreenBase CreateScreen<T>() where T : ScreenBase, new();
    }
}