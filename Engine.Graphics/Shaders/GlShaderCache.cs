namespace Core.ResourcesPipeline.Shaders
{
    using Engine.Graphics.Device.OpenGl;
    using System.Collections.Generic;
    using Models;
    using Silk.NET.OpenGL;

    public class GlShaderCache : IShaderCache
    {
        private const string SHADER_EXT = "glsl";

        private readonly Dictionary<string,  uint> _shaderProgramsDictionary;
        private readonly GL _api;

        public GlShaderCache(OpenGlDevice device)
        {
            _api = device.Api;
            _shaderProgramsDictionary = new Dictionary<string, uint>();
        }

        public void Dispose()
        {
            foreach (var (name, programHandle) in _shaderProgramsDictionary)
            {
                _api.DeleteProgram(programHandle);
                _shaderProgramsDictionary.Remove(name);
            }
        }

        public IShaderProgram LoadShader(string name, Dictionary<ShaderType, string> files)
        {
            GlShaderProgram shader;

            if (_shaderProgramsDictionary.TryGetValue(name, out var programHandle))
            {
                shader = new GlShaderProgram(_api, programHandle);
            }
            else
            {
                shader = new GlShaderProgram(_api);

                foreach (var (type, file) in files)
                {
                    shader.CompileShader(type, file);
                }
            }

            return shader;
        }
    }
}