using Reload.Core.Commands;

namespace Reload.Engine
{
    using global::Engine.AssetPipeline;
    using global::Engine.AssetPipeline.Audio;
    using global::Engine.AssetPipeline.GameObjects;
    using global::Engine.AssetPipeline.Textures;
    using global::Engine.Audio;
    using global::Engine.Configuration;
    using global::Engine.Graphics;
    using global::Engine.Scene;
    using Reload.Game;
    using Reload.Input;
    using System;
    using Silk.NET.Windowing.Common;
    using Reload.DataAccess;
    using global::Engine.GUI;
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
        public IConfigurationManager ConfigurationManager { get; }

        /// <summary>
        /// Graphics manager.
        /// </summary>
        public IGraphicsManager GraphicsManager { get; }

        /// <summary>
        /// Input manager.
        /// </summary>
        public InputManager InputManager { get; }

        /// <summary>
        /// Audio manager.
        /// </summary>
        public IAudioManager AudioManager { get; }

        /// <summary>
        /// Assets manager.
        /// </summary>
        public IAssetsManager AssetsManager { get; }

        /// <summary>
        /// Game scene manager.
        /// </summary>
        public ISceneManager SceneManager { get; }

        /// <summary>
        /// Persistent Data Access.
        /// </summary>
        public PersistentDb PersistentDb { get; }

        /// <summary>
        /// In memory Data Access.
        /// </summary>
        public InMemoryDb InMemoryDb { get; }

        public App UiApp { get; set; } = new App();

        protected abstract void OnInitialize();
        protected abstract void OnLoadContent();
        protected abstract void OnUpdate(double deltaTime);
        protected abstract void OnRender(double deltaTime);
        protected abstract void OnShutDown();

        public Game()
        {

            SubSystems = new ServiceCollection()
            #region Core sub-systems
                .AddSingleton(this as IGame)
                .AddSingleton<IConfigurationManager, ConfigurationManager>()
                .AddSingleton<IGraphicsManager, GraphicsManager>()
                .AddSingleton<InputManager>()
                .AddSingleton<IAudioManager, AudioManager>()
            #endregion
            #region Simultaion sub-systems
                .AddSingleton<ITextureCache, TextureCache>()
                .AddSingleton<IGameObjectCache, GameObjectCache>()
                .AddSingleton<IAudioCache, AudioCache>()

                .AddSingleton<IAssetsManager, AssetsManager>()
                .AddSingleton<ISceneManager, SceneManager>()
            #endregion

                .BuildServiceProvider();


            ConfigurationManager = SubSystems.GetService<IConfigurationManager>();
            GraphicsManager = SubSystems.GetService<IGraphicsManager>();
            InputManager = SubSystems.GetService<InputManager>();
            AudioManager = SubSystems.GetService<IAudioManager>();

            AssetsManager = SubSystems.GetService<IAssetsManager>();
            SceneManager = SubSystems.GetService<ISceneManager>();

            PersistentDb = SubSystems.GetService<PersistentDb>();
        }

        private void Initialize()
        {
            OnInitialize();

            Window = GraphicsManager.CreateWindow(ConfigurationManager.CreateDisplayConfiguration());

            InputManager.Initialize();
            AudioManager.Initialize();
            AssetsManager.Initialize(ConfigurationManager.CreateAssetsConfiguration());

            Window.Load += LoadContent;
            Window.Update += Update;
            Window.Render += Render;
            Window.Closing += ShutDownSubSystems;
            SceneManager.ExitProgram += Window.Close;

            UiApp.Run();
        }

        private void LoadContent()
        {
            OnLoadContent();
            SceneManager.Run();

            IsLoaded = true;
        }

        private void Update(double deltaTime)
        {
            OnUpdate(deltaTime);

            SceneManager.Update(deltaTime);
        }

        private void Render(double deltaTime)
        {
            OnRender(deltaTime);

            SceneManager.Render(deltaTime);
        }

        public override void Run()
        {
            Initialize();

            Window.Run();

            ShutDownSubSystems();
        }

        private void ShutDownSubSystems()
        {
            OnShutDown();

            AssetsManager.ShutDown();
            AudioManager.ShutDown();
        }
    }
}
