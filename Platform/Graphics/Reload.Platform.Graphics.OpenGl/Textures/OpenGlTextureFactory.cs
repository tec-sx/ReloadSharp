﻿using Reload.Core.Graphics.Rendering.Textures;
using Reload.Core.Graphics.Rendering.Textures.NullTextures;
using Silk.NET.OpenGL;

namespace Reload.Platform.Graphics.OpenGl.Textures
{
    /// <summary>
    /// The open gl texture factory implementation.
    /// </summary>
    public class OpenGlTextureFactory : TextureFactory
    {
        private GL _api;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGlTextureFactory"/> class.
        /// </summary>
        /// <param name="api">The api.</param>
        public OpenGlTextureFactory(GL api)
        {
            _api = api;
        }

        /// <inheritdoc/>
        public override Texture2D CreateBlankTexture2D(uint width, uint height) => new OpenGlTexture2D(width, height, _api);

        /// <inheritdoc/>
        public override Texture2D CreateTexture2DFromFile(string path) => new OpenGlTexture2D(path, _api);

        /// <inheritdoc/>
        public override TextureCube CreateBlankTextureCube(TextureFormat format, uint width, uint height) => new NullTextureCube();

        /// <inheritdoc/>
        public override TextureCube CreateTextureCubeFromFile(string path) => new NullTextureCube();
    }
}
