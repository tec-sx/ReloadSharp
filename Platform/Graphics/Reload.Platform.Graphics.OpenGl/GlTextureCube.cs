namespace Reload.Platform.Graphics.OpenGl
{
    using System;
    using Silk.NET.OpenGL;
    using Reload.Rendering;

    public class GlTextureCube : TextureCube
    {
        private readonly GL _gl;

        public unsafe GlTextureCube(string filepath, GL api)
        {
            _gl = api;
        }
        
        public GlTextureCube(TextureFormat format, uint width, uint height, GL api)
        {
            _gl = api;
        }

        public override void SetData(object data)
        {
            throw new NotImplementedException();
        }

        public override void Bind(uint slot = 0)
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
            throw new NotImplementedException();
        }

        public override string GetPath()
        {
            throw new NotImplementedException();
        }
    }
}