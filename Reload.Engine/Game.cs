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

    public class Game : GameBase
    {
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

        public Game()
        {
            SubSystems = new Container();

            SubSystems.RegisterInstance(this);

            #region Core sub-systems

            SubSystems.RegisterSingleton<IConfigurationManager, ConfigurationManager>();
            ConfigurationManager = SubSystems.GetInstance<IConfigurationManager>();

            SubSystems.RegisterSingleton<IGraphicsManager, GraphicsManager>();
            GraphicsManager = SubSystems.GetInstance<IGraphicsManager>();

            SubSystems.RegisterSingleton<InputManager>();
            InputManager = SubSystems.GetInstance<InputManager>();

            SubSystems.RegisterSingleton<IAudioManager, AudioManager>();
            AudioManager = SubSystems.GetInstance<IAudioManager>();
            #endregion

            #region Simultaion sub-systems

            SubSystems.RegisterSingleton<ITextureCache, TextureCache>();
            SubSystems.RegisterSingleton<IGameObjectCache, GameObjectCache>();
            SubSystems.RegisterSingleton<IAudioCache, AudioCache>();

            SubSystems.RegisterSingleton<IAssetsManager, AssetsManager>();
            AssetsManager = SubSystems.GetInstance<IAssetsManager>();

            SubSystems.RegisterSingleton<ISceneManager, SceneManager>();
            SceneManager = SubSystems.GetInstance<ISceneManager>();
            #endregion

            SubSystems.Verify();
        }

        protected override void OnInitialize()
        {
            GraphicsManager.CreateWindow(ConfigurationManager.CreateDisplayConfiguration());

            InputManager.Initialize(
                ConfigurationManager.CreateKeyboardConfiguration(),
                ConfigurationManager.CreateMouseConfiguration());

            AudioManager.Initialize();
            AssetsManager.Initialize(ConfigurationManager.CreateAssetsConfiguration());
        }

        protected override void OnLoadContent()
        {
            throw new NotImplementedException();
        }

        protected override void OnRender(double deltaTime)
        {
            throw new NotImplementedException();
        }

        protected override void OnShutDown()
        {
            AssetsManager.ShutDown();
            AudioManager.ShutDown();
        }

        protected override void OnUpdate(double deltaTime)
        {
            throw new NotImplementedException();
        }
    }
}
