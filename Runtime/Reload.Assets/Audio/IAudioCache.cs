using Reload.Core.Audio;

namespace Reload.Assets.Audio
{
    public interface IAudioCache
    {
        /// <summary>
        /// Loads a music file.
        /// </summary>
        /// <param name="fullPath">The full path.</param>
        /// <returns>A Music.</returns>
        Music LoadMusic(string fullPath);

        /// <summary>
        /// Loads a sound file.
        /// </summary>
        /// <param name="fullPath">The full path.</param>
        /// <returns>A Sound.</returns>
        Sound LoadSound(string fullPath);

        /// <summary>
        /// Cleans up resources.
        /// </summary>
        void CleanUp();
    }
}
