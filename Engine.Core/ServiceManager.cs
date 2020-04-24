using Core.AssetsPipeline;
using Core.AssetsPipeline.Audio;
using Core.AssetsPipeline.GameObjects;
using Core.AssetsPipeline.Textures;
using Core.CoreSystem.Audio;
using Core.CoreSystem.Audio.Device;
using Core.CoreSystem.Input;
using Core.GamePlay;
using Core.ResourcesPipeline;
using Core.ResourcesPipeline.Shaders;

namespace Core.Utilities
{
    using CoreSystem.Graphics;
    using Screen;
    using State;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceManager
    {
        private static ServiceProvider _serviceProvider;

        public static void RegisterServices()
        {
            var collection = new ServiceCollection();

            #region Core System

            collection.AddSingleton<GraphicsManager>();
            collection.AddSingleton<AudioManager>();
            collection.AddSingleton<InputManager>();
            #endregion

            #region Resources Pipeline

            collection.AddScoped<IResourcesManager, ResourcesManager>();
            #endregion

            #region Assets Pipeline

            collection.AddScoped<ITextureCache, TextureCache>();
            collection.AddScoped<IGameObjectCache, GameObjectCacheRl>();
            collection.AddScoped<IAudioCache, AudioCache>();
            collection.AddScoped<IAssetsManager, AssetsManager>();
            #endregion

            #region Gameplay

            collection.AddSingleton<PlayerAction>();
            collection.AddSingleton<ScreenManager>();
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