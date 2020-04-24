using Core.GamePlay;

namespace Core.Screen
{
    using System;
    using AssetsPipeline;
    
    public class ScreenManager
    {
        public IAssetsManager Assets { get; }
        public PlayerAction PlayerAction { get; }
        
        public ScreenBase ActiveScreen { get; set; }

        public ScreenManager(
            IAssetsManager assets,
            PlayerAction playerAction)
        {
            Assets = assets;
            PlayerAction = playerAction;
        }

        public ScreenBase MoveToNextScreen()
        {
            ActiveScreen = ActiveScreen.NextScreen;
            return ActiveScreen;
        }

        public ScreenBase MoveToPrevScreen()
        {
            ActiveScreen = ActiveScreen.PrevScreen;
            return ActiveScreen;
        }

        public void Update(double deltaTime)
        {
            if (ActiveScreen != null)
            {
                switch (ActiveScreen.State)
                {
                    case ScreenState.RUNNING:
                        ActiveScreen.Update(deltaTime);
                        break;
                    case ScreenState.PAUSED:
                        break;
                    case ScreenState.CHANGE_NEXT:
                    {
                        ActiveScreen.OnLeave();
                        ActiveScreen = MoveToNextScreen();

                        ActiveScreen?.Run();
                        ActiveScreen?.OnEnter();

                        break;
                    }
                    case ScreenState.CHANGE_PREV:
                    {
                        ActiveScreen.OnLeave();
                        ActiveScreen = MoveToPrevScreen();

                        ActiveScreen?.Run();
                        ActiveScreen?.OnEnter();

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

        public void Render(double deltaTime)
        {
            ActiveScreen.Render(deltaTime);
        }

        public ScreenBase CreateScreen<T>() where T : ScreenBase, new()
        {
            var newScreen = new T
            {
                Manager = this
            };

            if (ActiveScreen == null)
            {
                ActiveScreen = newScreen;
            }
            else
            {
                var tempScreen = ActiveScreen;

                while (ActiveScreen.NextScreen != null)
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