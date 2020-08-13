using System;
using System.IO;

namespace Reload.Core.VFS.Structures
{
    /// <summary>
    /// The asset entry.
    /// </summary>
    public class AssetEntry : Entry
    {
        /// <summary>
        /// Gets the asset processor.
        /// </summary>
        public uint Processor { get; }

        /// <summary>
        /// Gets the asset offset.
        /// </summary>
        public ulong Offset { get; }

        /// <summary>
        /// Gets the asset size.
        /// </summary>
        public ulong Size { get; }

        /// <summary>
        /// Prevents a default instance of the <see cref="AssetEntry"/> class from being created.
        /// </summary>
        private AssetEntry()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssetEntry"/> class.
        /// </summary>
        /// <param name="id">The asset id.</param>
        /// <param name="processor">The asset processor.</param>
        /// <param name="offset">The asset offset.</param>
        /// <param name="size">The asset size.</param>
        public AssetEntry(Guid id, uint processor, ulong offset, ulong size)
            : this()
        {
            Id = id;
            Processor = processor;
            Offset = offset;
            Size = size;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssetEntry"/> class
        /// with auto generated unique identifier.
        /// </summary>
        /// <param name="id">The asset id.</param>
        /// <param name="processor">The asset processor.</param>
        /// <param name="offset">The asset offset.</param>
        /// <param name="size">The asset size.</param>
        public AssetEntry(uint processor, ulong offset, ulong size)
           : this(new Guid(), processor, offset, size)
        { }
    }
}
