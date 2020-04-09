using System.Runtime.CompilerServices;
using Core.CoreSystem.Audio;
using Core.CoreSystem.Audio.Device;

[assembly:InternalsVisibleTo("Core.Tests")]

namespace Core
{
    using System;
    using CoreSystem.Graphics;
    using Config;
    using CoreSystem;
    using Screen;
    using Utilities;
    using Silk.NET.Windowing.Common;

    public abstract class GameBase : IDisposable
    {
        private readonly IGraphicsManager _graphicsManager;
        
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

            _graphicsManager = ServiceManager.GetService<IGraphicsManager>();
            _audioDevice    = ServiceManager.GetService<AudioDevice>();
            screenManager   = ServiceManager.GetService<IScreenManager>();
        }

        protected abstract void OnInitialize();
        protected abstract void AddScreens();
        protected abstract void OnDispose();

        private void Initialize()
        {

            _graphicsManager.CreateWindow();
            _graphicsManager.CreateDevice();
            
            AddScreens();

            _currentScreen = screenManager.CurrentScreen;
            _currentScreen?.Run();
            _currentScreen?.OnEnter();
        }

        public void Dispose()
        {
            OnDispose();
            
            _graphicsManager.DisposeResources();
            
            ServiceManager.DisposeServices();
        }

        private void LoadContent()
        {

        }

        private void Update(double deltaTime)
        {
            screenManager.Update();
        }

        private void Render(double deltaTime)
        {
            _currentScreen?.OnRender();
        }

        public void Run()
        {
            Initialize();

            _graphicsManager.Window.Load += LoadContent;
            _graphicsManager.Window.Update += Update;
            _graphicsManager.Window.Render += Render;
            _graphicsManager.Window.Closing += Dispose;

            _graphicsManager.Window.Run();
            _graphicsManager.Device.WaitForIdle();
           
        }
    }
}