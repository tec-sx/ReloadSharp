using Reload.Core.Properties;
using Reload.Core.Utilities;
using System;

namespace Reload.Core.Graphics.Rendering.Textures.NullTextures
{
    /// <summary>
    /// The null 2D texture.
    /// </summary>
    public class NullTexture2D : Texture2D
    {
        private readonly string _type = "Texture 2D";

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
        public override void IsLoaded()
        {
#if DEBUG
            Logger.Log().Warning(Resources.AccessingNullTextureMessage, _type);
#endif
        }

        /// <inheritdoc/>
        public override void Lock()
        {
#if DEBUG
            Logger.Log().Warning(Resources.AccessingNullTextureMessage, _type);
#endif
        }

        /// <inheritdoc/>
        public override void Resize(uint width, uint height)
        {
#if DEBUG
            Logger.Log().Warning(Resources.AccessingNullTextureMessage, _type);
#endif
        }

        /// <inheritdoc/>
        public override void SetData(Span<byte> data)
        {
#if DEBUG
            Logger.Log().Warning(Resources.AccessingNullTextureMessage, _type);
#endif
        }

        /// <inheritdoc/>
        public override void Unlock()
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
