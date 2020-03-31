namespace Core.Graphics
{
    using System;
    using Config;
    using Config.Models;
    using Raylib_cs;
    
    public class Window : IWindow
    {
        private readonly ApplicationSettings _settings;

        public Window(IConfiguration configuration)
        {
            _settings = configuration.Settings;
        }
        
        public void Create()
        {
            ConfigFlag flags = 0;
            Array.ForEach(_settings.Display.Flags, flag => flags |= flag);
            Raylib.SetConfigFlags(flags);
            
            Raylib.InitWindow(
                _settings.Display.Width, 
                _settings.Display.Height, 
                _settings.Info.WindowTitle);
            
            Raylib.SetTargetFPS(_settings.Display.TargetFps);
        }

        public void Dispose()
        {
            Raylib.CloseWindow();
        }
    }
}