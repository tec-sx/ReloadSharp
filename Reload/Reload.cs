namespace Reload
{
    using Core;
    using Screens;
    
    public class Reload : GameBase
    {
        protected override void OnInit()
        {
        }

        protected override void AddScreens()
        {
            var introScreen = new IntroScreen();
            ScreenList.AddScreen(introScreen);
            ScreenList.CurrentScreen = introScreen;
        }

        protected override void OnDispose()
        {
        }
    }
}