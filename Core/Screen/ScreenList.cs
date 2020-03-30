namespace Core.Screen
{
    using Audio;

    public class ScreenList : IScreenList
    {
        private readonly IAudioEngine _audioEngine;
        public ScreenBase CurrentScreen { get; set; }

        public ScreenList(IAudioEngine audioEngine)
        {
            _audioEngine = audioEngine;
        }
        
        public ScreenBase MoveToNextScreen()
        {
            CurrentScreen = CurrentScreen.NextScreen;
            return CurrentScreen;
        }

        public ScreenBase MoveToPrevScreen()
        {
            CurrentScreen = CurrentScreen.PrevScreen;
            return CurrentScreen;
        }

        public void Update()
        {
            if (CurrentScreen != null)
            {
                switch (CurrentScreen.State)
                {
                    case ScreenState.RUNNING:
                        CurrentScreen.Update();
                        break;
                    case ScreenState.PAUSED:
                        break;
                    case ScreenState.CHANGE_NEXT:
                    {
                        CurrentScreen.OnLeave();
                        CurrentScreen = MoveToNextScreen();
                        
                        CurrentScreen?.Run();
                        CurrentScreen?.OnEnter();
                        
                        break;
                    }
                    case ScreenState.CHANGE_PREV:
                    {
                        CurrentScreen.OnLeave();
                        CurrentScreen = MoveToPrevScreen();
                        
                        CurrentScreen?.Run();
                        CurrentScreen?.OnEnter();
                        
                        break;
                    }
                    case ScreenState.EXIT_APP:
                        GameBase.IsRunning = false;
                        break;
                }
            }
            else
            {
                GameBase.IsRunning = false;
            }
        }

        public void AddScreen(ScreenBase newScreen)
        {
            newScreen.SetDeps(_audioEngine);
            
            if (CurrentScreen == null)
            {
                CurrentScreen = newScreen;
                return;
            }

            var tempScreen = CurrentScreen;

            while (CurrentScreen.NextScreen != null)
            {
                tempScreen = tempScreen.NextScreen;
            }

            newScreen.PrevScreen = tempScreen;
            tempScreen.NextScreen = newScreen;
        }
    }
}