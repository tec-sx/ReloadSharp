namespace Engine.Scene
{
    using System;
    using Engine.AssetPipeline;
    using Engine.Scene.Enumerations;

    /// <summary>
    /// The scene manager. Instantiated as singleton in the
    /// service manager.
    /// </summary>
    public class SceneManager : ISceneManager
    {
        /// <summary>
        /// Event invoked when scene reaches ExitProgram state,
        /// or when active scene is null.
        /// </summary>
        public event Action ExitProgram;

        /// <summary>
        /// Reference to the asset manager.
        /// </summary>
        public IAssetsManager Assets { get; }


        /// <summary>
        /// Reference to the current active scene.
        /// </summary>
        public IScene ActiveScene { get; set; }


        /// <summary>
        /// Scene manager constructor.
        /// </summary>
        /// <param name="assets"></param>
        public SceneManager(IAssetsManager assets)
        {
            Assets = assets;
        }

        /// <summary>
        /// Sets the next screen as the active screen.
        /// </summary>
        /// <returns>The new active scene</returns>
        public IScene MoveToNextScreen()
        {
            ActiveScene = ActiveScene.NextScene;
            return ActiveScene;
        }

        /// <summary>
        /// Sets the previous screen as the active screen.
        /// </summary>
        /// <returns>The new active scene</returns>
        public IScene MoveToPrevScreen()
        {
            ActiveScene = ActiveScene.PrevScene;
            return ActiveScene;
        }

        /// <summary>
        /// Checks the active scene's state and
        /// updates it or swithes between scenes.
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Update(double deltaTime)
        {
            if (ActiveScene == null)
            {
                ExitProgram?.Invoke();
            }
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
                case SceneState.ExitProgram:
                    ExitProgram?.Invoke();
                    break;
                case SceneState.None:
                    throw new ApplicationException(
                        Properties.Resources.SceneNotInitializedExceptionMessage);
                default:
                    throw new ApplicationException(
                        Properties.Resources.InvalidSceneStateExceptionMessage);
            }
        }

        /// <summary>
        /// Render the active scene.
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Render(double deltaTime)
        {
            ActiveScene.Render(deltaTime);
        }

        /// <summary>
        /// Adds new scene to the end of the scene chain.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IScene AddScene<T>() where T : IScene, new()
        {
            var newScene = new T
            {
                Manager = this as ISceneManager
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