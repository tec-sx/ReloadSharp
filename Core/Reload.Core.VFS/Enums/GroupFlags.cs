namespace Reload.Core.VFS
{
    using System;

    [Flags]
    public enum GroupFlags : byte
    {
        None = 0,
        Basic,
        Compressed,
        Metadate
    }
}
