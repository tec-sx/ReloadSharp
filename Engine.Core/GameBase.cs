namespace Engine.Core
{
    using Engine.Graphics;
    using Engine.Audio;
    using Engine.Input;
    using Engine.Configuration;
    using Scene;
    using Engine.AssetPipeline;
    using Silk.NET.Windowing.Common;

    public abstract class GameBase
    {
        private readonly IConfigurationManager configurationManager;
        private readonly IGraphicsManager graphicsManager;
        private readonly IInputManager inputManager;
        private readonly IAudioManager audioManager;

        protected readonly IWindow window;
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
        protected abstract void OnCleanUp();

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

        private void CleanUp()
        {
            OnCleanUp();

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

            graphicsManager.Window.Run();
            graphicsManager.Device.WaitForIdle();

            graphicsManager.Window.Load += LoadContent;
            graphicsManager.Window.Update += Update;
            graphicsManager.Window.Render += Render;
            graphicsManager.Window.Closing += CleanUp;

            sceneManager.ExitProgram += graphicsManager.Window.Close;
        }
    }
}