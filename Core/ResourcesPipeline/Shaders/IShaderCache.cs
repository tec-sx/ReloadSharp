namespace Core.ResourcesPipeline.Shaders
{
    using System;
    using System.Collections.Generic;
    using Models;
    using Silk.NET.OpenGL;

    public interface IShaderCache : IDisposable
    {
        IShaderProgram LoadShader(string name, Dictionary<ShaderType, string>files);
    }
}