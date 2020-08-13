using Reload.Core.VFS.Properties;
using Reload.Core.VFS.Structures;
using System;
using System.IO;

namespace Reload.Core.VFS.Extensions
{
    /// <summary>
    /// The binary writer extensions.
    /// </summary>
    internal static class BinaryWriterExtensions
    {
        /// <summary>
        /// Writes a <see cref="Guid"/> to the stream.
        /// </summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> with which to write the <see cref="Guid">.
        /// <param name="guid">The <see cref="Guid"/> to write to the stream.
        public static void Write(this BinaryWriter writer, Guid guid)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(Resources.BinaryWriterNullArgument);
            }

            unsafe
            {
                byte* ptr = (byte*)&guid;
                for (int i = 0; i < 16; i++)
                    writer.Write(*ptr++);
            }
        }

        /// <summary>
        /// Writes the header to the filesystem..
        /// </summary>
        /// <param name="writer">The writer.</param>
        public static void WriteHeader(this BinaryWriter writer, Header header)
        {
            if (writer == null)
            {
                throw new ArgumentNullException(Resources.BinaryWriterNullArgument);
            }
            if (header == null)
            {
                throw new ArgumentNullException(Resources.HeaderNullArgument);
            }

            writer.Write(Header.MagicNumber);
            writer.Write(header.GroupIndexOffset);
        }
    }
}
