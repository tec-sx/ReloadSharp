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
using Reload.Core.Exceptions;
using Reload.Core.Properties;
using System.Collections.ObjectModel;

namespace Reload.Core.Graphics.Rendering.Buffers
{
    /// <summary>
    /// The buffer layout is a <see cref="Collection{T}"/> of type <see cref="BufferElement"/>.
    /// </summary>
    public sealed class BufferLayout : Collection<BufferElement>
    {
        /// <summary>
        /// Gets the number of bytes from one buffer
        /// element to the other.
        /// </summary>
        public uint Stride { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BufferLayout"/> class.
        /// </summary>
        public BufferLayout()
        {
            Stride = 0;
        }

        /// <summary>
        /// Adds new buffer ellement to the collection and sets the
        /// correct offset and stride.
        /// </summary>
        /// <param name="bufferElement">The buffer element.</param>
        public new void Add(BufferElement bufferElement)
        {
            if (bufferElement == null)
            {
                throw new ReloadArgumentNullException(Resources.BufferElementNullArgumentMessage);
            }

            base.Add(bufferElement with { Offset = Stride });
            Stride += bufferElement.Size;
        }
    }
}
