namespace Reload.Core.VFS.Structures
{
    using System.Collections.Generic;
    using System.IO;

    public class AssetIndex : List<AssetEntry>, IFsStructure
    {
        public void Read(BinaryReader reader)
        {
            var count = reader.ReadInt32();

            for (int i = 0; i < count; i++)
            {
                var assetEntry = new AssetEntry();

                assetEntry.Read(reader);

                Add(assetEntry);
            }
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Count);

            foreach (var assetEntry in this)
            {
                assetEntry.Write(writer);
            }
        }
    }
}
