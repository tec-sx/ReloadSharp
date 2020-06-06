namespace Reload.Core.VFS.Tests
{
    using System;
    using Reload.Core.VFS.Structures;
    using System.Collections.Generic;
    using System.Diagnostics;
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
        public void VfsReadWriteSuccess()
        {
            var stopwatch = new Stopwatch();

            using (var memory = new MemoryStream())
            {
                var vfs = new VirtualFileSystem();

                vfs.ScopeGroup("Test");

                foreach (var randomDataSet in RandomDataSets)
                {
                    var data = new MemoryStream(randomDataSet.Value);

                    vfs.AddAsset(new AssetEntry(randomDataSet.Key), data);
                }

                stopwatch.Restart();
                vfs.Write(memory);
                stopwatch.Stop();

                Console.WriteLine($"Writing data took: {stopwatch.ElapsedMilliseconds}ms.");

                memory.Position = 0;

                vfs = new VirtualFileSystem();
                vfs.Read(memory);

                foreach (var group in vfs.Groups)
                {
                    vfs.ScopeGroup(group);

                    foreach(var asset in vfs.Assets)
                    {
                        stopwatch.Restart();

                        using (var data = vfs.OpenRead(asset))
                        {
                            stopwatch.Stop();

                            Console.WriteLine($"Writing data took: {stopwatch.ElapsedMilliseconds}ms.");

                            var dataIsInSet = RandomDataSets.ContainsKey(((MemoryStream)data).ToArray().Md5Hash());
                            dataIsInSet.Should().BeTrue();
                        }
                    }
                }

                Console.WriteLine($"Hash for file: {memory.ToArray().Md5Hash()}");
            }
        }
    }
}
