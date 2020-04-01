namespace Core.Screen
{
    using System;
    using Resources;
    using Audio;

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
        public abstract void Update();
        public abstract void Render();

        public void Run()
        {
            State = ScreenState.RUNNING;
        }

        public void Dispose()
        {
        }
    }
}