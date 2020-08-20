using Reload.Core.Properties;
using Reload.Core.Utilities;
using System;

namespace Reload.Core.Graphics.Rendering.Textures.NullTextures
{
    /// <summary>
    /// The null texture cube.
    /// </summary>
    public class NullTextureCube : TextureCube
    {
        private readonly string _type = "Texture Cube";

        /// <inheritdoc/>
        public override void Bind(uint slot = 0)
        {
#if DEBUG
            Logger.Log().Warning(Resources.AccessingNullTextureMessage, _type);
#endif
        }

        /// <inheritdoc/>
        public override void Unbind()
        {
#if DEBUG
            Logger.Log().Warning(Resources.AccessingNullTextureMessage, _type);
#endif
        }

        /// <inheritdoc/>
        public override string GetPath()
        {
#if DEBUG
            Logger.Log().Warning(Resources.AccessingNullTextureMessage, _type);
#endif
            return string.Empty;
        }

        /// <inheritdoc/>
        public override void SetData(Span<byte> data)
        {
#if DEBUG
            Logger.Log().Warning(Resources.AccessingNullTextureMessage, _type);
#endif
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
#if DEBUG
            Logger.Log().Warning(Resources.AccessingNullTextureMessage, _type);
#endif
        }
    }
}
