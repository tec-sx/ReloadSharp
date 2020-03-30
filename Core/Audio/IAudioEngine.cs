namespace Core.Audio
{
    using System;

    public interface IAudioEngine: IDisposable
    {
        void Init();
        SoundEffect LoadSound(string file);
        MusicStream LoadMusic(string file);
    }
}