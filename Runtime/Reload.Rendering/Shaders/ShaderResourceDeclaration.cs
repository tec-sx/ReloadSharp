namespace Reload.Rendering.Shaders
{
    public abstract class ShaderResourceDeclaration
    {
        public string Name { get; protected set; }
        public uint Register { get; protected set; }
        public uint Count { get; protected set; }

        public ShaderResourceDeclaration(string name, uint count, uint register)
        {
            Name = name;
            Count = count;
            Register = register;
        }
    }
}
