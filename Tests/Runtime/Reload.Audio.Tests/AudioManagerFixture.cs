namespace Reload.Audio.Tests
{
    using System;

    public class AudioManagerFixture : IDisposable
    {
        private readonly AudioManager _audioManager = new AudioManager();

        public void Dispose()
        {
            _audioManager.ShutDown();
        }
    }
}
