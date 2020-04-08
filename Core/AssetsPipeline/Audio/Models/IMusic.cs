namespace Core.Resources.Audio
{
    public interface IMusic
    {
        void Play(int numOfLoops = -1);
        void Update();
        void Pause();
        void Stop();
        void Resume();
    }
}
