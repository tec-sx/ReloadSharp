using Reload.Core.Exceptions;
using Reload.Core.Properties;
using System.Collections.ObjectModel;

namespace Reload.Core.Models.Rendering.Buffers
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
