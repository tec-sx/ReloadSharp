namespace Core.Resources.Audio
{
    using System.Collections.Generic;
    using Models;
    using Raylib_cs;

    public class AudioCacheRl : IAudioCache
    {
        private readonly Dictionary<string, Sound> _effectsList;
        private readonly Dictionary<string, Music> _musicList;

        public AudioCacheRl()
        {
            _effectsList = new Dictionary<string, Sound>();
            _musicList = new Dictionary<string, Music>();
        }

        public void Dispose()
        {
            foreach (var (key, value) in _musicList)
            {
                Raylib.UnloadMusicStream(value);
                _musicList.Remove(key);
            }

            foreach (var (key, value) in _effectsList)
            {
                Raylib.UnloadSound(value);
                _effectsList.Remove(key);
            }
        }

        public ISound LoadSound(string fullPath)
        {
            var sound = new Sound_RL();

            if (!_effectsList.TryGetValue(fullPath, out var chunk))
            {
                sound.Chunk = chunk;
            }
            else
            {
                sound.Chunk = Raylib.LoadSound(fullPath);
                _effectsList.Add(fullPath, sound.Chunk);
            }

            return sound;
        }

        public IMusic LoadMusic(string fullPath)
        {
            var music = new Music_RL();

            if (_musicList.TryGetValue(fullPath, out var stream))
            {
                music.Stream = stream;
            }
            else
            {
                music.Stream = Raylib.LoadMusicStream(fullPath);
                _musicList.Add(fullPath, music.Stream);
            }

            return music;
        }
    }


}
