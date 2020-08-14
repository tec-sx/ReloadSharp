using Reload.Core;
using Reload.Core.Exceptions;
using Reload.Core.Game;
using Reload.Platform.OS.Linux;
using Reload.Platform.OS.Windows;
using System.Runtime.InteropServices;

namespace Reload.Editor
{
    class Program
    {
        static void Main(string[] args)
        {
            IPlatformOS platform;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                platform = new PlatformLinux();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                platform = new PlatformWindows();
            }
            else
            {
                throw new ReloadUnsupporedOSPlatformException();
            }

            GameSystem game = platform.BuildForPlatform<GameEditor>();

            game.Run();
            game.ShutDown();
        }
    }
}
