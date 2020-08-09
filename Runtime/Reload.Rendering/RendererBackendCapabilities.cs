using System;

namespace Reload.Rendering
{
    /// <summary>
    /// The renderer backend capabilities.
    /// </summary>
    public readonly struct RendererBackendCapabilities : IEquatable<RendererBackendCapabilities>
    {
        /// <summary>
        /// Gets the renderer api vendor.
        /// </summary>
        public string Vendor { get; }

        /// <summary>
        /// Gets the renderer name.
        /// </summary>
        public string Renderer { get; }

        /// <summary>
        /// Gets the renderer version.
        /// </summary>
        public string Version { get; }

        /// <summary>
        /// Gets the maximum number of samples supported.
        /// </summary>
        public int MaxSamples { get; }

        /// <summary>
        /// Gets the maximum anisotropy value supported.
        /// </summary>
        public float MaxAnisotropy { get; }

        /// <summary>
        /// Gets the maximum number of texture units supported.
        /// </summary>
        public int MaxTextureUnits { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RendererBackendCapabilities"/> class.
        /// </summary>
        /// <param name="vendor">The vendor.</param>
        /// <param name="renderer">The renderer.</param>
        /// <param name="version">The version.</param>
        /// <param name="maxSamples">The max samples.</param>
        /// <param name="maxAnisotropy">The max anisotropy.</param>
        /// <param name="maxTextureUnits">The max texture units.</param>
        public RendererBackendCapabilities(
            string vendor,
            string renderer,
            string version,
            int maxSamples,
            float maxAnisotropy,
            int maxTextureUnits)
        {
            Vendor = vendor;
            Renderer = renderer;
            Version = version;
            MaxSamples = maxSamples;
            MaxAnisotropy = maxAnisotropy;
            MaxTextureUnits = maxTextureUnits;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return Equals((RendererBackendCapabilities)obj);
        }

        /// <inheritdoc/>
        public bool Equals(RendererBackendCapabilities other)
        {
            return other != null
                && other.Vendor == Vendor
                && other.Renderer == Renderer
                && other.Version == Version
                && other.MaxSamples == MaxSamples
                && other.MaxAnisotropy == MaxAnisotropy
                && other.MaxTextureUnits == MaxTextureUnits;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Vendor, Renderer, Version, MaxSamples, MaxAnisotropy, MaxTextureUnits);
        }

        /// <inheritdoc/>
        public static bool operator ==(RendererBackendCapabilities left, RendererBackendCapabilities right)
        {
            return left.Equals(right);
        }

        /// <inheritdoc/>
        public static bool operator !=(RendererBackendCapabilities left, RendererBackendCapabilities right)
        {
            return !(left == right);
        }

    }
}
