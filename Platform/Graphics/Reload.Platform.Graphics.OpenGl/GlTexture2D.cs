using Silk.NET.OpenGL;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Runtime.InteropServices;
using Reload.Core.Utils;
using System;
using Reload.Rendering.Model;

namespace Reload.Platform.Graphics.OpenGl
{
    /// <summary>
    /// The OpenGl Texture2D implementation.
    /// Inherits from <see cref="Texture2D"/>
    /// <inheritdoc/>
    /// </summary>
    public sealed class GlTexture2D : Texture2D
    {
        private readonly GL _gl;

        private readonly uint _handle;

        private readonly GLEnum _internalFormat;

        private readonly PixelFormat _dataFormat;

        private readonly Image<Rgba32> _image;

        private PathStringFormat _path;

        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="GlTexture2D"/> class
        /// and sets the OpenGl api, internal format, data format and generates
        /// the texture handle.
        /// </summary>
        /// <param name="api">The api.</param>
        private GlTexture2D(GL api)
        {
            _gl = api;
            _internalFormat = GLEnum.Rgba8;
            _dataFormat = PixelFormat.Rgba;
            _handle = _gl.GenTexture();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GlTexture2D"/> class
        /// with a texture loaded from file.
        /// </summary>
        /// <param name="filepath">The filepath.</param>
        /// <param name="api">The api.</param>
        public unsafe GlTexture2D(string filepath, GL api)
            : this(api)
        {
            _image = Image.Load<Rgba32>(filepath);

            Width = (uint)_image.Width;
            Height = (uint)_image.Height;

            _image.Mutate(img => img.Flip(FlipMode.Vertical));

            if (!_image.TryGetSinglePixelSpan(out var pixelSpan))
            {
                Logger.PrintError("Can't load texture");
                throw new ApplicationException();
            }

            fixed (void* data = &MemoryMarshal.GetReference(pixelSpan))
            {
                //_gl.ActiveTexture(TextureUnit.Texture0);
                _gl.BindTexture(TextureTarget.Texture2D, _handle);

                _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)GLEnum.Linear);
                _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)GLEnum.Nearest);
                _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)GLEnum.Repeat);
                _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)GLEnum.Repeat);


                _gl.GenerateMipmap(TextureTarget.Texture2D);

                _gl.TexImage2D(TextureTarget.Texture2D, 0, (int)_internalFormat, Width, Height, 0, _dataFormat, PixelType.UnsignedByte, data);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GlTexture2D"/> class
        /// with empty white texture.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="api">The api.</param>
        public unsafe GlTexture2D(uint width, uint height, GL api)
            : this(api)
        {
            Width = width;
            Height = height;

            _gl.ActiveTexture(TextureUnit.Texture0);
            _gl.BindTexture(TextureTarget.Texture2D, _handle);

            _gl.TextureStorage2D(_handle, 1, _internalFormat, width, height);

            _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)GLEnum.Linear);
            _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)GLEnum.Nearest);

            _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)GLEnum.Repeat);
            _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)GLEnum.Repeat);

            _gl.TexImage2D(TextureTarget.Texture2D, 0, (int)_internalFormat, Width, Height, 0, _dataFormat, PixelType.UnsignedByte, null);

            _gl.BindTexture(TextureTarget.Texture2D, 0);
        }

        /// <inheritdoc/>
        public override void Bind(uint slot = 0)
        {
            var slotUnit = GlUtils.TextureSlotIdToTextureUnit(slot);

            _gl.ActiveTexture(slotUnit);
            _gl.BindTexture(TextureTarget.Texture2D, _handle);
        }

        /// <inheritdoc/>
        public unsafe override void SetData(Span<byte> pixelSpan)
        {
            _gl.TextureSubImage2D(_handle, 0, 0, 0, Width, Height, _internalFormat, GLEnum.UnsignedByte, pixelSpan);
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _image?.Dispose();
                }

                _gl.DeleteTexture(_handle);
            }

            _isDisposed = true;
        }

        public override void IsLoaded()
        {
            throw new NotImplementedException();
        }

        public override void Lock()
        {
            throw new NotImplementedException();
        }

        public override void Unlock()
        {
            throw new NotImplementedException();
        }

        public override void Resize(uint width, uint height)
        {
            throw new NotImplementedException();
        }

        public override string GetPath()
        {
            throw new NotImplementedException();
        }
    }
}
