namespace Reload.Platform.Graphics.OpenGl
{
    using Reload.Rendering;
    using Silk.NET.OpenGL;

    public class GlTexture2D : Texture2D
    {
        private GL _gl;
        private GLEnum _internalFormat;
        private GLEnum _dataFormat;
        private PathStringFormat _path;
        private uint _handle;

        public GlTexture2D(string filepath, GL api)
        {
            _gl = api;
        }

        public unsafe GlTexture2D(uint width, uint height, GL api)
        {
            _gl = api;
            Width = width;
            Height = height;

            _internalFormat = GLEnum.Rgba8;
            _dataFormat = GLEnum.Rgba;

            _gl.CreateTextures(GLEnum.Texture2D, 1, (uint*)_handle);
               _gl.TextureStorage2D(_handle, 1, _internalFormat, width, height);

            _gl.TextureParameter(_handle, GLEnum.TextureMinFilter, (int)GLEnum.Linear);
            _gl.TextureParameter(_handle, GLEnum.TextureMagFilter, (int)GLEnum.Linear);

            _gl.TextureParameter(_handle, GLEnum.TextureWrapS, (int)GLEnum.Repeat);
            _gl.TextureParameter(_handle, GLEnum.TextureWrapT, (int)GLEnum.Repeat);

            // Alternative
            //_handle = _gl.GenTexture();

            //_gl.TexImage2D(TextureTarget.Texture2D, 0, (int)InternalFormat.Rgba, width, height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, data);
            //_gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)GLEnum.ClampToEdge);
            //_gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)GLEnum.ClampToEdge);
            //_gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)GLEnum.Linear);
            //_gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)GLEnum.Linear);


        }

        public override void Bind(uint slot)
        {
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
