﻿using Reload.Core.Commands;
using Reload.UI;

namespace Reload.Engine
{
    using global::Reload.AssetPipeline;
    using global::Reload.AssetPipeline.Audio;
    using global::Reload.AssetPipeline.GameObjects;
    using global::Reload.AssetPipeline.Textures;
    using global::Reload.Audio;
    using global::Reload.Configuration;
    using global::Reload.Graphics;
    using global::Reload.Scene;
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
        public IConfigurationManager ConfigurationManager { get; }

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
        public IAudioManager AudioManager { get; }

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
                .AddSingleton<IConfigurationManager, ConfigurationManager>()
                .AddSingleton<GraphicsManager>()
                .AddSingleton<InputManager>()
                .AddSingleton<IAudioManager, AudioManager>()

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


            ConfigurationManager = SubSystems.GetService<IConfigurationManager>();
            GraphicsManager = SubSystems.GetService<GraphicsManager>();
            InputManager = SubSystems.GetService<InputManager>();
            AudioManager = SubSystems.GetService<IAudioManager>();

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

            Window.Load += OnWindowLoad;
            Window.Update += Update;
            Window.Render += Render;
            Window.Closing += ShutDownSubSystems;
            SceneManager.ExitProgram += Window.Close;
        }

        private void OnWindowLoad()
        {
            InputManager.Load();
            UserInterfaceManager.Load();

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
            UserInterfaceManager.Render();
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
            
            UserInterfaceManager.ShutDown();
            InputManager.ShutDown();
            GraphicsManager.ShutDown();
        }
    }
}