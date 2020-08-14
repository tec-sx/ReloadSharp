using Reload.Core;
using Reload.Core.Audio;
using Reload.Core.Game;
using Reload.Core.Graphics;

namespace Reload.Platform.OS.Windows
{
    /// <summary>
    /// The startup class for the Windows platfrorm
    /// </summary>
    public sealed class PlatformWindows : IPlatformOS
    {
        private readonly IGraphicsBackend _graphicsBackend;

        private readonly IAudioBackend _audioBackend;

        private readonly IGameWindow _window;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformWindows"/> class.
        /// </summary>
        public PlatformWindows()
        {

        }

        /// <inheritdoc/>
        public GameSystem BuildForPlatform<T>() where T : GameSystem, new()
        {
            return new GameBuilder<T>()
                .WithGraphicsBackend(_graphicsBackend)
                .WithAudioBackend(_audioBackend)
                .WithWindow(_window)
                .Build();
        }
    }
}
