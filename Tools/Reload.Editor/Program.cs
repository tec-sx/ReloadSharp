using Reload.Core;
using Reload.Core.Exceptions;
using Reload.Core.Game;
using Reload.Platform.OS.Linux;
using Reload.Platform.OS.Windows;
using System.Runtime.InteropServices;

namespace Reload.Editor
{
    /// <summary>
    /// The main program.
    /// </summary>
    class Program
    {
        /// <summary>
        /// The main entry point.
        /// </summary>
        /// <param name="args">The args.</param>
        static void Main(string[] args)
        {

            GameSystem game = BuildGameForRuntimeOS();

            game.Run();
            game.ShutDown();
        }

        /// <summary>
        /// Builds the game for the the operating system currently running.
        /// </summary>
        /// <returns>A GameSystem.</returns>
        public static GameSystem BuildGameForRuntimeOS()
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

            return platform.BuildForPlatform<GameEditor>();
        }
    }
}
