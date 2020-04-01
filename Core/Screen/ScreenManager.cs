using System;

namespace Core.Screen
{
    using Audio;
    using Resources;

    public class ScreenManager : IScreenManager
    {
        public IResourceManager Resources { get; }

        public ScreenBase CurrentScreen { get; set; }

        public ScreenManager(IAudioEngine audio, IResourceManager resources)
        {
            Resources = resources;
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
                    case ScreenState.NONE:
                        throw new ApplicationException("Screen not yet initialized.");
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                GameBase.IsRunning = false;
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