using System;

namespace Core.Screen
{
    using Resources;

    public class ScreenManager : IScreenManager
    {
        public IAssetsManager Assetses { get; }

        public ScreenBase CurrentScreen { get; set; }

        public ScreenManager(IAssetsManager assetses)
        {
            Assetses = assetses;
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
                        CurrentScreen.OnUpdate();
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
                        GameBase.isRunning = false;
                        break;
                    case ScreenState.NONE:
                        throw new ApplicationException("Screen not yet initialized.");
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                GameBase.isRunning = false;
            }
        }

        public ScreenBase CreateScreen<T>() where T : ScreenBase, new()
        {
            var newScreen = new T
            {
                Manager = this
            };

            if (CurrentScreen == null)
            {
                CurrentScreen = newScreen;
            }
            else
            {
                var tempScreen = CurrentScreen;

                while (CurrentScreen.NextScreen != null)
                {
                    tempScreen = tempScreen.NextScreen;
                }

                newScreen.PrevScreen = tempScreen;
                tempScreen.NextScreen = newScreen;
            }

            return newScreen;
        }
    }
}