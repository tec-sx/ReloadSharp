using Reload.Core;

namespace Reload.Platform.OS.Windows
{
    /// <summary>
    /// The startup class for the Windows platfrorm
    /// </summary>
    public sealed class PlatformWindows : PlatformOS
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
