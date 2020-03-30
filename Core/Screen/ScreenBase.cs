namespace Core.Screen
{
    using System;
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
        protected IAudioEngine audioEngine;
        public ScreenState State { get; private set; }
        public ScreenBase NextScreen { get; set; }
        public ScreenBase PrevScreen { get; set; }

        protected ScreenBase()
        {
            State = ScreenState.NONE;
        }

        public abstract void OnEnter();
        public abstract void OnLeave();
        public abstract void Update();
        public abstract void Render();

        public void SetDeps(IAudioEngine audioEngineDep)
        {
            audioEngine = audioEngineDep; 
        }
        
        public void Run()
        {
            audioEngine.Init();
            State = ScreenState.RUNNING;
        }
        
        public void Dispose()
        {
            audioEngine.Dispose();    
        }
    }
}