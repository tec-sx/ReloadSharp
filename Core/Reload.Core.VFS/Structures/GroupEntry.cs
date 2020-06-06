namespace Reload.Core.VFS.Structures
{
    using System.IO;

    public class GroupEntry : IFsStructure
    {
        public GroupFlags Flags { get; set; }
        public string Name { get; set; }
        public ulong Count { get; set; }
        public ulong Offset { get; set; }

        public void Read(BinaryReader reader)
        {
            Flags = (GroupFlags)reader.ReadByte();
            Name = reader.ReadString();
            Count = reader.ReadUInt64();
            Offset = reader.ReadUInt64();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write((byte)Flags);
            writer.Write(Name);
            writer.Write(Count);
            writer.Write(Offset);
        }
    }
}
