namespace Engine.AssetPipeline.Audio
{
    using System.Collections.Generic;
    using System.IO;
    using Engine.AssetPipeline.Audio.Models;
    using Engine.Audio;

    public class AudioCache : IAudioCache
    {
        private readonly Dictionary<string, byte[]> _soundCache;

        public AudioCache()
        {
            _soundCache = new Dictionary<string, byte[]>();
        }

        public void CleanUp()
        {
            foreach (var sound in _soundCache)
            {
                _soundCache.Remove(sound.Key);
            }
        }

        public IMusic LoadMusic(string fullPath)
        {
            var audioSource = new AudioSource(File.OpenRead(fullPath));

            return new Music(audioSource);
        }

        public ISound LoadSound(string fullPath)
        {
            AudioSource audioSource;

            if (_soundCache.TryGetValue(fullPath, out var data))
            {
                audioSource = new AudioSource(new MemoryStream(data));
            }
            else
            {
                data = File.ReadAllBytes(fullPath);
                audioSource = new AudioSource(new MemoryStream(data));

                _soundCache.Add(fullPath, data);
            }

            return new Sound(audioSource);
        }
    }
}