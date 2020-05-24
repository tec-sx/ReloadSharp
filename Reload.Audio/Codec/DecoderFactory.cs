using Reload.Audio.Codec.Mp3;
using Reload.Audio.Codec.Vorbis;

namespace Reload.Audio.Codec
{
    using Reload.Audio.Codec.Mp3;
    using Reload.Audio.Codec.Vorbis;
    using System;
    using System.IO;
    using System.Linq;

    internal static class DecoderFactory
    {
        public static Decoder CreateDecoder(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            var fourcc = stream.ReadFourCc();
            stream.Seek(0, SeekOrigin.Begin);

            if (fourcc.SequenceEqual(MakeFourCC("ID3\u0001")) ||
                fourcc.SequenceEqual(MakeFourCC("ID3\u0002")) ||
                fourcc.SequenceEqual(MakeFourCC("ID3\u0003")) ||
                fourcc.AsSpan(0, 2).SequenceEqual(new byte[] { 0xFF, 0xFB }))
            {
                return new Mp3Decoder(stream);
            }

            else if (fourcc.SequenceEqual(MakeFourCC("OggS")))
            {
                return new VorbisDecoder(stream);
            }
            else
            {
                throw new InvalidDataException("Unknown format: " + fourcc);
            }
        }

        private static byte[] MakeFourCC(string magic)
        {
            return new[] {  (byte)magic[0],
                            (byte)magic[1],
                            (byte)magic[2],
                            (byte)magic[3]};
        }
    }
}
