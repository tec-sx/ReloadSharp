using Core.CoreSystem.ErrorHandling.Exceptions;

namespace Core.Resources.Audio.Models
{
    using System;
    using static SDL2.SDL_mixer;
    
    public class SoundSdl : ISound
    {
        public IntPtr Chunk { get; set; }
        
        public void Play()
        {
            if (Mix_PlayChannel(-1, Chunk, 0) == -1)
            {
                if (Mix_PlayChannel(0, Chunk, 0) == -1)
                {
                    throw new SoundException("Error playing sound file.");
                }
            }
        }
    }
}