namespace Core.AssetsPipeline.Audio
{
    using System;
    using System.Collections.Generic;
    using Models;
    using CoreSystem.ErrorHandling.Exceptions;
    using static SDL2.SDL_mixer;

    public class AudioCache : IAudioCache
    {
        private readonly Dictionary<string, IntPtr> _effectsDictionary;
        private readonly Dictionary<string, IntPtr> _musicDictionary;

        public AudioCache()
        {
            _effectsDictionary = new Dictionary<string, IntPtr>();
            _musicDictionary = new Dictionary<string, IntPtr>();
        }

        public void Dispose()
        {
            foreach (var (key, value) in _musicDictionary)
            {
                Mix_FreeMusic(value);
                _musicDictionary.Remove(key);
            }

            foreach (var (key, value) in _effectsDictionary)
            {
                Mix_FreeChunk(value);
                _effectsDictionary.Remove(key);
            }
        }

        public IMusic LoadMusic(string fullPath)
        {
            var music = new MusicSdl();

            if (_musicDictionary.TryGetValue(fullPath, out var stream))
            {
                music.Stream = stream;
            }
            else
            {
                music.Stream = Mix_LoadMUS(fullPath);

                if (music.Stream == null)
                {
                    throw new MusicException($"{fullPath} can't be loaded.");
                }

                _musicDictionary.Add(fullPath, music.Stream);
            }

            return music;
        }

        public ISound LoadSound(string fullPath)
        {
            var sound = new SoundSdl();

            if (!_effectsDictionary.TryGetValue(fullPath, out var chunk))
            {
                sound.Chunk = chunk;
            }
            else
            {
                sound.Chunk = Mix_LoadWAV(fullPath);

                if (sound.Chunk == null)
                {
                    throw new SoundException($"{fullPath} can't be loaded.");
                }

                _effectsDictionary.Add(fullPath, sound.Chunk);
            }

            return sound;
        }
    }
}