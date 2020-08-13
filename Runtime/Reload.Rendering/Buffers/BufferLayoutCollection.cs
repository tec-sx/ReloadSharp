using System.Collections.ObjectModel;

namespace Reload.Rendering.Buffers
{
    /// <summary>
    /// The buffer layout is a <see cref="Collection{T}"/> of type <see cref="BufferElement"/>.
    /// </summary>
    public class BufferLayoutCollection : Collection<BufferElement>
    {
        /// <summary>
        /// Gets the buffer stride.
        /// </summary>
        public uint Stride { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BufferLayoutCollection"/> class.
        /// </summary>
        public BufferLayoutCollection()
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
            bufferElement.Offset = Stride;
            Stride += bufferElement.Size;

            base.Add(bufferElement);
        }
    }
}
