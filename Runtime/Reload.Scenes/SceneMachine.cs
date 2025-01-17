using Reload.Scenes.Enumerations;
using System;

namespace Reload.Scenes
{   
    /// <summary>
    /// The scene manager. Instantiated as singleton in the
    /// service manager.
    /// </summary>
    public class SceneMachine
    {
        /// <summary>
        /// Event invoked when scene reaches ExitProgram state,
        /// or when active scene is null.
        /// </summary>
        public event Action ExitProgram;

        /// <summary>
        /// Reference to the current active scene.
        /// </summary>
        public Scene ActiveScene { get; set; }

        /// <summary>
        /// Sets the next screen as the active screen.
        /// </summary>
        /// <returns>The new active scene</returns>
        public Scene MoveToNextScreen()
        {
            var oldScene = ActiveScene;

            ActiveScene = ActiveScene.NextScene;

            oldScene.SceneStateChange -= SceneStateChanged;
            ActiveScene.SceneStateChange += SceneStateChanged;

            return ActiveScene;
        }

        /// <summary>
        /// Sets the previous screen as the active screen.
        /// </summary>
        /// <returns>The new active scene</returns>
        public Scene MoveToPrevScreen()
        {
            var oldScene = ActiveScene;

            ActiveScene = ActiveScene.PrevScene;

            oldScene.SceneStateChange -= SceneStateChanged;
            ActiveScene.SceneStateChange += SceneStateChanged;

            return ActiveScene;
        }

        ///<inheritdoc/>
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
        public void UpdateActiveScene(double deltaTime)
        {
            ActiveScene?.Update(deltaTime);
        }

        /// <summary>
        /// Render the active scene.
        /// </summary>
        /// <param name="deltaTime"></param>
        public void RenderActiveScene(double deltaTime)
        {
            ActiveScene?.Render(deltaTime);
        }

        /// <summary>
        /// Adds new scene to the end of the scene chain.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Scene AddScene<T>() where T : Scene, new()
        {
            var newScene = new T
            {
                SceneMachine = this
            };

            if (ActiveScene == null)
            {
                ActiveScene = newScene;
                ActiveScene.SceneStateChange += SceneStateChanged;
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
        /// Callback method for when the <see cref="Scene.SceneStateChange"/> event is fired.
        /// </summary>
        /// <param name="state"></param>
        public void SceneStateChanged(SceneState state)
        {
            switch (state)
            {
                case SceneState.Running:
                    if (ActiveScene?.PreviousState == SceneState.Stopped) ActiveScene?.OnEnter();
                    break;
                case SceneState.Paused:
                    ActiveScene = MoveToNextScreen();
                    ActiveScene?.Run();
                    break;
                case SceneState.Stopped:
                    ActiveScene?.OnLeave();
                    break;
                case SceneState.ChangeNext:
                    ActiveScene?.Stop();
                    ActiveScene = MoveToNextScreen();
                    ActiveScene?.Run();
                    break;
                case SceneState.ChangePrev:
                    ActiveScene?.Stop();
                    ActiveScene = MoveToPrevScreen();
                    ActiveScene?.Run();
                    break;
                case SceneState.ExitProgram:
                    ExitProgram?.Invoke();
                    break;
                default:
                    // TODO: Move the string to resources file.
                    throw new ApplicationException("Invalid scene state");
            }
        }
    }
}