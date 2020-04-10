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
        public ScreenState State { get; private set; }
        public ScreenBase NextScreen { get; set; }
        public ScreenBase PrevScreen { get; set; }
        public ScreenManager Manager { get; set; }

        protected ScreenBase()
        {
            State = ScreenState.NONE;
        }

        public abstract void OnEnter();
        public abstract void OnLeave();
        public abstract void OnUpdate(double deltaTime);
        public abstract void OnRender(double deltaTime);

        public void Dispose()
        {
        }

        public void Run()
        {
            State = ScreenState.RUNNING;
        }

        public void Update(double deltaTime)
        {
            if (State == ScreenState.RUNNING)
            {
                OnUpdate(deltaTime);
            }
        }

        public void Render(double deltaTime)
        {
            if (State == ScreenState.RUNNING)
            {
                OnRender(deltaTime);
            }
        }
    }
}