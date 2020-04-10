using System.Runtime.CompilerServices;
using Core.CoreSystem.Audio;
using Core.CoreSystem.Audio.Device;

[assembly:InternalsVisibleTo("Core.Tests")]

namespace Core
{
    using System;
    using CoreSystem.Graphics;
    using Config;
    using Screen;
    using Utilities;
    using Silk.NET.Windowing.Common;

    public abstract class GameBase : IDisposable
    {
        private readonly GraphicsManager _graphicsManager;
        private readonly AudioManager _audioManager;
        
        protected readonly IScreenManager screenManager;

        internal static bool isRunning;

        protected GameBase()
        {
            Configuration.LoadDefaultConfiguration();
            EmbeddedResourceManager.LoadNativeLibraries();
            ServiceManager.RegisterServices();

            _graphicsManager = ServiceManager.GetService<GraphicsManager>();
            _audioManager    = ServiceManager.GetService<AudioManager>();
            screenManager   = ServiceManager.GetService<IScreenManager>();
        }

        protected abstract void OnInitialize();
        protected abstract void AddScreens();
        protected abstract void OnDispose();

        private void Initialize()
        {

            _graphicsManager.CreateWindow();
            _graphicsManager.CreateDevice();
            _audioManager.CreateDevice();

            AddScreens();
        }

        public void Dispose()
        {
            OnDispose();
            
            _graphicsManager.DisposeResources();
            _audioManager.DisposeResources();
            
            ServiceManager.DisposeServices();
        }

        private void LoadContent()
        {

        }

        private void Update(double deltaTime)
        {
            screenManager.Update(deltaTime);
        }

        private void Render(double deltaTime)
        {
            screenManager.Render(deltaTime);
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