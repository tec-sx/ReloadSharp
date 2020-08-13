using Reload.Core.VFS.Properties;
using System;
using System.IO;

namespace Reload.Core.VFS.Extensions
{
    /// <summary>
    /// The stream extensions.
    /// </summary>
    internal static class StreamExtensions
    {
        /// <summary>
        /// Block copy from one stream to another.
        /// </summary>
        /// <param name="from">The a.</param>
        /// <param name="to">The to.</param>
        /// <param name="size">The size.</param>
        public static void BlockCopyTo(this Stream from, Stream to, ulong size)
        {
            if (from is null || to is null)
            {
                throw new ArgumentNullException(Resources.StreamNullArgument);
            }

            ulong bytesPerCycle = 8192;
            ulong numCycles = size / bytesPerCycle;

            void ReadChunk(ulong size)
            {
                byte[] buf = new byte[size];
                from.Read(buf);
                to.Write(buf);
            }

            for (ulong i = 0; i < numCycles; i++)
            {
                ReadChunk(bytesPerCycle);
            }

            ulong remainder = size - (numCycles * bytesPerCycle);
            ReadChunk(remainder);
        }
    }
}
