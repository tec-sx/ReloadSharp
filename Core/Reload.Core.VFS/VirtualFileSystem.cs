namespace Reload.Core.VFS
{
    using Reload.Core.VFS.Structures;
    using System.Linq;
    using System.Collections.Generic;
    using System.IO;
    using K4os.Compression.LZ4.Streams;
    using System.Text;
    using K4os.Compression.LZ4;
    using System;
    using Reload.Core.VFS.Properties;
    using System.Diagnostics.CodeAnalysis;
    using Reload.Core.VFS.Extensions;

    /// <summary>
    /// Virtual file system.
    /// </summary>
    public class VirtualFileSystem
    {
        private Header _header;
        
        private AssetEntryGroupCollection _groupCollection;
        
        private List<AssetEntryCollection> _assetCollectionIndexes;
        
        private Dictionary<Guid, Stream> _assetsData;

        private int _activeGroupIndex;
        
        private bool _isInReadMode;

        private AssetEntryCollection _readAsset;
        
        private BinaryReader _streamReader;

        /// <summary>
        /// Gets the asset entry groups.
        /// </summary>
        public IReadOnlyList<Guid> Groups => _groupCollection.Select(group => group.Id).ToList();

        /// <summary>
        /// Gets the asset entries.
        /// </summary>
        public IReadOnlyList<AssetEntry> Assets => _isInReadMode ? _readAsset : _assetCollectionIndexes[_activeGroupIndex];

        /// <summary>
        /// Virtual file system constructor.
        /// </summary>
        public VirtualFileSystem()
        {
            _header = new Header();
            _groupCollection = new AssetEntryGroupCollection();
            _assetCollectionIndexes = new List<AssetEntryCollection>();
            _assetsData = new Dictionary<Guid, Stream>();
            _activeGroupIndex = 0;
            _isInReadMode = false;
        }

        /// <summary>
        /// Scope the filesystem to work with specific asset group.
        /// </summary>
        /// <param name="id"></param>
        public void ScopetoGroup(Guid id)
        {
            if (_isInReadMode)
            {
                _readAsset = new AssetEntryCollection();

                AssetEntryGroup groupIndex = _groupCollection.First(entry => entry.Id == id);

                _streamReader.BaseStream.Position = (long)groupIndex.Offset;
                _readAsset.Read(_streamReader);
            }

            if (_groupCollection.Count == 0)
            {
                _groupCollection.Add(new AssetEntryGroup
                {
                    Count = 0,
                    Flags = GroupFlags.Compressed,
                    Id = id,
                    Offset = 0
                });

                _assetCollectionIndexes.Add(new AssetEntryCollection());
                _activeGroupIndex = 0;
            }
            else
            {
                if (_groupCollection.All(entry => entry.Id != id))
                {
                    _groupCollection.Add(new AssetEntryGroup
                    {
                        Count = 0,
                        Flags = GroupFlags.Compressed,
                        Id = id,
                        Offset = 0
                    });
                }

                _activeGroupIndex = _groupCollection.IndexOf(_groupCollection.First(entry => entry.Id == id));
                _assetCollectionIndexes.Add(_activeGroupIndex, new AssetEntryCollection());
            }
        }

        /// <summary>
        /// Add new asset and it's data to the collection.
        /// </summary>
        /// <param name="assetEntry"></param>
        /// <param name="data"></param>
        public void AddNewAsset(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(Resources.StreamNullArgument);
            }

            AssetEntry asset = new AssetEntry

            _assetCollectionIndexes[_activeGroupIndex].Add(assetEntry);
            _assetsData.Add(assetEntry.Id, data);
        }

        /// <summary>
        /// Read all entry bytes as string instead of opening a stream.
        /// </summary>
        /// <param name="asset"></param>
        /// <returns></returns>
        public string ReadAllText(AssetEntry asset)
        {
            if (asset is null)
            {
                throw new ArgumentNullException(Resources.AssetEntryNullArgument);
            }

            if (_groupCollection[_activeGroupIndex].Flags.HasFlag(GroupFlags.Compressed))
            {
                _streamReader.BaseStream.Position = (long)asset.Offset;

                using MemoryStream stream = new MemoryStream();
                using LZ4DecoderStream decoder = LZ4Stream.Decode(_streamReader.BaseStream, null, true);
                
                decoder.CopyTo(stream, (int)asset.Size);

                return Encoding.ASCII.GetString(stream.ToArray());
            }
            else
            {
                _streamReader.BaseStream.Position = (long)asset.Offset;

                using MemoryStream stream = new MemoryStream((int)asset.Size);

                _streamReader.BaseStream.CopyTo(stream, (int)asset.Size);

                return Encoding.ASCII.GetString(stream.ToArray());
            }
        }

        /// <summary>
        /// Open the read stream.
        /// </summary>
        /// <param name="asset"></param>
        /// <returns>
        /// New stream. Remember to dispose it after usage.
        /// </returns>
        public Stream OpenRead(AssetEntry asset)
        {
            if (asset is null)
            {
                throw new ArgumentNullException(Resources.AssetEntryNullArgument);
            }

            MemoryStream memory = new MemoryStream();

            if (_groupCollection[_activeGroupIndex].Flags.HasFlag(GroupFlags.Compressed))
            {
                _streamReader.BaseStream.Position = (long)asset.Offset;
                using LZ4DecoderStream decoder = LZ4Stream.Decode(_streamReader.BaseStream, null, true);
                decoder.BlockCopyTo(memory, asset.Size);
                memory.Position = 0;
            }
            else
            {
                memory.Capacity = (int)asset.Size;
                _streamReader.BaseStream.Position = (long)asset.Offset;
                _streamReader.BaseStream.CopyTo(memory, (int)asset.Size);

                memory.Position = 0;
            }

            return memory;
        }

        /// <summary>
        /// Read from the opened stream.
        /// </summary>
        /// <param name="stream"></param>
        public void Read(Stream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(Resources.StreamNullArgument);
            }

            BinaryReader reader = new BinaryReader(stream);

            _streamReader = reader;
            _isInReadMode = true;

            _header = reader.ReadHeader();

            stream.Position = _header.GroupIndexOffset;
            _groupCollection.Read(reader);
        }

        /// <summary>
        /// Writes the assets to a stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public void Write([DisallowNull]Stream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(Resources.StreamNullArgument);
            }

            using BinaryWriter writer = new BinaryWriter(stream, Encoding.Default, true);

            writer.WriteHeader(_header);

            for (int i = 0; i < _assetCollectionIndexes.Count; i++)
            {
                foreach (AssetEntry asset in _assetCollectionIndexes[i])
                {
                    Stream data = _assetsData[asset.Id];

                    if (_groupCollection[_activeGroupIndex].Flags.HasFlag(GroupFlags.Compressed))
                    {
                        asset.Offset = (ulong)stream.Position;

                        LZ4EncoderSettings encoderSettings = new LZ4EncoderSettings
                        {
                            CompressionLevel = LZ4Level.L00_FAST
                        };

                        using (LZ4EncoderStream encoder = LZ4Stream.Encode(stream, encoderSettings, true))
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

                _groupCollection[i].Offset = (ulong)stream.Position;
                _groupCollection[i].Write(writer);
            }

            Header header = new Header
            {
                GroupIndexOffset = (uint)stream.Position
            };

            stream.Position = 0;

            writer.WriteHeader(header);

            stream.Position = header.GroupIndexOffset;

            _groupCollection.Write(writer);
        }
    }
}
