using Reload.Core.Audio;
using System.Collections.Generic;
using System.IO;

namespace Reload.Assets.Audio
{
    public class AudioCache : IAudioCache
    {
        private readonly Dictionary<string, byte[]> soundCache;

        public AudioCache()
        {
            soundCache = new Dictionary<string, byte[]>();
        }

        public void CleanUp()
        {
            foreach (var sound in soundCache)
            {
                soundCache.Remove(sound.Key);
            }
        }

        public Music LoadMusic(string fullPath)
        {
            var audioSource = AudioSource.Create(File.OpenRead(fullPath));

            return new Music(audioSource);
        }

        public Sound LoadSound(string fullPath)
        {
            AudioSource audioSource;

            if (soundCache.TryGetValue(fullPath, out var data))
            {
                audioSource = AudioSource.Create(new MemoryStream(data));
            }
            else
            {
                data = File.ReadAllBytes(fullPath);
                audioSource = AudioSource.Create(new MemoryStream(data));

                soundCache.Add(fullPath, data);
            }

            return new Sound(audioSource);
        }
    }
}