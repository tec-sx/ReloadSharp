namespace Core.AssetsPipeline.Audio.Models
{
    using System;
    using CoreSystem.ErrorHandling.Exceptions;
    using static SDL2.SDL_mixer;

    public class MusicSdl: IMusic
    {
        public IntPtr Stream { get; set; }

        public void Play(int numOfLoops = -1)
        {
            if (Mix_PlayMusic(Stream, numOfLoops) == -1)
            {
                throw new MusicException("Can't play music file.");
            }
        }

        public void Update()
        {
        }

        public void Pause()
        {
        }

        public void Stop()
        {
        }

        public void Resume()
        {
        }
    }
}