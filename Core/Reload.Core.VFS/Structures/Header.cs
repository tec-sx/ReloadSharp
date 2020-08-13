namespace Reload.Core.VFS.Structures
{
    /// <summary>
    /// The file header. (4bytes)
    /// </summary>
    public record Header
    {
        /// <summary>
        /// A constant numerical value used to identify a file format.
        /// </summary>
        public const uint MagicNumber = 0xB289;

        /// <summary>
        /// Gets or sets the group index offset.
        /// </summary>
        public uint GroupIndexOffset { get; init; }
    }
}
