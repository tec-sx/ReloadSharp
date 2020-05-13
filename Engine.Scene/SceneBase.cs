namespace Engine.Scene
{
    using Engine.Scene.Enumerations;
    using Engine.Scene.Layers;

    /// <summary>
    /// Scene base abstract class. every scene, be it gameplay,
    /// menu, cut scene, etc. must inherit from this class.
    /// </summary>
    public abstract class SceneBase : IScene
    {
        /// <summary>
        /// The currrent scene's layers stack.
        /// </summary>
        protected LayerStack Layers { get; }

        /// <summary>
        /// Current scene's state.
        /// </summary>
        public SceneState State { get; private set; }

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
            State = SceneState.None;
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
            State = SceneState.Running;
        }

        /// <summary>
        /// If screen is running, calls <see cref="OnUpdate(double)"/>
        /// for current screen and then updates all layers.
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Update(double deltaTime)
        {
            if (State != SceneState.Running)
            {
                return;
            }

            OnUpdate(deltaTime);
            Layers.Update();
        }

        /// <summary>
        /// If screen is running, calls <see cref="OnRender(double)"/>
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Render(double deltaTime)
        {
            if (State != SceneState.Running)
            {
                return;
            }

            OnRender(deltaTime);
        }
    }
}