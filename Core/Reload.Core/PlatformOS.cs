using Reload.Core.Audio;
using Reload.Core.Game;
using Reload.Core.Graphics;
using Reload.Core.Input;

namespace Reload.Core
{
    /// <summary>
    /// The OS platform base class. Every opering system implementation
    /// must inherit from this class.
    /// </summary>
    public abstract class PlatformOS
    {
        /// <summary>
        /// Checks if the window is compatible with the running operating system.
        /// </summary>
        /// <returns>A bool.</returns>
        public abstract bool CheckWindowCompatability<T>() where T : IGameWindow;

        /// <summary>
        /// Checks if the garphics backend is compatible with the running operating system.
        /// </summary>
        /// <returns>A bool.</returns>
        public abstract bool CheckGraphicsBackendCompatability<T>() where T : IGraphicsBackend;

        /// <summary>
        /// Checks if the audio backend is compatible with the running operating system.
        /// </summary>
        /// <returns>A bool.</returns>
        public abstract bool CheckAudioBackendCompatability<T>() where T : IAudioBackend;

        /// <summary>
        /// Checks if the input is compatible with the running operating system.
        /// </summary>
        /// <returns>A bool.</returns>
        public abstract bool CheckInputCompatability<T>() where T : IInputSystem;
    }
}
