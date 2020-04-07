namespace Reload
{
    using Core;
    using Screens;

    public class Reload : GameBase
    {
        protected override void OnInitialize()
        {
        }

        protected override void AddScreens()
        {
            var introScreen = screenManager.CreateScreen<IntroScreen>();
            screenManager.CurrentScreen = introScreen;
        }

        protected override void OnDispose()
        {
        }
    }
}