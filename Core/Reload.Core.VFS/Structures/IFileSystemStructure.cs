using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Reload.Core.VFS.Structures
{
    public interface IFileSystemStructure
    {
        /// <summary>
        /// Reads from the file system.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public void Read([DisallowNull]BinaryReader reader);


        /// <summary>
        /// Writes to the file system.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public void Write([DisallowNull]BinaryWriter writer);
    }
}
