namespace Reload.Core.VFS.Structures
{
    using Reload.Core.VFS.Extensions;
    using Reload.Core.VFS.Properties;
    using System;
    using System.IO;

    /// <summary>
    /// The asset group entry.
    /// </summary>
    public class AssetEntryGroup : IFileSystemStructure
    {
        /// <summary>
        /// Gets the asset group id.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the number of asset entries in the group.
        /// </summary>
        public ulong Count { get; private set; }

        /// <summary>
        /// Gets the asset group offset.
        /// </summary>
        public ulong Offset { get; set; }

        /// <summary>
        /// Gets the asset group flags.
        /// </summary>
        public GroupFlags Flags { get; private set; }

        /// <summary>
        /// Prevents a default instance of the <see cref="AssetEntryGroup"/> class from being created.
        /// </summary>
        private AssetEntryGroup()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssetEntryGroup"/> class.
        /// </summary>
        /// <param name="id">The asset id.</param>
        /// <param name="count">The asset count.</param>
        /// <param name="offset">The asset offset.</param>
        /// <param name="flags">The asset flags.</param>
        public AssetEntryGroup(Guid id, ulong count, ulong offset, GroupFlags flags)
            : this()
        {
            Id = id;
            Count = count;
            Offset = offset;
            Flags = flags;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssetEntryGroup"/> class
        /// with auto generated unique identifier.
        /// </summary>
        /// <param name="count">The asset count.</param>
        /// <param name="offset">The asset offset.</param>
        /// <param name="flags">The asset flags.</param>
        public AssetEntryGroup(ulong count, ulong offset, GroupFlags flags)
            : this(new Guid(), count, offset, flags)
        { }

        /// <inheritdoc/>
        public void Read(BinaryReader reader)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(Resources.BinaryReaderNullArgument);
            }

            Flags = (GroupFlags)reader.ReadByte();
            Id = reader.ReadGuid();
            Count = reader.ReadUInt64();
            Offset = reader.ReadUInt64();
        }

        /// <inheritdoc/>
        public void Write(BinaryWriter writer)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(Resources.BinaryWriterNullArgument);
            }

            writer.Write((byte)Flags);
            writer.Write(Id);
            writer.Write(Count);
            writer.Write(Offset);
        }
    }
}
