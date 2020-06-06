namespace Reload.Core.VFS
{
    using Reload.Core.VFS.Structures;
    using System.Linq;
    using System.Collections.Generic;
    using System.IO;
    using K4os.Compression.LZ4.Streams;
    using System.Text;
    using K4os.Compression.LZ4;

    /// <summary>
    /// Virtual file system.
    /// </summary>
    public class VirtualFileSystem
    {
        private Header _header;
        private GroupIndex _groupIndex;
        private Dictionary<int, AssetIndex> _assetIndexes;
        private Dictionary<AssetEntry, Stream> _assetsData;

        private int _activeGroupIndex;
        private bool _isInReadMode;
        private AssetIndex _readAsset;
        private BinaryReader _streamReader;

        public List<string> Groups => _groupIndex.Select(entry => entry.Name).ToList();
        public List<AssetEntry> Assets
        {
            get
            {
                if (_isInReadMode)
                {
                    return _readAsset.Select(asset => asset).ToList();
                }
                else
                {
                    return _assetIndexes[_activeGroupIndex].Select(asset => asset).ToList();
                }
            }
        }

        /// <summary>
        /// Virtual file system constructor.
        /// </summary>
        public VirtualFileSystem()
        {
            _header = new Header();
            _groupIndex = new GroupIndex();
            _assetIndexes = new Dictionary<int, AssetIndex>();
            _assetsData = new Dictionary<AssetEntry, Stream>();
            _activeGroupIndex = 0;
            _isInReadMode = false;
        }

        /// <summary>
        /// Scope an assetgroup.
        /// </summary>
        /// <param name="name"></param>
        public void ScopeGroup(string name)
        {
            if (_isInReadMode)
            {
                _readAsset = new AssetIndex();

                var groupIndex = _groupIndex.First(entry => entry.Name == name);

                _streamReader.BaseStream.Position = (long)groupIndex.Offset;
                _readAsset.Read(_streamReader);
            }

            if (_groupIndex.Count == 0)
            {
                _groupIndex.Add(new GroupEntry
                {
                    Count = 0,
                    Flags = GroupFlags.Compressed,
                    Name = name,
                    Offset = 0
                });

                _assetIndexes.Add(0, new AssetIndex());
                _activeGroupIndex = 0;
            }
            else
            {
                if (_groupIndex.All(entry => entry.Name != name))
                {
                    _groupIndex.Add(new GroupEntry
                    {
                        Count = 0,
                        Flags = GroupFlags.Compressed,
                        Name = name,
                        Offset = 0
                    });
                }

                _activeGroupIndex = _groupIndex.IndexOf(_groupIndex.First(entry => entry.Name == name));
                _assetIndexes.Add(_activeGroupIndex, new AssetIndex());
            }
        }

        /// <summary>
        /// Add new asset.
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="data"></param>
        public void AddAsset(AssetEntry entry, Stream data)
        {
            var index = _assetIndexes[_activeGroupIndex];

            index.Add(entry);
            _assetsData.Add(entry, data);
        }

        /// <summary>
        /// Read all entry bytes as string instead of opening a stream.
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public string ReadAllText(AssetEntry entry)
        {
            if (_groupIndex[_activeGroupIndex].Flags.HasFlag(GroupFlags.Compressed))
            {
                _streamReader.BaseStream.Position = (long)entry.Offset;

                using (var stream = new MemoryStream())
                using (var decoder = LZ4Stream.Decode(_streamReader.BaseStream, null, true))
                {
                    decoder.CopyTo(stream, (int)entry.Size);

                    return Encoding.ASCII.GetString(stream.ToArray());
                }
            }
            else
            {
                _streamReader.BaseStream.Position = (long)entry.Offset;

                using (var stream = new MemoryStream((int)entry.Size))
                {
                    _streamReader.BaseStream.CopyTo(stream, (int)entry.Size);

                    return Encoding.ASCII.GetString(stream.ToArray());
                }
            }
        }

        /// <summary>
        /// Open the read stream. Remember to dispose the
        /// stream after usage.
        /// </summary>
        /// <param name="entry"></param>
        /// <returns>New stream</returns>
        public Stream OpenRead(AssetEntry entry)
        {
            if (_groupIndex[_activeGroupIndex].Flags.HasFlag(GroupFlags.Compressed))
            {
                _streamReader.BaseStream.Position = (long)entry.Offset;

                var stream = new MemoryStream();
                using (var decoder = LZ4Stream.Decode(_streamReader.BaseStream, null, true))
                {
                    ulong bytesPerCycle = 8192;
                    ulong numberOfCycles = entry.Size / bytesPerCycle;

                    void ReadChunk(ulong size)
                    {
                        byte[] buffer = new byte[size];

                        decoder.Read(buffer);
                        stream.Write(buffer);
                    }

                    for (ulong i = 0; i < numberOfCycles; i++)
                    {
                        ReadChunk(bytesPerCycle);
                    }

                    ulong remainder = entry.Size - (numberOfCycles * bytesPerCycle);
                    ReadChunk(remainder);

                    stream.Position = 0;
                    return stream;
                }
            }
            else
            {
                _streamReader.BaseStream.Position = (long)entry.Offset;
                var stream = new MemoryStream((int)entry.Size);

                _streamReader.BaseStream.CopyTo(stream, (int)entry.Size);

                stream.Position = 0;
                return stream;
            }
        }

        /// <summary>
        /// Read from the opened stream.
        /// </summary>
        /// <param name="stream"></param>
        public void Read(Stream stream)
        {
            var reader = new BinaryReader(stream);

            _streamReader = reader;
            _isInReadMode = true;

            _header.Read(reader);
            stream.Position = _header.GroupIndexOffset;
            _groupIndex.Read(reader);
        }

        public void Write(Stream stream)
        {
            using (var writer = new BinaryWriter(stream, Encoding.Default, true))
            {
                _header.Write(writer);

                foreach (var(i, assetIndex) in _assetIndexes)
                {
                    foreach (var asset in assetIndex)
                    {
                        var data = _assetsData[asset];

                        if (_groupIndex[_activeGroupIndex].Flags.HasFlag(GroupFlags.Compressed))
                        {
                            asset.Offset = (ulong)stream.Position;

                            var encoderSettings = new LZ4EncoderSettings
                            {
                                CompressionLevel = LZ4Level.L00_FAST
                            };

                            using (var encoder = LZ4Stream.Encode(stream, encoderSettings, true))
                            {
                                data.CopyTo(encoder);
                            }

                            asset.Size = (ulong)data.Length;
                        }
                        else
                        {
                            asset.Offset = (ulong)stream.Position;
                            asset.Size = (ulong)data.Length;
                            data.CopyTo(stream);
                        }
                    }

                    _groupIndex[i].Offset = (ulong)stream.Position;
                    assetIndex.Write(writer);
                }

                _header.GroupIndexOffset = (uint)stream.Position;
                stream.Position = 0;
                _header.Write(writer);

                stream.Position = _header.GroupIndexOffset;

                _groupIndex.Write(writer);
            }
        }
    }
}
