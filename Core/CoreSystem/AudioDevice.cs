namespace Core.CoreSystem
{
    using ErrorHandling.Exceptions;
    using static SDL2.SDL_mixer;
    
    public class AudioDevice
    {
        private static bool _engineIsInitialized;

        public AudioDevice()
        {
            if (_engineIsInitialized)
            {
                throw new AudioEngineException("Already initialized.");   
            }

            const MIX_InitFlags mixInitFlags = MIX_InitFlags.MIX_INIT_MP3 | 
                                               MIX_InitFlags.MIX_INIT_OGG;
            
            if (Mix_Init(mixInitFlags) == -1)
            {
                throw new AudioEngineException("SDL Mixer could not be initialized.");
            }

            var frequency = MIX_DEFAULT_FREQUENCY;
            var format = MIX_DEFAULT_FORMAT;
            var channels = MIX_DEFAULT_CHANNELS;
            var chunkSize = 1024;
            
            if (Mix_OpenAudio(frequency, format, channels, chunkSize) == -1)
            {
                throw new AudioEngineException("Opening audio device error.");
            }

            _engineIsInitialized = true;
        }

        public void Dispose()
        {
            if (!_engineIsInitialized)
            {
                return;
            }

            _engineIsInitialized = false;

            Mix_CloseAudio();
            Mix_Quit();
        }
    }
}