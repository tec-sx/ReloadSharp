namespace Core.Utilities
{
    using static SDL2.SDL;
    
    public class FpsLimiter
    {
        private const float ONE_SECOND = 1000.0f;
        private const int NUM_OF_SAMPLES = 10;
        
        private static readonly float[] FrameTimes = new float[NUM_OF_SAMPLES];
        private static int _currentFrame = 0;
        private static uint _previousTicks = SDL_GetTicks();
        
        private float _fps;
        private float _maxFps;
        private float _frameTime;
        private uint _startTicks;
        private float _lastDeltaTime;

        public float DeltaTime { get; set; }
        
        public void SetMaxFps(float maxFps)
        {
            _maxFps = maxFps;
        }

        public void Begin()
        {
            _startTicks = SDL_GetTicks();
        }

        public float End()
        {
            CalculateFps();
            
            var nowDeltaTime = SDL_GetTicks();
            var frameTicks = SDL_GetTicks() - _startTicks;

            if (ONE_SECOND / _maxFps > frameTicks)
            {
                SDL_Delay((uint)(ONE_SECOND / _maxFps - frameTicks));
            }

            if (nowDeltaTime > _lastDeltaTime)
            {
                DeltaTime = nowDeltaTime - _lastDeltaTime;
                _lastDeltaTime = nowDeltaTime;
            }

            return _fps;
        }

        private void CalculateFps()
        {
            var currentTicks = SDL_GetTicks();

            _frameTime = currentTicks - _previousTicks;
            FrameTimes[_currentFrame % NUM_OF_SAMPLES] = _frameTime;
            _previousTicks = currentTicks;
            _currentFrame++;
            
            var frameCount = _currentFrame < NUM_OF_SAMPLES ? _currentFrame : NUM_OF_SAMPLES;
            var frameTimeAverage = 0.0f;

            for (var i = 0; i < frameCount; i++)
            {
                frameTimeAverage += FrameTimes[i];
            }

            frameTimeAverage /= frameCount;

            _fps = frameTimeAverage > 0 ? ONE_SECOND / frameTimeAverage : 60.0f;
        }
    }
}