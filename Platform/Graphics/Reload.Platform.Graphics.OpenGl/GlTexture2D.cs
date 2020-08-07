namespace Reload.Platform.Graphics.OpenGl
{
    using Reload.Rendering;
    using Silk.NET.OpenGL;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.PixelFormats;
    using SixLabors.ImageSharp.Processing;
    using System.Runtime.InteropServices;
    using SixLabors.ImageSharp.Advanced;
    using Reload.Core.Utils;
    using System;

    public class GlTexture2D : Texture2D
    {
        private readonly GL _gl;
        private InternalFormat _internalFormat;
        private PixelFormat _dataFormat;
        private PathStringFormat _path;
        private uint _handle;

        public unsafe GlTexture2D(string filepath, GL api)
        {
            _gl = api;

            Image<Rgba32> image = Image.Load<Rgba32>(filepath);

            image.Mutate(img => img.Flip(FlipMode.Vertical));

            if (!image.TryGetSinglePixelSpan(out var pixelSpan))
            {
                Logger.PrintError("Can't load texture");
                throw new ApplicationException();
            }
            
            fixed (void* data = &MemoryMarshal.GetReference(pixelSpan))
            {
                Load((uint)image.Width, (uint)image.Height, data);
            }
        }

        public GlTexture2D(uint width, uint height, GL api)
        {
            throw new NotImplementedException();
        }

        private unsafe void Load(uint width, uint height, void* data)
        {
            Width = width;
            Height = height;

            _internalFormat = InternalFormat.Rgba;
            _dataFormat = PixelFormat.Rgba;

            _handle = _gl.GenTexture();

            _gl.ActiveTexture(TextureUnit.Texture0);
            _gl.BindTexture(TextureTarget.Texture2D, _handle);

            _gl.TexImage2D(TextureTarget.Texture2D, 0, (int)_internalFormat, width, height, 0, _dataFormat, PixelType.UnsignedByte, data);
            
            _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)GLEnum.Repeat);
            _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)GLEnum.Repeat);
            _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)GLEnum.Linear);
            _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)GLEnum.Nearest);

            //Generating mipmaps.
            _gl.GenerateMipmap(TextureTarget.Texture2D);
        }

        public override void Bind(uint slot = 0)
        {
            var slotUnit = GlUtils.TextureSlotIdToTextureUnit(slot);

            _gl.ActiveTexture(slotUnit);
            _gl.BindTexture(TextureTarget.Texture2D, _handle);
        }

        public override void SetData(object data)
        {
            throw new System.NotImplementedException();
        }

        public override void Dispose()
        {
            _gl.DeleteTexture(_handle);
        }
    }
}
