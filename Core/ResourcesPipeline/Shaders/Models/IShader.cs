namespace Core.ResourcesPipeline.Shaders.Models
{
    public interface IShader
    {
        void CompileShaders();
        void LinkShaders();
        void SetUniform<T>(string name, T value);
        T GetUniform<T>(string name);
        void Use();
    }
}