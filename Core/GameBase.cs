using Core.CoreSystem.Graphics;

namespace Core
{
    using System;
    using Libraries;
    using Config;
    using CoreSystem;
    using Screen;
    using Utilities;
    using Silk.NET.Windowing;
    using Silk.NET.Windowing.Common;

    public abstract class GameBase : IDisposable
    {
        private IWindow _window;
        private VulkanBackend _backend;
        
        private readonly AudioDevice _audioDevice;
        protected readonly IScreenManager screenManager;

        protected internal static bool isRunning;
        protected static float deltaTime;
        protected static float fps;

        private ScreenBase _currentScreen;

        protected GameBase()
        {
            Configuration.LoadDefaultConfiguration();
            EmbeddedResourceManager.LoadNativeLibraries();
            ServiceManager.RegisterServices();
            
            _backend = new VulkanBackend();
            _audioDevice    = ServiceManager.GetService<AudioDevice>();
            screenManager   = ServiceManager.GetService<IScreenManager>();
        }

        protected abstract void OnInitialize();
        protected abstract void AddScreens();
        protected abstract void OnDispose();

        private void Initialize()
        {

            _window = WindowFactory.CreateWindow();
            _backend.Initialize(_window);
            
            AddScreens();

            _currentScreen = screenManager.CurrentScreen;
            _currentScreen?.Run();
            _currentScreen?.OnEnter();
        }

        public void Dispose()
        {
            OnDispose();

            ServiceManager.DisposeServices();
        }

        private void LoadContent()
        {

        }

        private void Update(float deltaTime)
        {
            screenManager.Update();
        }

        private void Render(float deltaTime)
        {
            _currentScreen?.OnRender();
        }

        public void Run()
        {
            Initialize();

            _window.Run();
            _backend.WaitForIdle();
            
            // var fpsLimiter = new FpsLimiter();
            //
            // while (isRunning)
            // {
            //     fpsLimiter.Begin();
            //
            //     deltaTime = fpsLimiter.DeltaTime;
            //     fps = fpsLimiter.End();
            // }
        }
    }
}