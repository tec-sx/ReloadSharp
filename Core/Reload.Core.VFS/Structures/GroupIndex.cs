﻿namespace Reload.Core.VFS.Structures
{
    using System.Collections.ObjectModel;
    using System.IO;

    public class GroupIndex : Collection<GroupEntry>, IFsStructure
    {
        public void Read(BinaryReader reader)
        {
            var count = reader.ReadInt32();

            for (int i = 0; i < count; i++)
            {
                var groupEntry = new GroupEntry();

                groupEntry.Read(reader);

                Add(groupEntry);
            }
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Count);

            foreach (var groupEntry in this)
            {
                groupEntry.Write(writer);
            }
        }
    }
}
