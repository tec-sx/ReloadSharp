using Silk.NET.OpenGL;

namespace Core.ResourcesPipeline.Shaders.Models
{
    public class VkShaderProgram : IShaderProgram
    {
        private const string SHADER_EXT = "spv";

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public void CompileShader(ShaderType type, string shaderName)
        {
            throw new System.NotImplementedException();
        }

        public void AddAttribute(string attributeName)
        {
            throw new System.NotImplementedException();
        }

        public void LinkShaders()
        {
            throw new System.NotImplementedException();
        }

        public void SetUniform(string name, int value)
        {
            throw new System.NotImplementedException();
        }

        public void SetUniform(string name, float value)
        {
            throw new System.NotImplementedException();
        }

        public void Use()
        {
            throw new System.NotImplementedException();
        }
    }
}