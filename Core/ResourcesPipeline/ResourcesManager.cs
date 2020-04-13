namespace Core.ResourcesPipeline
{
    using Config;
    using Shaders;
    using Shaders.Models;
    using CoreSystem.Graphics.Device;
    using CoreSystem.Graphics.Device.OpenGl;
    using System.Collections.Generic;
    using Silk.NET.OpenGL;
    
    public class ResourcesManager : IResourcesManager
    {
        private readonly IShaderCache _shaderCache;
        
        public ResourcesManager(IGraphicsDevice device)
        {
            _shaderCache = Configuration.Settings.Display.UseVulkan 
                ? new VkShaderCache()
                : new GlShaderCache(device as OpenGlDevice)
                    as IShaderCache;
        }

        public IShaderProgram LoadShader(string name)
        {
            var shaders = new Dictionary<ShaderType, string>
            {
                { ShaderType.VertexShader, $"{name}.vert"}
            };
            return _shaderCache.LoadShader(name, shaders);
        }
    }
}