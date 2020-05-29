namespace Reload.AssetPipeline.Audio
{
    using Reload.AssetPipeline.Audio.Models;
    using Reload.Audio;
    using System.Collections.Generic;
    using System.IO;

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

        public IMusic LoadMusic(string fullPath)
        {
            var audioSource = new AudioSource(File.OpenRead(fullPath));

            return new Music(audioSource);
        }

        public ISound LoadSound(string fullPath)
        {
            AudioSource audioSource;

            if (soundCache.TryGetValue(fullPath, out var data))
            {
                audioSource = new AudioSource(new MemoryStream(data));
            }
            else
            {
                data = File.ReadAllBytes(fullPath);
                audioSource = new AudioSource(new MemoryStream(data));

                soundCache.Add(fullPath, data);
            }

            return new Sound(audioSource);
        }
    }
}