namespace Engine.ResourcesPipeline.Shaders
{
    using System.Collections.Generic;
    using Models;
    using Silk.NET.OpenGL;

    public class VkShaderCache : IShaderCache
    {
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public IShaderProgram LoadShader(string name, Dictionary<ShaderType, string>files)
        {
            throw new System.NotImplementedException();
        }
    }
}