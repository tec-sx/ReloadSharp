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
using Reload.Core.Audio.Buffers;
using Reload.Core.Game;
using System;

namespace Reload.Core.Audio
{
    /// <summary>
    /// The audio API base.
    /// </summary>
    public abstract class AudioAPI : ISubSystem, IDisposable
    {
        /// <summary>
        /// Gets the audio backend type.
        /// </summary>
        public AudioAPIType Type { get; }

        /// <summary>
        /// Gets or sets the audio factory.
        /// </summary>
        protected internal static AudioFactory AudioFactory { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioAPI"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public AudioAPI(AudioAPIType type)
        {
            Type = type;
        }

        /// <inheritdoc/>
        public abstract void StartUp();

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
