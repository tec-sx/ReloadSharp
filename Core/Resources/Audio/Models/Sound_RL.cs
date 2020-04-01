namespace Core.Resources.Audio.Models
{
    using Raylib_cs;

    public class Sound_RL : ISound
    {
        public Sound Chunk { get; set; }

        public void Play()
        {
            Raylib.PlaySound(Chunk);
        }
    }
}
