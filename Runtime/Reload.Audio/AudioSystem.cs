using Reload.Core.Audio;
using Reload.Core.Game;
using Silk.NET.OpenAL;
using System.Collections.Generic;
using System.IO;

namespace Reload.Audio
{
    public sealed class AudioSystem : ISubSystem
    {
        private readonly IAudioBackend _backend;

        public AudioContext Context { get; init; }

        public AudioSystem(IAudioBackend backend)
        {
            _backend = backend;
            _backend.SetDistanceModel(DistanceModel.InverseDistanceClamped);

            Context = new AudioContext();
            soundCache = new Dictionary<string, byte[]>();
        }

        private readonly Dictionary<string, byte[]> soundCache;

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

        public void Initialize()
        {
            
        }

        public void ShutDown()
        {
        }
    }
}
