using Reload.Core.Audio.Buffers;
using Reload.Core.Game;
using System;

namespace Reload.Core.Audio
{
    /// <summary>
    /// The audio API base.
    /// </summary>
    public abstract class AudioAPI : ICoreSystem
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

        /// <summary>
        /// Protected dispose method overload with disposing parameter that indicates 
        /// whether the method call comes from a Dispose method (value is true) or
        /// from a finalizer (value is false)
        /// </summary>
        /// <param name="disposing"></param>
        protected abstract void Dispose(bool disposing);

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
