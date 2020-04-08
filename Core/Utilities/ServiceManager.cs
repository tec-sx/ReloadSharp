namespace Core.Utilities
{
    using CoreSystem.Graphics;
    using CoreSystem;
    using Resources;
    using Resources.Audio;
    using Resources.GameObjects;
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
            
            collection.AddSingleton<AudioDevice>();

            collection.AddScoped<ITextureCache, TextureCacheRl>();
            collection.AddScoped<IGameObjectCache, GameObjectCacheRl>();
            collection.AddScoped<IAudioCache, AudioCache>();
            collection.AddScoped<IAssetsManager, AssetsManager>();

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