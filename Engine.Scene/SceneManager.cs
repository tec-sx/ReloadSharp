namespace Engine.Scene
{
    using System;
    using Engine.AssetPipeline;

    public class SceneManager
    {
        public event Action ExitGame;
        public IAssetsManager Assets { get; }

        public Scene ActiveScene { get; set; }

        public SceneManager(IAssetsManager assets)
        {
            Assets = assets;
        }

        public Scene MoveToNextScreen()
        {
            ActiveScene = ActiveScene.NextScene;
            return ActiveScene;
        }

        public Scene MoveToPrevScreen()
        {
            ActiveScene = ActiveScene.PrevScene;
            return ActiveScene;
        }

        public void Update(double deltaTime)
        {
            if (ActiveScene != null)
            {
                switch (ActiveScene.State)
                {
                    case SceneState.Running:
                        ActiveScene.Update(deltaTime);
                        break;
                    case SceneState.Paused:
                        break;
                    case SceneState.ChangeNext:
                    {
                        ActiveScene.OnLeave();
                        ActiveScene = MoveToNextScreen();

                        ActiveScene?.Run();
                        ActiveScene?.OnEnter();

                        break;
                    }
                    case SceneState.ChangePrev:
                    {
                        ActiveScene.OnLeave();
                        ActiveScene = MoveToPrevScreen();

                        ActiveScene?.Run();
                        ActiveScene?.OnEnter();

                        break;
                    }
                    case SceneState.ExitApp:
                        ExitGame?.Invoke();
                        break;
                    case SceneState.None:
                        throw new ApplicationException("Scene not yet initialized.");
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                ExitGame?.Invoke();
            }
        }

        public void Render(double deltaTime)
        {
            ActiveScene.Render(deltaTime);
        }

        public Scene CreateScene<T>() where T : Scene, new()
        {
            var newScene = new T
            {
                Manager = this
            };

            if (ActiveScene == null)
            {
                ActiveScene = newScene;
            }
            else
            {
                var tempScreen = ActiveScene;

                while (ActiveScene.NextScene != null)
                {
                    tempScreen = tempScreen.NextScene;
                }

                newScene.PrevScene = tempScreen;
                tempScreen.NextScene = newScene;
            }

            return newScene;
        }
    }
}