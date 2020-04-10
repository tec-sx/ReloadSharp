namespace Core.ResourcesPipeline
{
    using Shaders.Models;
    using Silk.NET.OpenGL;

    public interface IResourcesManager
    {
        IShader LoadShader(ShaderType type, string name);
    }
}