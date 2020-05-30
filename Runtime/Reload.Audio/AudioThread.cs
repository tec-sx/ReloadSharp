namespace Reload.Audio
{
    using System.Threading;

    internal static class AudioThread
    {
        public static Mutex TryGetAudioMutex()
        {
            try
            {
                return Mutex.OpenExisting(Properties.Resources.AudioMutex);
            }
            catch(WaitHandleCannotBeOpenedException)
            {
                return new Mutex(true, Properties.Resources.AudioMutex);
            }
        }
    }
}
