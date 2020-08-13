using Reload.Core.VFS.Properties;
using Reload.Core.VFS.Structures;
using System;
using System.Diagnostics;
using System.IO;

namespace Reload.Core.VFS.Extensions
{
    /// <summary>
    /// The binary reader extensions.
    /// </summary>
    internal static class BinaryReaderExtensions
    {
        /// <summary>
        /// Reads a <see cref="Guid"/> from the stream.
        /// </summary>
        /// <param name="reader">The <see cref="BinaryReader"/> from which to read the <see cref="Guid">.
        /// <returns>The <see cref="Guid"/> that was read from the stream.
        public static Guid ReadGuid(this BinaryReader reader)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(Resources.BinaryReaderNullArgument);
            }

            return new Guid(reader.ReadBytes(16));
        }

        /// <summary>
        /// Reads the header from the filesystem.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns>A Header.</returns>
        public static Header ReadHeader(this BinaryReader reader)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(Resources.BinaryReaderNullArgument);
            }

            uint magicNumber = reader.ReadUInt32();

            Debug.Assert(magicNumber == Header.MagicNumber, Resources.MagicNumberInvalidMessage);

            return new Header
            {
                GroupIndexOffset = reader.ReadUInt32()
            };
        }
    }
}
