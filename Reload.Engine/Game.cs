namespace Reload.Engine
{
    using Reload.UI;
    using Reload.AssetPipeline;
    using Reload.AssetPipeline.Audio;
    using Reload.AssetPipeline.GameObjects;
    using Reload.AssetPipeline.Textures;
    using Reload.Audio;
    using Reload.Configuration;
    using Reload.Graphics;
    using Reload.Scene;
    using Reload.Game;
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using System.Drawing;
    using Reload.Engine.Input;
    using Reload.Game.Scenes;

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

        public TaskManager TaskManager { get; set; }

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
        public SceneManager SceneManager { get; }

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
                .AddSingleton<TaskManager>()

                #endregion

                #region Simultaion sub-systems

                .AddSingleton<ITextureCache, TextureCache>()
                .AddSingleton<IGameObjectCache, GameObjectCache>()
                .AddSingleton<IAudioCache, AudioCache>()
                .AddSingleton<IAssetsManager, AssetsManager>()
                .AddSingleton<UiManager>()
                .AddSingleton<SceneManager>()

                #endregion

                .BuildServiceProvider();


            ConfigurationManager = SubSystems.GetService<ConfigurationManager>();
            GraphicsManager = SubSystems.GetService<GraphicsManager>();
            InputManager = SubSystems.GetService<InputManager>();
            AudioManager = SubSystems.GetService<AudioManager>();
            TaskManager = SubSystems.GetService<TaskManager>();

            AssetsManager = SubSystems.GetService<IAssetsManager>();
            SceneManager = SubSystems.GetService<SceneManager>();

            UiManager = SubSystems.GetService<UiManager>();

            Window = GraphicsManager.CreateWindow(ConfigurationManager.CreateDisplayConfiguration());
            AttachHandlers();

        }

        public void AttachHandlers()
        {
            // Will detach upon invoking.
            Window.Load += OnWindowLoad;

            Window.Resize += OnWindowResize;
            Window.Update += OnWindowUpdate;
            //Window.Render += TaskManager.Render;
            Window.Render += OnWindowRender;
            Window.Closing += ShutDownSubSystems;

            SceneManager.ExitProgram += Window.Close;
        }

        public void DetachHandlers()
        {
            SceneManager.ExitProgram -= Window.Close;
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

            GraphicsManager.SetupOpenGl();
            AudioManager.Initialize();
            InputManager.Initialize(Window);
            AssetsManager.Initialize(ConfigurationManager.CreateAssetsConfiguration());
            UiManager.Initilize();

            OnInitialize();
            OnLoadContent();

            SceneManager.Run();
        }

        private void OnWindowResize(Size size)
        {
            GraphicsManager.Gl.Viewport(size);
        }

        private void OnWindowUpdate(double deltaTime)
        {
            OnUpdate(deltaTime);
            UiManager.Update(deltaTime);
            InputManager.Update();
            SceneManager.Update(deltaTime);
        }

        private void OnWindowRender(double deltaTime)
        {
            OnRender(deltaTime);
            SceneManager.Render(deltaTime);
            UiManager.Render(deltaTime);
        }

        private void ShutDownSubSystems()
        {
            DetachHandlers();
            OnShutDown();

            AssetsManager.ShutDown();
            AudioManager.ShutDown();

            UiManager.Dispose();
            InputManager.Dispose();
            GraphicsManager.Dispose();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}