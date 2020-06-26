namespace Reload.Engine
{
    using Reload.Gameplay;
    using Reload.Engine.SceneSystem;
    using Reload.UI;
    using Reload.Assets;
    using Reload.Assets.Audio;
    using Reload.Assets.GameObjects;
    using Reload.Assets.Textures;
    using Reload.Audio;
    using Reload.Configuration;
    using Reload.Graphics;
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using System.Drawing;
    using Reload.Input;
    using Reload.Rendering;
    using Reload.Platform.Graphics.OpenGl;

    public abstract class Game : GameBase
    {
        /// <summary>
        /// Static event that will be fired when a game is initialized
        /// </summary>
        public static event Action GameStarted;

        /// <summary>
        /// Static event that will be fired when a game is destroyed
        /// </summary>
        public static event Action GameDestroyed;


        #region Sub system properties

        public ServiceProvider SubSystems;

        /// <summary>
        /// Configuration manager.
        /// </summary>
        public ConfigurationManager ConfigurationManager { get; }

        /// <summary>
        /// Graphics manager.
        /// </summary>
        public GraphicsManager GraphicsManager { get; }

        /// <summary>
        /// Input manager.
        /// </summary>
        public InputManager InputManager { get; }

        /// <summary>
        /// Audio manager.
        /// </summary>
        public AudioManager AudioManager { get; }

        /// <summary>
        /// Assets manager.
        /// </summary>
        public IAssetsManager AssetsManager { get; }

        /// <summary>
        /// Game scene manager.
        /// </summary>
        public SceneMachine SceneMachine { get; }

        public UiManager UiManager { get; }

        #endregion

        protected abstract void OnInitialize();
        protected abstract void OnLoadContent();
        protected abstract void OnUpdate(double deltaTime);
        protected abstract void OnRender(double deltaTime);
        protected abstract void OnShutDown();

        protected Game(string[] args) : base(args)
        {
            SubSystems = new ServiceCollection()

                #region Core sub-systems

                .AddSingleton(this as IGame)
                .AddSingleton<ConfigurationManager>()
                .AddSingleton<GraphicsManager>()
                .AddSingleton<InputManager>()
                .AddSingleton<AudioManager>()

                #endregion

                #region Simultaion sub-systems

                .AddSingleton<ITextureCache, TextureCache>()
                .AddSingleton<IGameObjectCache, GameObjectCache>()
                .AddSingleton<IAudioCache, AudioCache>()
                .AddSingleton<IAssetsManager, AssetsManager>()
                .AddSingleton<UiManager>()
                .AddSingleton<SceneMachine>()

                #endregion

                .BuildServiceProvider();


            ConfigurationManager = SubSystems.GetService<ConfigurationManager>();
            GraphicsManager = SubSystems.GetService<GraphicsManager>();
            InputManager = SubSystems.GetService<InputManager>();
            AudioManager = SubSystems.GetService<AudioManager>();

            AssetsManager = SubSystems.GetService<IAssetsManager>();
            SceneMachine = SubSystems.GetService<SceneMachine>();

            UiManager = SubSystems.GetService<UiManager>();
        }

        public void CreateWindow()
        {
            CreateWindow(ConfigurationManager.CreateDefaultDisplayConfiguration());
        }

        public void CreateWindow(DisplayConfiguration configuration)
        {
            Window = GraphicsManager.CreateWindow(configuration);
            AttachHandlers();
        }

        public void AttachHandlers()
        {
            // Will detach upon invoking.
            Window.Load += OnWindowLoad;

            Window.Resize += OnWindowResize;
            Window.Update += OnWindowUpdate;
            Window.Render += OnWindowRender;
            Window.Closing += ShutDownSubSystems;

            SceneMachine.ExitProgram += Window.Close;
        }

        public void DetachHandlers()
        {
            SceneMachine.ExitProgram -= Window.Close;
            Window.Closing -= ShutDownSubSystems;
            Window.Render -= OnWindowRender;
            Window.Update -= OnWindowUpdate;
            Window.Resize -= OnWindowResize;
        }

        public override void Run()
        {
            Window.Initialize();

            while (!Window.IsClosing)
            {
                Window.DoEvents();

                if (!Window.IsClosing)
                {
                    Window.DoUpdate();
                    Window.DoRender();
                    Window.SwapBuffers();
                }
            }

            Window.DoEvents();
            Window.Reset();
        }

        private void OnWindowLoad()
        {
            Window.Load -= OnWindowLoad;

            var glContext = new GlContext(Window);

            Renderer.Initialize();
            InputManager.Initialize(Window);
            AssetsManager.Initialize(ConfigurationManager.CreateAssetsConfiguration());
            UiManager.Initilize(glContext.Api, Window, InputManager.Context);

            OnInitialize();
            OnLoadContent();

            SceneMachine.Run();
        }

        private void OnWindowResize(Size size)
        {
            Renderer.OnWindowResize(size);
        }

        private void OnWindowUpdate(double deltaTime)
        {
            OnUpdate(deltaTime);
            InputManager.Update();
        }

        private void OnWindowRender(double deltaTime)
        {
            OnRender(deltaTime);
            
        }

        private void ShutDownSubSystems()
        {
            DetachHandlers();
            OnShutDown();

            AssetsManager.ShutDown();
            AudioManager.ShutDown();

            //UiManager.Dispose();
            InputManager.Dispose();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}