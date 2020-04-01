namespace Core.Resources.Audio.Models
{
    using Raylib_cs;

    public class Music_RL : IMusic
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
}
