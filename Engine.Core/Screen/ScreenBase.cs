using Core.Screen.Layers;

namespace Core.Screen
{
    using System;
    using CoreSystem;


    public enum ScreenState
    {
        NONE,
        RUNNING,
        PAUSED,
        EXIT_APP,
        CHANGE_NEXT,
        CHANGE_PREV
    }

    public abstract class ScreenBase : IDisposable
    {
        protected LayerStack Layers { get; }
        public ScreenState State { get; private set; }
        public ScreenBase NextScreen { get; set; }
        public ScreenBase PrevScreen { get; set; }
        public ScreenManager Manager { get; set; }

        protected ScreenBase()
        {
            State = ScreenState.NONE;
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
            State = ScreenState.RUNNING;
        }

        public void Update(double deltaTime)
        {
            if (State != ScreenState.RUNNING)
            {
                return;
            }
            
            OnUpdate(deltaTime);
            Layers.Update();
        }

        public void Render(double deltaTime)
        {
            if (State != ScreenState.RUNNING)
            {
                return;
            }
            
            OnRender(deltaTime);
        }
    }
}