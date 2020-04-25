namespace Engine.Scene
{
    using Engine.Scene.Layers;
    using System;

    public enum SceneState
    {
        None,
        Running,
        Paused,
        ExitApp,
        ChangeNext,
        ChangePrev
    }

    public abstract class Scene : IDisposable
    {
        protected LayerStack Layers { get; }
        public SceneState State { get; private set; }
        public Scene NextScene { get; set; }
        public Scene PrevScene { get; set; }
        public SceneManager Manager { get; set; }

        protected Scene()
        {
            State = SceneState.None;
            Layers = new LayerStack();
        }

        public abstract void OnEnter();
        public abstract void OnLeave();
        public abstract void OnUpdate(double deltaTime);
        public abstract void OnRender(double deltaTime);

        public void Dispose()
        {
            Layers.DisposeStack();
        }

        public void Run()
        {
            State = SceneState.Running;
        }

        public void Update(double deltaTime)
        {
            if (State != SceneState.Running)
            {
                return;
            }

            OnUpdate(deltaTime);
            Layers.Update();
        }

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