using System.Drawing;
using System.Threading;

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
        public SceneManager SceneManager { get; }

        public UiManager UiManager { get; }

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
                .AddSingleton<SceneManager>()

                #endregion

                .BuildServiceProvider();


            ConfigurationManager = SubSystems.GetService<ConfigurationManager>();
            GraphicsManager = SubSystems.GetService<GraphicsManager>();
            InputManager = SubSystems.GetService<InputManager>();
            AudioManager = SubSystems.GetService<AudioManager>();

            AssetsManager = SubSystems.GetService<IAssetsManager>();
            SceneManager = SubSystems.GetService<SceneManager>();

            UiManager = SubSystems.GetService<UiManager>();
        }

        public override void Run()
        {
            Window = GraphicsManager.CreateWindow(ConfigurationManager.CreateDisplayConfiguration());

            Window.Load += OnWindowLoad;
            Window.Resize += OnWindowResize;
            Window.Update += OnWindowUpdate;
            Window.Render += OnWindowRender;

            OnInitialize();

            Window.Run();

            ShutDownSubSystems();
        }

        private void OnWindowLoad()
        {
            GraphicsManager.SetupOpenGl();
            AudioManager.Initialize();
            InputManager.Initialize();
            AssetsManager.Initialize(ConfigurationManager.CreateAssetsConfiguration());
            UiManager.Initilize();

            OnLoadContent();

            SceneManager.ExitProgram += Window.Close;
            SceneManager.Run();
        }

        private void OnWindowResize(Size size)
        {
            UiManager.Resize(size);
        }

        private void OnWindowUpdate(double deltaTime)
        {
            OnUpdate(deltaTime);
            UiManager.Update(deltaTime);
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
            OnShutDown();

            AssetsManager.ShutDown();
            AudioManager.ShutDown();

            UiManager.ShutDown();
            InputManager.ShutDown();
            GraphicsManager.ShutDown();
        }
    }
}