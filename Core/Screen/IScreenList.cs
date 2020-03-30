namespace Core.Screen
{
    public interface IScreenList
    {
        ScreenBase CurrentScreen { get; set; }
        ScreenBase MoveToNextScreen();
        ScreenBase MoveToPrevScreen();
        void Update();
        void AddScreen(ScreenBase newScreen);
    }
}