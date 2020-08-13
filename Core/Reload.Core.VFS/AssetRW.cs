using Reload.Core.VFS.Extensions;
using Reload.Core.VFS.Properties;
using Reload.Core.VFS.Structures;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Reload.Core.VFS
{
    /// <summary>
    /// Encapsulates the asset structures read and writ methods.
    /// </summary>
    public static class AssetRW
    {

        /// <summary>
        /// Reads an asset entry from the filesystem.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns>An AssetEntry.</returns>
        public static AssetEntry ReadAsset([DisallowNull] BinaryReader reader)
        { 
            if (reader == null)
            {
                throw new ArgumentNullException(Resources.BinaryReaderNullArgument);
            }

            return new AssetEntry(
                id: reader.ReadGuid(),
                processor: reader.ReadUInt32(),
                offset: reader.ReadUInt64(),
                size: reader.ReadUInt64());
        }
    }
}
