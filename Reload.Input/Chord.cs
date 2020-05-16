namespace Reload.Input
{
    using Reload.Core;
    using Silk.NET.Input.Common;
    using System.Collections.Generic;

    public struct Chord
    {
        public List<Key> Keys { get; }
        public int KeyMatchCount { get; set; }

        public Chord(List<Key> keys, Command command)
        {
            Keys = keys;
            KeyMatchCount = 0;
        }
    }
}
