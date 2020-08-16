using Reload.Core;
using Reload.Core.Exceptions;
using Reload.Core.Game;
using Reload.Editor.Platform;
using Reload.Platform.Audio.OpenAl;
using Reload.Platform.Graphics.OpenGl;
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

            game.Initilize();
            game.Run();
            game.ShutDown();
        }

        /// <summary>
        /// Builds the game for the the operating system currently running.
        /// </summary>
        /// <returns>A GameSystem.</returns>
        public static GameSystem BuildGameForRuntimeOS()
        {
            GameBuilder<GameEditor> gameBuilder;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                gameBuilder = new GameBuilder<GameEditor>(new PlatformLinux());
                gameBuilder = gameBuilder
                    .WithGraphicsBackend<OpenGLBackend>()
                    .WithAudioBackend<OpenAl>();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                gameBuilder = new GameBuilder<GameEditor>(new PlatformWindows());
                gameBuilder = gameBuilder
                    .WithGraphicsBackend<OpenGLBackend>()
                    .WithAudioBackend<OpenAl>();
            }
            else
            {
                throw new ReloadUnsupporedOSPlatformException();
            }

            return gameBuilder
                    .WithWindow<MainWindow>()
                    .BuildForPlatform();

        }
    }
}
