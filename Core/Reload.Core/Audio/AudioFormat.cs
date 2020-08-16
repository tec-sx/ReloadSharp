namespace Reload.Core.Audio
{
    public record AudioFormat
    {
        /// <summary>
        /// Gets the audio sample rate.
        /// </summary>
        public int SampleRate { get; init; }

        /// <summary>
        /// Gets the audio channel count.
        /// </summary>
        public int Channels { get; init; }

        /// <summary>
        /// Gets the bits per sample.
        /// </summary>
        public int BitsPerSample { get; init; }

        /// <summary>
        /// Gets the bytes per sample.
        /// </summary>
        public int BytesPerSample => BitsPerSample / 8;

        /// <summary>
        /// Gets the bytes per second.
        /// </summary>
        public int BytesPerSecond => BytesPerSample * SampleRate * Channels;
    }
}
