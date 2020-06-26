namespace Reload.Editor
{
    using Reload.Graphics;
    using Silk.NET.Windowing.Common;
    using System.Drawing;

    class Program
    {
        public static GameEditor Editor;

        static void Main(string[] args)
        {
            Editor = new GameEditor(args);

            var configuration = new DisplayConfiguration
            {
                Resolution = new Point(1280, 720),
                RefreshRate = 60,
                TargetFps = 60,
                InFullScreen = false,
                EnableVSync = true,
                EnableVulkan = false,
                WindowTitle = "Reload Editor",
                WindowBorder = WindowBorder.Resizable,
                Position = new Point(100, 100)
            };

            Editor.CreateWindow(configuration);
            Editor.Run();
        }
    }
}
