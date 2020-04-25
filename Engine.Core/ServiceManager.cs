namespace Engine.Core
{
    using Engine.Graphics;
    using Engine.AssetPipeline;
    using Engine.AssetPipeline.Audio;
    using Engine.AssetPipeline.GameObjects;
    using Engine.AssetPipeline.Textures;
    using Engine.Audio;
    using Engine.Input;
    using Engine.GamePlay;
    using Engine.Scene;
    using State;
    using Microsoft.Extensions.DependencyInjection;
    using Engine.Configuration;

    public static class ServiceManager
    {
        private static ServiceProvider _serviceProvider;

        public static void RegisterServices()
        {
            var collection = new ServiceCollection();

            #region Core System
            collection.AddSingleton<ConfigurationManager>();
            collection.AddSingleton<GraphicsManager>();
            collection.AddSingleton<AudioManager>();
            collection.AddSingleton<InputManager>();
            collection.AddSingleton<IAssetsManager, AssetsManager>();
            collection.AddSingleton<SceneManager>();
            #endregion

            #region Assets Pipeline

            collection.AddScoped<ITextureCache, TextureCache>();
            collection.AddScoped<IGameObjectCache, GameObjectCache>();
            collection.AddScoped<IAudioCache, AudioCache>();
            #endregion

            #region Gameplay

            collection.AddSingleton<PlayerAction>();
            collection.AddScoped(typeof(IStateMachine<>), typeof(StateMachine<>));
            #endregion

            _serviceProvider = collection.BuildServiceProvider();
        }

        public static void DisposeServices()
        {
            _serviceProvider?.DisposeAsync();
        }

        public static T GetService<T>() => _serviceProvider.GetService<T>();
    }
}