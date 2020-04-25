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
        private readonly ConfigurationManager configurationManager;
        private readonly GraphicsManager graphicsManager;
        private readonly InputManager inputManager;
        private readonly AudioManager audioManager;

        protected readonly SceneManager sceneManager;
        protected readonly IAssetsManager assetsManager;

        internal static bool isRunning;

        protected GameBase()
        {
            ServiceManager.RegisterServices();

            configurationManager = ServiceManager.GetService<ConfigurationManager>();
            graphicsManager = ServiceManager.GetService<GraphicsManager>();
            inputManager = ServiceManager.GetService<InputManager>();
            audioManager    = ServiceManager.GetService<AudioManager>();

            assetsManager = ServiceManager.GetService<IAssetsManager>();
            sceneManager   = ServiceManager.GetService<SceneManager>();
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

            graphicsManager.DisposeResources();
            audioManager.DisposeResources();

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