namespace Engine.Core
{
    using System;
    using Engine.Graphics;
    using Engine.Audio;
    using Engine.Input;
    using Engine.Configuration;
    using Scene;
    using Utilities;
    using Silk.NET.Windowing.Common;
    using Engine.Configuration.Extensions;
    using Engine.AssetPipeline;

    public abstract class GameBase : IDisposable
    {
        private readonly IConfigurationManager configurationManager;
        private readonly IGraphicsManager graphicsManager;
        private readonly IInputManager inputManager;
        private readonly IAudioManager audioManager;

        protected readonly ISceneManager sceneManager;
        protected readonly IAssetsManager assetsManager;

        internal static bool isRunning;

        protected GameBase()
        {
            ServiceManager.RegisterServices();

            configurationManager = ServiceManager.GetService<IConfigurationManager>();
            graphicsManager = ServiceManager.GetService<IGraphicsManager>();
            inputManager = ServiceManager.GetService<IInputManager>();
            audioManager    = ServiceManager.GetService<IAudioManager>();

            assetsManager = ServiceManager.GetService<IAssetsManager>();
            sceneManager   = ServiceManager.GetService<ISceneManager>();
        }

        protected abstract void OnInitialize();
        protected abstract void AddScenes();
        protected abstract void OnDispose();

        private void Initialize()
        {
            graphicsManager.Initialize(configurationManager.CreateDisplayConfiguration());
            assetsManager.Initialize(configurationManager.CreateAssetsConfiguration());

            graphicsManager.CreateWindow();
            graphicsManager.CreateDevice();
            inputManager.Initialize(graphicsManager.Window);
            audioManager.CreateContext();

            AddScenes();
            sceneManager.ActiveScene.OnEnter();
            sceneManager.ActiveScene.Run();
        }

        public void Dispose()
        {
            OnDispose();

            graphicsManager.Dispose();
            audioManager.Dispose();

            ServiceManager.DisposeServices();
        }

        private void LoadContent()
        {

        }

        private void Update(double deltaTime)
        {
            sceneManager.Update(deltaTime);
        }

        private void Render(double deltaTime)
        {
            sceneManager.Render(deltaTime);
        }

        public void Run()
        {
            Initialize();

            graphicsManager.Window.Load += LoadContent;
            graphicsManager.Window.Update += Update;
            graphicsManager.Window.Render += Render;
            graphicsManager.Window.Closing += Dispose;

            sceneManager.ExitGame += () => isRunning = false;

            graphicsManager.Window.Run();
            graphicsManager.Device.WaitForIdle();

        }
    }
}