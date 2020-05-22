using Noesis;
using NoesisApp;
using System.Runtime.InteropServices;

namespace Engine.GUI
{


    public class App : Application
    {
        public App()
        {
            Uri = "App.xaml";
        }

        protected override Display CreateDisplay()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return new Win32Display();
            }
            else
            {
                return new XDisplay();
            }

        }

        protected override RenderContext CreateRenderContext()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return new RenderContextD3D11();
            }
            else
            {
                return new RenderContextGLX();
            }
        }
    }
}
