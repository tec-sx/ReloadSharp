namespace Core
{
    using System;
    using Config;
    using Screen;
    using Graphics;
    using Audio;
    using State;
    using Microsoft.Extensions.DependencyInjection;
    using Raylib_cs;

    public abstract class GameBase : IDisposable
    {
        public static bool IsRunning { get; set; }
        
        private readonly ServiceProvider _serviceProvider;
        
        protected readonly IWindow Window;
        protected readonly IAudioEngine AudioEngine;
        protected readonly IScreenList ScreenList;
        protected ScreenBase CurrentScreen { get; set; }

        protected GameBase()
        {
            _serviceProvider = RegisterServices().BuildServiceProvider();
            
            Window = _serviceProvider.GetService<IWindow>();
            AudioEngine = _serviceProvider.GetService<IAudioEngine>();
            ScreenList = _serviceProvider.GetService<IScreenList>();

            IsRunning = false;
        }

        protected abstract void OnInit();
        protected abstract void AddScreens();
        protected abstract void OnDispose();

        private ServiceCollection RegisterServices()
        {
            var collection = new ServiceCollection();

            collection.AddTransient(typeof(IStateMachine<>), typeof(StateMachine<>));
            collection.AddSingleton<IConfiguration, Configuration>();
            collection.AddSingleton<IWindow, Window>();
            collection.AddSingleton<IAudioEngine, AudioEngine>();
            collection.AddSingleton<IScreenList, ScreenList>();

            return collection;
        }

        private bool Init()
        {
            Window.Create();

            OnInit();
            AddScreens();

            CurrentScreen = ScreenList.CurrentScreen;
            CurrentScreen?.Run();
            CurrentScreen?.OnEnter();

            return true;
        }
        
        private void Update(double deltaTime)
        {
            ScreenList.Update();
        }

        private void Render()
        {
            CurrentScreen?.Render();
            
            Raylib.DrawFPS(700, 15);
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.WHITE);
            Raylib.DrawText("Hello, world!", 12, 12, 20, Color.BLACK);
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

                if (CurrentScreen != null && CurrentScreen.State == ScreenState.RUNNING)
                {
                    CurrentScreen.Render();
                }
                
                Render();
            }
        }

        public void Dispose()
        {
            OnDispose();

            AudioEngine?.Dispose();
            Window?.Dispose();
            _serviceProvider?.Dispose();
        }
    }
}