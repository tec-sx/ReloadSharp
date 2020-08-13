using Reload.Core.VFS.Properties;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace Reload.Core.VFS.Structures
{
    /// <summary>
    /// Collection containing elements of type the <see cref="AssetEntry"/>.
    /// </summary>
    public class AssetEntryCollection : Collection<AssetEntry>, IFileSystemStructure
    {
        /// <summary>
        /// Reads assets from the filesystem and add the to the collection.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public void Read(BinaryReader reader)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(Resources.BinaryReaderNullArgument);
            }

            var count = reader.ReadInt32();

            for (int i = 0; i < count; i++)
            {
                AssetEntry assetEntry = new AssetEntry();
                assetEntry.Read(reader);
                Add(assetEntry);
            }
        }

        /// <summary>
        /// Writes the assets collection to the filesystem.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public void Write(BinaryWriter writer)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(Resources.BinaryWriterNullArgument);
            }

            writer.Write(Count);

            foreach (var assetEntry in this)
            {
                assetEntry.Write(writer);
            }
        }
    }
}
