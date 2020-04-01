namespace Core.Audio
{
    using System.IO;
    using Config;
    using System.Collections.Generic;
    using Raylib_cs;

    public class MusicStream
    {
        public Music Stream { get; set; }

        public void Play(int numOfLoops = -1)
        {
            Raylib.SetMusicLoopCount(Stream, numOfLoops);
            Raylib.PlayMusicStream(Stream);
        }

        public void Update() => Raylib.UpdateMusicStream(Stream);
        public void Pause() => Raylib.PauseMusicStream(Stream);
        public void Stop() => Raylib.StopMusicStream(Stream);
        public void Resume() => Raylib.ResumeMusicStream(Stream);
    }

    public class SoundEffect
    {
        public Sound Chunk { get; set; }

        public void Play()
        {
            Raylib.PlaySound(Chunk);
        }
    }

    public class AudioEngine : IAudioEngine
    {
        private readonly ApplicationSettings _settings;
        private readonly ContentPath _contentPath;

        private readonly Dictionary<string, Sound> _effectsList;
        private readonly Dictionary<string, Music> _musicList;

        public AudioEngine(IConfiguration configuration)
        {
            _settings = configuration.Settings;
            _contentPath = configuration.ContentPath;
            
            _effectsList = new Dictionary<string, Sound>();
            _musicList = new Dictionary<string, Music>();
        }

        public void Init()
        {
            Raylib.InitAudioDevice();
            Raylib.SetMasterVolume(_settings.Audio.MasterVolume);
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

            Raylib.CloseAudioDevice();
        }

        public SoundEffect LoadSound(string file)
        {
            string fullPath = Path.Combine(_contentPath.Music, $"{file}.{_settings.Audio.Format}");
            var sound = new SoundEffect();

            if (_effectsList.TryGetValue(fullPath, out var chunk))
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

        public MusicStream LoadMusic(string file)
        {
            var fullPath = Path.Combine(_contentPath.Music, $"{file}.{_settings.Audio.Format}");
            var music = new MusicStream();

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