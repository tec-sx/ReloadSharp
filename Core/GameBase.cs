

namespace Core
{
    using System;
    using Config;
    using Screen;
    using Audio;
    using State;
    using Resources;
    using Resources.Textures;
    using Resources.GameObjects;
    using Resources.Audio;
    using Microsoft.Extensions.DependencyInjection;
    using Raylib_cs;

    public abstract class GameBase : IDisposable
    {
        public static bool IsRunning { get; set; }

        private readonly ServiceProvider _serviceProvider;

        private readonly IAudioEngine _audioEngine;

        protected readonly IScreenManager screenManager;

        private ScreenBase _currentScreen;

        protected GameBase()
        {
            _serviceProvider = RegisterServices().BuildServiceProvider();

            _audioEngine = _serviceProvider.GetService<IAudioEngine>();

            screenManager = _serviceProvider.GetService<IScreenManager>();


            IsRunning = false;
        }

        public void Dispose()
        {
            OnDispose();


            Raylib.CloseWindow();
            _audioEngine?.Dispose();
            _serviceProvider?.Dispose();
        }

        protected abstract void OnInit();
        protected abstract void AddScreens();
        protected abstract void OnDispose();

        private ServiceCollection RegisterServices()
        {
            var collection = new ServiceCollection();

            #region System

            collection.AddSingleton<IConfiguration, Configuration>();
            collection.AddSingleton<IAudioEngine, AudioEngine_RL>();

            #endregion

            #region Resources

            collection.AddScoped<ITextureCache, TextureCache_RL>();
            collection.AddScoped<IGameObjectCache, GameObjectCache_RL>();
            collection.AddScoped<IAudioCache, AudioCache_RL>();
            collection.AddScoped<IResourceManager, ResourceManager>();

            #endregion

            #region Gameplay

            collection.AddSingleton<IScreenManager, ScreenManager>();
            collection.AddScoped(typeof(IStateMachine<>), typeof(StateMachine<>));

            #endregion

            return collection;
        }

        private bool Init()
        {
            var settings = _serviceProvider.GetService<IConfiguration>().Settings;

            ConfigFlag flags = 0;
            Array.ForEach(settings.Display.Flags, flag => flags |= flag);
            Raylib.SetConfigFlags(flags);

            Raylib.InitWindow(
                settings.Display.Width,
                settings.Display.Height,
                settings.Info.WindowTitle);

            Raylib.SetTargetFPS(settings.Display.TargetFps);

            _audioEngine.Init();

            OnInit();
            AddScreens();

            _currentScreen = screenManager.CurrentScreen;
            _currentScreen?.Run();
            _currentScreen?.OnEnter();

            return true;
        }

        private void Update(double deltaTime)
        {
            screenManager.Update();
        }

        private void Render()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.BLACK);

            _currentScreen?.Render();

            Raylib.DrawFPS(700, 15);

            Raylib.EndDrawing();
        }

        public void Run()
        {
            if (Init())
            {
                IsRunning = true;
            }

            while (!Raylib.WindowShouldClose())
            {
                Update(Raylib.GetFrameTime());

                if (_currentScreen != null && _currentScreen.State == ScreenState.RUNNING)
                {
                    _currentScreen.Render();
                }

                Render();
            }
        }
    }
}