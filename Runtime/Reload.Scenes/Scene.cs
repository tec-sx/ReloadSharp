namespace Reload.Scenes
{
    using System;
    using Reload.Rendering.Camera;
    using Reload.Scenes.Enumerations;
    using Reload.Scenes.Layers;

    /// <summary>
    /// Scene base abstract class. every scene, be it gameplay,
    /// menu, cut scene, etc. must inherit from this class.
    /// </summary>
    public abstract class Scene
    {
        public event Action<SceneState> SceneStateChange;

        /// <summary>
        /// The current scene's layers stack.
        /// </summary>
        public LayerStack Layers { get; set; }

        /// <summary>
        /// Current scene's state.
        /// </summary>
        public SceneState CurrentState { get; private set; }

        /// <summary>
        /// Previous scene's state.
        /// </summary>
        public SceneState PreviousState { get; private set; }

        /// <summary>
        /// Reference to the next scene.
        /// </summary>
        public Scene NextScene { get; set; }

        /// <summary>
        /// Reference to the previos scene.
        /// </summary>
        public Scene PrevScene { get; set; }

        /// <summary>
        /// The scene manager managing the current scene.
        /// </summary>
        public SceneMachine SceneMachine { get; set; }

        /// <summary>
        /// Gets or sets the scene's camera.
        /// </summary>
        public Camera Camera { get; set; }

        /// <summary>
        /// Gets or sets the camera controller.
        /// </summary>
        public CameraController CameraController { get; set; }

        /// <summary>
        /// Scene base constructor.
        /// </summary>
        protected Scene()
        {
            PreviousState = CurrentState = SceneState.Stopped;
            Layers = new LayerStack(this);
        }

        /// <summary>
        /// Cleans resources from memory.
        /// </summary>
        public void CleanResources()
        {
            Layers.ClearStack();
        }

        /// <summary>
        /// Override this method to add logic upon entering/initializng
        /// the scene.
        /// </summary>
        public abstract void OnEnter();

        /// <summary>
        /// Override this method to add logic before leaving/disposing
        /// the scene.
        /// </summary>
        public abstract void OnLeave();

        /// <summary>
        /// Override this method to add update logic. Called before updating the layers.
        /// the scene.
        /// </summary>
        public abstract void OnUpdate(double deltaTime);

        /// <summary>
        /// Override this method to add rendering logic.
        /// </summary>
        /// <param name="deltaTime"></param>
        public abstract void OnRender(double deltaTime);

        /// <summary>
        /// Sets the scene state to running.
        /// </summary>
        public void Run() => ChangeSceneState(SceneState.Running);

        /// <inheritdoc/>
        public void Pause() => ChangeSceneState(SceneState.Paused);

        /// <inheritdoc/>
        public void Stop() => ChangeSceneState(SceneState.Stopped);

        /// <inheritdoc/>
        public void Update(double deltaTime)
        {
            if (CurrentState != SceneState.Running)
            {
                return;
            }

            OnUpdate(deltaTime);
            Layers.Update(deltaTime);
        }

        /// <inheritdoc/>
        public void Render(double deltaTime)
        {
            if (CurrentState != SceneState.Running)
            {
                return;
            }

            Layers.Draw(deltaTime);

            OnRender(deltaTime);
        }

        /// <inheritdoc/>
        public void ChangeSceneState(SceneState state)
        {
            PreviousState = CurrentState;
            CurrentState = state;
            SceneStateChange?.Invoke(state);
        }
    }
}