namespace Reload.Core.VFS.Structures
{
    using System.Diagnostics;
    using System.IO;

    public class Header : IFsStructure
    {
        public uint MagicNumber => 0xB289;
        public uint GroupIndexOffset { get; set; }

        public void Read(BinaryReader reader)
        {
            var magicNumber = reader.ReadUInt32();

            Debug.Assert(magicNumber == MagicNumber, Properties.Resources.MagicNumberInvalidMessage);

            GroupIndexOffset = reader.ReadUInt32();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(MagicNumber);
            writer.Write(GroupIndexOffset);
        }
    }
}
