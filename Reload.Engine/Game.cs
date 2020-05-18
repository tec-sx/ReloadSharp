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
    using SimpleInjector;
    using System;
    using Silk.NET.Windowing.Common;
    using System.Collections.Generic;
    using Reload.Core;
    using Reload.DataAccess;
    using Microsoft.EntityFrameworkCore;

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

        public Container SubSystems;

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

        protected abstract void OnInitialize();
        protected abstract void OnLoadContent();
        protected abstract void OnUpdate(double deltaTime);
        protected abstract void OnRender(double deltaTime);
        protected abstract void OnShutDown();

        public Game()
        {
            SubSystems = new Container();

            SubSystems.RegisterInstance(this as IGame);

            #region Core sub-systems

            SubSystems.RegisterSingleton<IConfigurationManager, ConfigurationManager>();
            SubSystems.RegisterSingleton<IGraphicsManager, GraphicsManager>();
            SubSystems.RegisterSingleton<InputManager>();
            SubSystems.RegisterSingleton<IAudioManager, AudioManager>();
            #endregion

            #region Simultaion sub-systems

            SubSystems.RegisterSingleton<ITextureCache, TextureCache>();
            SubSystems.RegisterSingleton<IGameObjectCache, GameObjectCache>();
            SubSystems.RegisterSingleton<IAudioCache, AudioCache>();

            SubSystems.RegisterSingleton<IAssetsManager, AssetsManager>();
            SubSystems.RegisterSingleton<ISceneManager, SceneManager>();
            #endregion

            SubSystems.Verify();

            ConfigurationManager = SubSystems.GetInstance<ConfigurationManager>();
            GraphicsManager = SubSystems.GetInstance<GraphicsManager>();
            InputManager = SubSystems.GetInstance<InputManager>();
            AudioManager = SubSystems.GetInstance<AudioManager>();

            AssetsManager = SubSystems.GetInstance<IAssetsManager>();
            SceneManager = SubSystems.GetInstance<ISceneManager>();

            PersistentDb = SubSystems.GetInstance<PersistentDb>();
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
        }

        private void LoadContent()
        {
            OnLoadContent();

            SceneManager.ActiveScene.OnEnter();
            SceneManager.ActiveScene.Run();

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
