namespace Reload.Rendering.Shaders
{
    using System.Collections.Generic;

    public abstract class ShaderStruct
    {
        private List<ShaderUniformDeclaration> _fields;
        public IReadOnlyList<ShaderUniformDeclaration> Fields => _fields;
        public string Name { get; }
        public uint Size { get; private set; }
        public uint Offset { get; set; }

        public ShaderStruct(string name)
        {
            Name = name;
            Size = 0;
            Offset = 0;
            _fields = new List<ShaderUniformDeclaration>(8);
        }

        public void AddField(ShaderUniformDeclaration field)
        {
            Size += field.Size;
            uint offset = 0;

            if (_fields.Count > 0)
            {
                var previousField = _fields[_fields.Count - 1];
                offset = previousField.Offset + previousField.Size;
            }

            field.Offset = offset;
            _fields.Add(field);
        }
    }
}
