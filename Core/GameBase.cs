using Core.Resources;

namespace Core
{
    using System;
    using Config;
    using Screen;
    using Audio;
    using State;
    using Microsoft.Extensions.DependencyInjection;
    using Raylib_cs;

    public abstract class GameBase : IDisposable
    {
        public static bool IsRunning { get; set; }
        
        private readonly ServiceProvider _serviceProvider;
        
        protected readonly IScreenManager screenManager;
        private ScreenBase _currentScreen;

        protected GameBase()
        {
            _serviceProvider = RegisterServices().BuildServiceProvider();
            screenManager = _serviceProvider.GetService<IScreenManager>();

            IsRunning = false;
        }

        protected abstract void OnInit();
        protected abstract void AddScreens();
        protected abstract void OnDispose();

        private ServiceCollection RegisterServices()
        {
            var collection = new ServiceCollection();

            collection.AddSingleton<IConfiguration, Configuration>();
            collection.AddSingleton<IAudioEngine, AudioEngine>();
            
            collection.AddSingleton<IScreenManager, ScreenManager>();
            collection.AddTransient(typeof(IStateMachine<>), typeof(StateMachine<>));
            collection.AddTransient<IResourceManager, ResourceManager>();

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

        public void Dispose()
        {
            OnDispose();
            
            Raylib.CloseWindow();
            _serviceProvider?.Dispose();
        }
    }
}