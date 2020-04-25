namespace Engine.Scene
{
    using Engine.Scene.Layers;
    using Engine.Scene.Enumerations;
    using System;

    public abstract class SceneBase : IScene
    {
        protected LayerStack Layers { get; }
        public SceneState State { get; private set; }
        public IScene NextScene { get; set; }
        public IScene PrevScene { get; set; }
        public ISceneManager Manager { get; set; }

        protected SceneBase()
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