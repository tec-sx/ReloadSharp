using Reload.Core.Audio.Buffers;
using Reload.Core.Game;

namespace Reload.Core.Audio
{
    public abstract class AudioAPI : ISubSystem
    {
        /// <summary>
        /// Gets the audio backend type.
        /// </summary>
        public AudioAPIType Type { get; protected init; }

        /// <summary>
        /// Gets or sets the audio factory.
        /// </summary>
        protected internal static AudioFactory AudioFactory { get; protected set; }

        /// <inheritdoc/>
        public abstract void Initialize();

        /// <inheritdoc/>
        public abstract void ShutDown();
    }
}
