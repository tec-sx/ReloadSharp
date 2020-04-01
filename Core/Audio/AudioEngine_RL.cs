namespace Core.Audio
{
    using Config;
    using Raylib_cs;


    public class AudioEngine_RL : IAudioEngine
    {
        private readonly ApplicationSettings _settings;

        public AudioEngine_RL(IConfiguration configuration)
        {
            _settings = configuration.Settings;
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