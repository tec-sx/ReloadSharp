﻿using Reload.Core.Audio.Buffers;
using System;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Reload.Core.Audio
{
    /// <summary>
    /// The audio source.
    /// </summary>
    public abstract class AudioSource : IDisposable
    {
        /// <summary>
        /// Gets or sets the audio source gain.
        /// </summary>
        public abstract float Gain { get; set; }

        /// <summary>
        /// Gets a value indicating whether the audio source is playing.
        /// </summary>
        public abstract bool IsPlaying { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the audio source looping.
        /// </summary>
        public abstract bool Looping { get; set; }

        /// <summary>
        /// Gets or sets the audio source pitch.
        /// </summary>
        public abstract float Pitch { get; set; }

        /// <summary>
        /// Gets or sets the audio source position.
        /// </summary>
        public abstract Vector3 Position { get; set; }

        /// <summary>
        /// Gets or sets the audio source direction.
        /// </summary>
        public abstract Vector3 Direction { get; set; }

        /// <summary>
        /// Gets or sets the audio source velocity.
        /// </summary>
        public abstract Vector3 Velocity { get; set; }

        /// <summary>
        /// Gets the audio source duration time as <see cref="TimeSpan"/>.
        /// </summary>
        public abstract TimeSpan Duration { get; }

        /// <summary>
        /// Gets the audio source elapsed time as <see cref="TimeSpan"/>.
        /// </summary>
        public abstract TimeSpan Elapsed { get; }

        /// <summary>
        /// Plays the audio source.
        /// </summary>
        /// <param name="loop">If true, the audio source is played in loop.</param>
        public abstract void Play(bool loop);

        /// <summary>
        /// Stops playing the audio source.
        /// </summary>
        public abstract void Stop();

        /// <summary>
        /// Factory method for creating a new audio source from a stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>An AudioSource.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AudioSource Create(Stream stream)
        {
            return AudioAPI.AudioFactory.CreateAudioSource(stream);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Protected dispose method overload with disposing parameter that indicates 
        /// whether the method call comes from a Dispose method (value is true) or
        /// from a finalizer (value is false)
        /// </summary>
        /// <param name="disposing"></param>
        protected abstract void Dispose(bool disposing);
    }
}
