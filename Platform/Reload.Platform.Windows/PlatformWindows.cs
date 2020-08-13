using Reload.Core;
using Reload.Core.Audio;
using Reload.Core.Game;
using Reload.Core.Graphics;

namespace Reload.Platform.Windows
{
    public class PlatformWindows<T> : IPlatform where T : GameSystem, new()
    {
        private GameSystem _game;

        private IGraphicsBackend _graphicsBackend;

        private IAudioBackend _audioBackend;

        private IGameWindow _window;

        public PlatformWindows()
        {
            _game = new GameBuilder<T>()
                .WithGraphicsBackend(_graphicsBackend)
                .WithAudioBackend(_audioBackend)
                .WithWindow(_window)
                .Build();
        }

        public void RunGame()
        {
            _game.Run();
        }
    }
}
