namespace Reload.Core.VFS
{
    using System;

    [Flags]
    public enum GroupFlags : byte
    {
        Basic = 1,
        Compressed = 2,
        Metadate = 3
    }
}
