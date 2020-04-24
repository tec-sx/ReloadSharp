namespace Engine.Audio
{
    using System.Collections.Generic;
    using System.IO;
    using Engine.Audio.Backend;

    public sealed class SoundManager
    {
        private Dictionary<string, byte[]> _soundCache;

        public SoundManager()
        {
            _soundCache = new Dictionary<string, byte[]>();
        }

        public AudioSource LoadSound(string fullSoundPath)
        {
            if (_soundCache.TryGetValue(fullSoundPath, out var data))
            {
                return new AudioSource(new MemoryStream(data));
            }
            else
            {
                data = File.ReadAllBytes(fullSoundPath);

                _soundCache.Add(fullSoundPath, data);

                return new AudioSource(new MemoryStream(data));
            }
        }

        public AudioSource LoadMusic(string fullMusicPath)
        {
            return new AudioSource(File.OpenRead(fullMusicPath));
        }

        public void DisposeResources()
        {
            foreach (var sound in _soundCache)
            {
                _soundCache.Remove(sound.Key);
            }
        }
    }
}
