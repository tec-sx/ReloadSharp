using System.Collections.Generic;

namespace Core.ResourcesPipeline
{
    using Shaders.Models;
    using Silk.NET.OpenGL;

    public interface IResourcesManager
    {
        IShaderProgram LoadShader(string name);
    }
}