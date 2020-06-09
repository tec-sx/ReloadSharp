namespace Reload.Core.VFS.Tests
{
    using Reload.Core.VFS.Structures;
    using System.Collections.Generic;
    using System.IO;
    using Xunit;
    using FluentAssertions;
    using Reload.Core.VFS.Extensions;

    public class VirtualFileSystemTests
    {
        public Dictionary<string, byte[]> RandomDataSets;

        public VirtualFileSystemTests()
        {
            RandomDataSets = new Dictionary<string, byte[]>();

            var data = File.ReadAllBytes("Assets/Player.png");

            RandomDataSets.Add(data.Md5Hash(), data);
        }

        [Fact]
        public void VirtualFileSystemWriteSuccess()
        {
            using (var memory = new MemoryStream())
            {
                var vfs = new VirtualFileSystem();

                vfs.ScopeGroup("Test");

                foreach (var randomDataSet in RandomDataSets)
                {
                    var data = new MemoryStream(randomDataSet.Value);

                    vfs.AddAsset(new AssetEntry(randomDataSet.Key), data);
                }

                vfs.Write(memory);

                var dataIsInSet = RandomDataSets.ContainsKey(memory.ToArray().Md5Hash());
                dataIsInSet.Should().BeTrue();
            }
        }

        [Fact]
        public void VirtualFileSystemReadSuccess()
        {
            using var memory = new MemoryStream();

            var vfs = new VirtualFileSystem();
            vfs.Read(memory);

            foreach (var group in vfs.Groups)
            {
                vfs.ScopeGroup(group);

                foreach (var asset in vfs.Assets)
                {
                    using (var data = vfs.OpenRead(asset))
                    {
                        var dataIsInSet = RandomDataSets.ContainsKey(((MemoryStream)data).ToArray().Md5Hash());
                        dataIsInSet.Should().BeTrue();
                    }
                }
            }
        }
    }
}
