using Core.AssetsPipeline;
using Core.AssetsPipeline.Audio;
using Core.AssetsPipeline.GameObjects;
using Core.AssetsPipeline.Textures;
using Core.CoreSystem.Audio;
using Core.CoreSystem.Audio.Device;
using Core.ResourcesPipeline;
using Core.ResourcesPipeline.Shaders;

namespace Core.Utilities
{
    using CoreSystem.Graphics;
    using CoreSystem;
    using Resources;
    using Resources.Textures;
    using Screen;
    using State;
    using Silk.NET.Windowing.Common;
    using Silk.NET.Windowing;
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
            #endregion
            
            #region Resources Pipeline

            collection.AddScoped<IShaderCache, ShaderCache>();
            collection.AddScoped<IResourcesManager, ResourcesManager>();
            #endregion
            
            #region Assets Pipeline
            
            collection.AddScoped<ITextureCache, TextureCache>();
            collection.AddScoped<IGameObjectCache, GameObjectCacheRl>();
            collection.AddScoped<IAudioCache, AudioCache>();
            collection.AddScoped<IAssetsManager, AssetsManager>();
            #endregion
            
            collection.AddSingleton<IScreenManager, ScreenManager>();
            collection.AddScoped(typeof(IStateMachine<>), typeof(StateMachine<>));

            _serviceProvider = collection.BuildServiceProvider();
        }

        public static void DisposeServices()
        {
            _serviceProvider?.DisposeAsync();
        }

        public static T GetService<T>() => _serviceProvider.GetService<T>();
    }
}