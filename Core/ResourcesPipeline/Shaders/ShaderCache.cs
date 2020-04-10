namespace Core.ResourcesPipeline.Shaders
{
    using System.Collections.Generic;
    using Models;
    using Silk.NET.OpenGL;
    
    public class ShaderCache : IShaderCache
    {
        private readonly Dictionary<string, uint> _shadersDictionary;

        public ShaderCache()
        {
            _shadersDictionary = new Dictionary<string, uint>();
        }

        public void Dispose()
        {
            
        }
        
        public IShader LoadShader(ShaderType type, string shaderSrc)
        {
            if (Configuration.Settings.Display.UseVulkan)
            {
                _shader = new VulkanShader(shaderDictionary);
            }
            else
            {
                _shader = new OpenGlShader(shaderDictionary);
            }
        }
    }
}