namespace Reload.Core.Graphics.Rendering.Shaders
{
    public enum ShaderDomain
    {
        None = 0,
        Vertex = 0,
        Pixel = 1
    }

    public abstract class ShaderUniformDeclaration
    {
        protected uint offset;
        public abstract uint Offset { get;  set; }
        public string Name { get; }
        public uint Size { get; }
        public uint Count { get; }
        public ShaderDomain Domain { get; }

        public ShaderUniformDeclaration(string name, uint size, uint count, ShaderDomain domain)
        {
            Name = name;
            Size = size;
            Count = count;
            Domain = domain;
        }
    }
}
