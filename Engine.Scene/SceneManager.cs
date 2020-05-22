namespace Engine.Scene
{
    using Engine.AssetPipeline;
    using Engine.Scene.Enumerations;
    using Reload.Input;
    using System;

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
        /// Reference to the input manager.
        /// </summary>
        public InputManager Input { get; }

        /// <summary>
        /// Reference to the asset manager.
        /// </summary>
        public IAssetsManager Assets { get; }

        /// <summary>
        /// Reference to the current active scene.
        /// </summary>
        public IScene ActiveScene { get; set; }

        /// <summary>
        /// Initialize scene manager.
        /// </summary>
        /// <param name="assets"></param>
        /// /// <param name="event"></param>
        public SceneManager(IAssetsManager assets, InputManager input)
        {
            Assets = assets;
            Input = input;
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

        ///<inheritdoc>
        public void Run()
        {
            if (ActiveScene == null)
            {
                throw new ApplicationException("No scenes added.");
            }

            ActiveScene.Run();
        }

        /// <summary>
        /// Checks the active scene's state and
        /// updates it or swithes between scenes.
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Update(double deltaTime)
        {
            ActiveScene?.Update(deltaTime);
        }

        /// <summary>
        /// Render the active scene.
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Render(double deltaTime)
        {
            ActiveScene?.Render(deltaTime);
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

        /// <summary>
        /// Callback method for when the <see cref="SceneBase.SceneStateChange"/> event is fired.
        /// </summary>
        /// <param name="state"></param>
        public void SceneStateChanged(SceneState state)
        {
            switch (state)
            {
                case SceneState.Running:
                    break;
                case SceneState.Paused:
                    break;
                case SceneState.ChangeNext:
                    {
                        ActiveScene.OnLeave();
                        ActiveScene = MoveToNextScreen();

                        ActiveScene?.Run();

                        break;
                    }
                case SceneState.ChangePrev:
                    {
                        ActiveScene.OnLeave();
                        ActiveScene = MoveToPrevScreen();

                        ActiveScene?.Run();

                        break;
                    }
                case SceneState.ExitProgram:
                    ExitProgram?.Invoke();
                    break;
                case SceneState.Ready:
                    break;
                default:
                    throw new ApplicationException(
                        Properties.Resources.InvalidSceneStateExceptionMessage);
            }
        }
    }
}