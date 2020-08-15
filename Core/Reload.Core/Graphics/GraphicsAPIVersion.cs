namespace Reload.Core.Graphics
{
    public record GraphicsAPIVersion
    {
        /// <summary>
        /// Gets the major API version.
        /// </summary>
        public int Major { get; init; }

        /// <summary>
        /// Gets the minor API Version.
        /// </summary>
        public int Minor { get; init; }
    }
}
