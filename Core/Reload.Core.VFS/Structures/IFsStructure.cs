namespace Reload.Core.VFS.Structures
{
    using System.IO;

    public interface IFsStructure
    {
        public void Read(BinaryReader reader);
        public void Write(BinaryWriter writer);
    }
}
