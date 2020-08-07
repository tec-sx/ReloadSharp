namespace Reload.Rendering.Data
{
    public struct Statistics
    {
        /// <summary>
        /// Gets or sets the draw calls count.
        /// </summary>
        public uint DrawCallsCount { get; set; }

        /// <summary>
        /// Gets or sets the quad count.
        /// </summary>
        public uint QuadCount { get; set; }

        /// <summary>
        /// Gets or sets the line count.
        /// </summary>
        public uint LineCount { get; set; }

        /// <summary>
        /// Gets the total vertex count.
        /// </summary>
        public uint TotalVertexCount => QuadCount * 4 + LineCount * 2;

        /// <summary>
        /// Gets the total index count.
        /// </summary>
        public uint TotalIndexCount => QuadCount * 6 + LineCount * 2;
    }
}
