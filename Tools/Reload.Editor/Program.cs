using System;
using System.Runtime.InteropServices;
using NoesisApp;

namespace Reload.Editor
{
    using Reload.Graphics;
    using Silk.NET.Windowing.Common;
    using System.Drawing;

    class Program : Application
    {
        public static GameEditor Editor;

        static void Main(string[] args)
        {
            Noesis.GUI.Init("technosex", "oXYjGeM+Ixl5O54mBpZY2EhBB+eJRfw6BzyxfximuoesiBsP");
            
            Program program = new Program();
            program.Uri = "Program.xaml";
            program.Run();
            
            // Editor = new GameEditor(args);
            //
            // var configuration = new DisplayConfiguration
            // {
            //     Resolution = new Point(1280, 720),
            //     RefreshRate = 60,
            //     TargetFps = 60,
            //     InFullScreen = false,
            //     EnableVSync = true,
            //     EnableVulkan = false,
            //     WindowTitle = "Reload Editor",
            //     WindowBorder = WindowBorder.Resizable,
            //     Position = new Point(100, 100)
            // };
            //
            // Editor.CreateWindow(configuration);
            // Editor.Run();
        }

        protected override Display CreateDisplay()
        {
            if (RuntimeInformation.IsOSPlatform((OSPlatform.Linux)))
            {
                return new XDisplay();
            }
            if (RuntimeInformation.IsOSPlatform((OSPlatform.Windows)))
            {
                return new Win32Display();
            }
            
            throw new ApplicationException("OS platform not supported.");
        }

        protected override RenderContext CreateRenderContext()
        {
            if (RuntimeInformation.IsOSPlatform((OSPlatform.Linux)))
            {
                return new RenderContextGLX();
            }
            if (RuntimeInformation.IsOSPlatform((OSPlatform.Windows)))
            {
                return new RenderContextD3D11();
            }
            
            throw new ApplicationException("OS platform not supported.");
        }
    }
}
