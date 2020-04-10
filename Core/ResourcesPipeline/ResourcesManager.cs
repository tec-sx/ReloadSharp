namespace Core.ResourcesPipeline
{
    using System;
    using System.IO;
    using Config;
    using Shaders;
    using Shaders.Models;
    using Silk.NET.OpenGL;
    
    public class ResourcesManager : IResourcesManager
    {
        private readonly IShaderCache _shaderCache;
        
        public ResourcesManager(IShaderCache shaderCache)
        {
            _shaderCache = shaderCache;
        }
        
        public IShader LoadShader(ShaderType type, string name)
        {
            var resourceName = Configuration.Settings.Display.UseVulkan
                ? $"Core.Resources.Shaders.{name}.spv"
                : $"Core.Resources.Shaders.{name}.glsl";

            return _shaderCache.LoadShader(type, LoadEmbeddedResourceString(resourceName));
        }
        
        private static byte[] LoadEmbeddedResourceBytes(string path)
        {
            using var stream = typeof(GameBase).Assembly.GetManifestResourceStream(path);
            using var ms = new MemoryStream();

            if (stream == null)
            {
                throw new ApplicationException($"Embedded resource {path} not found.");    
            }
            
            stream.CopyTo(ms);
            return ms.ToArray();
        }
        
        private static string LoadEmbeddedResourceString(string path)
        {
            using var stream = typeof(GameBase).Assembly.GetManifestResourceStream(path);

            if (stream == null)
            {
                throw new ApplicationException($"Embedded resource {path} not found.");    
            }
            
            using var reader = new StreamReader(stream);
            
            return reader.ReadToEnd();
        }
    }
}