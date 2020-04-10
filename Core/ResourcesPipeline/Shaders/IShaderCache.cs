namespace Core.ResourcesPipeline.Shaders
{
    using System;
    using Models;
    using Silk.NET.OpenGL;

    public interface IShaderCache : IDisposable
    {
        IShader LoadShader(ShaderType type, string shaderSrc);
    }
}