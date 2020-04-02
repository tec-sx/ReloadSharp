namespace Core.CoreSystem.Audio
{
    using Config;
    using Raylib_cs;

    public class AudioEngineRl : IAudioEngine
    {
        private readonly ApplicationSettings _settings;

        public AudioEngineRl()
        {
            _settings = Configuration.Settings;
        }

        public void Init()
        {
            Raylib.InitAudioDevice();
            Raylib.SetMasterVolume(_settings.Audio.MasterVolume);
        }

        public void Dispose()
        {

            Raylib.CloseAudioDevice();
        }
    }
}