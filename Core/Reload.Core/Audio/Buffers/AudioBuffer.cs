#region copyright
/*
-----------------------------------------------------------------------------
Copyright (c) 2020 Ivan Trajchev

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
-----------------------------------------------------------------------------
*/
#endregion
using System;

namespace Reload.Core.Audio.Buffers
{
    /// <summary>
    /// The audio buffer.
    /// </summary>
    public abstract class AudioBuffer : IDisposable
    {
        /// <summary>
        /// Gets or sets the audio format.
        /// </summary>
        public AudioFormat Format { get; protected set; }

        /// <summary>
        /// Gets or sets the buffer.
        /// </summary>
        public uint Buffer { get; protected set; }

        /// <summary>
        /// Sets the data to the buffer.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="audioFormat">The audio format.</param>
        public abstract void SetData<T>(T[] data, AudioFormat audioFormat) where T : unmanaged;

        /// <summary>
        /// Creates a new audio buffer.
        /// </summary>
        /// <returns>An AudioBuffer.</returns>
        public static AudioBuffer Create() => AudioAPI.AudioFactory.CreateAudioBuffer();

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
