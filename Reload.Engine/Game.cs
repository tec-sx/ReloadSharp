namespace Reload.Engine
{
    using Reload.Core.Commands;
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
    using Reload.Input;
    using System;
    using Silk.NET.Windowing.Common;
    using Reload.DataAccess;
    using Microsoft.Extensions.DependencyInjection;

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
        public ISceneManager SceneManager { get; }

        public UserInterfaceManager UserInterfaceManager { get; }

        protected abstract void OnInitialize();
        protected abstract void OnLoadContent();
        protected abstract void OnUpdate(double deltaTime);
        protected abstract void OnRender(double deltaTime);
        protected abstract void OnShutDown();

        public Game(string[] args) : base(args)
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
                .AddSingleton<ISceneManager, SceneManager>()
                .AddTransient<UserInterfaceManager>()

                #endregion

                .BuildServiceProvider();


            ConfigurationManager = SubSystems.GetService<ConfigurationManager>();
            GraphicsManager = SubSystems.GetService<GraphicsManager>();
            InputManager = SubSystems.GetService<InputManager>();
            AudioManager = SubSystems.GetService<AudioManager>();

            AssetsManager = SubSystems.GetService<IAssetsManager>();
            SceneManager = SubSystems.GetService<ISceneManager>();

            UserInterfaceManager = SubSystems.GetService<UserInterfaceManager>();
        }

        private void Initialize()
        {
            OnInitialize();

            Window = GraphicsManager.CreateWindow(ConfigurationManager.CreateDisplayConfiguration());

            AudioManager.Initialize();
            AssetsManager.Initialize(ConfigurationManager.CreateAssetsConfiguration());

            Window.Load += GraphicsManager.SetupOpenGl;
            Window.Load += InputManager.Load;
            Window.Load += UserInterfaceManager.Load;
            Window.Load += OnLoadContent;

            IsLoaded = true;

            Window.Update += OnUpdate;
            Window.Update += SceneManager.Update;
            Window.Update += UserInterfaceManager.Update;

            Window.Resize += UserInterfaceManager.Resize;

            Window.Render += OnRender;
            Window.Render += SceneManager.Render;
            Window.Render += UserInterfaceManager.Render;

            Window.Closing += ShutDownSubSystems;
            SceneManager.ExitProgram += Window.Close;
        }

        public override void Run()
        {
            Initialize();

            SceneManager.Run();
            Window.Run();

            ShutDownSubSystems();
        }

        private void ShutDownSubSystems()
        {
            OnShutDown();

            AssetsManager.ShutDown();
            AudioManager.ShutDown();

            UserInterfaceManager.ShutDown();
            InputManager.ShutDown();
            GraphicsManager.ShutDown();
        }
    }
}