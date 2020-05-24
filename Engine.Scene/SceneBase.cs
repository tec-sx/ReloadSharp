using Reload.Scene.Enumerations;
using Reload.Scene.Layers;

namespace Reload.Scene
{
    using Reload.Scene.Enumerations;
    using Reload.Scene.Layers;
    using System;

    /// <summary>
    /// Scene base abstract class. every scene, be it gameplay,
    /// menu, cut scene, etc. must inherit from this class.
    /// </summary>
    public abstract class SceneBase : IScene
    {
        ///<inheritdoc>
        public event Action<SceneState> SceneStateChange;

        /// <summary>
        /// The currrent scene's layers stack.
        /// </summary>
        protected LayerStack Layers { get; }

        /// <summary>
        /// Current scene's state.
        /// </summary>
        private SceneState _state;

        /// <summary>
        /// Reference to the next scene.
        /// </summary>
        public IScene NextScene { get; set; }

        /// <summary>
        /// Reference to the previos scene.
        /// </summary>
        public IScene PrevScene { get; set; }

        /// <summary>
        /// The scene manager managing the current scene.
        /// </summary>
        public ISceneManager Manager { get; set; }

        /// <summary>
        /// Scene base constructor.
        /// </summary>
        protected SceneBase()
        {
            _state = SceneState.Ready;
            Layers = new LayerStack();
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
        public void Run()
        {
            if (_state != SceneState.Ready)
            {
                throw new ApplicationException(Properties.Resources.SceneIsAlreadyRunningExceptionMessage);
            }

            OnEnter();
            ChangeSceneState(SceneState.Running);
        }

        /// <inheritdoc/>
        public void Update(double deltaTime)
        {
            if (_state != SceneState.Running)
            {
                return;
            }

            OnUpdate(deltaTime);
            Layers.Update();
        }

        /// <inheritdoc/>
        public void Render(double deltaTime)
        {
            if (_state != SceneState.Running)
            {
                return;
            }

            OnRender(deltaTime);
        }

        /// <inheritdoc>
        public void Pause()
        {
            ChangeSceneState(SceneState.Paused);
        }

        /// <inheritdoc>
        public void Resume()
        {
            ChangeSceneState(SceneState.Running);
        }

        /// <inheritdoc>
        public void ChangeSceneState(SceneState state)
        {
            _state = state;
            SceneStateChange?.Invoke(state);
        }
    }
}