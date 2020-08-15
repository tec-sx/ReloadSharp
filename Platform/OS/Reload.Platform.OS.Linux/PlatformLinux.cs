using Reload.Core;

namespace Reload.Platform.OS.Linux
{
    /// <summary>
    /// The startup class for the Linux platfrorm
    /// </summary>
    public sealed class PlatformLinux : PlatformOS
    {
        /// <inheritdoc/>
        public override bool CheckAudioBackendCompatability<T>()
        {
            return true;
        }

        /// <inheritdoc/>

        public override bool CheckGraphicsBackendCompatability<T>()
        {
            return true;
        }

        /// <inheritdoc/>

        public override bool CheckInputCompatability<T>()
        {
            return true;
        }

        /// <inheritdoc/>

        public override bool CheckWindowCompatability<T>()
        {
            return true;
        }
    }
}
