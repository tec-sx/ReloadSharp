using Reload.Core;
using Reload.Core.Configuration;
using Reload.Platform.Audio.OpenAl;
using Reload.Platform.Graphics.OpenGl;

#if Linux
using Reload.Platform.OS.Linux;
#elif Windows
using Reload.Platform.OS.Windows;
#endif

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
            PlatformOS platform;
#if Linux
            platform = new PlatformLinux()
                    .RegisterMainProgram<GameEditor>()
                    .WithConfiguration<SystemConfiguration>()
                    .WithWindow<DefaultViewport>()
                    .WithGraphicsAPI<OpenGlAPI>()
                    .WithAudioAPI<OpenAl>();
#elif Windows
            
                platform = new PlatformWindows()
                    .RegisterMainProgram<GameEditor>()
                    .WithConfiguration<SystemConfiguration>()
                    .WithWindow<DefaultViewport>()
                    .WithGraphicsAPI<OpenGlAPI>()
                    .WithAudioAPI<OpenAl>();
#else
            throw new ReloadUnsupporedOSPlatformException();
#endif

            platform.ConfigureAndBuild();
            platform.Run();
        }
    }
}
