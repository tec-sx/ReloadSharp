namespace Reload.Rendering.Structures
{
    using System;
    using Reload.Rendering.Properties;

    public class BufferElement
    {
        public string Name;
        public ShaderDataType Type;
        public uint Offset;
        public uint Size;
        public bool Normalized;

        public BufferElement()
        {}

        public BufferElement(ShaderDataType type, string name)
            : this(type, name, false)
        {}

        public BufferElement(ShaderDataType type, string name, bool normalized)
        {
            Name = name;
            Type = type;
            Size = Utils.GetShaderDatatypeSize(type);
            Offset = 0;
            Normalized = normalized;
        }

        public int GetComponentCount()
        {
            return Type switch
            {
                ShaderDataType.Float => 1,
                ShaderDataType.Float2 => 2,
                ShaderDataType.Float3 => 3,
                ShaderDataType.Float4 => 4,
                ShaderDataType.Mat3 => 3 * 3,
                ShaderDataType.Mat4 => 4 * 4,
                ShaderDataType.Int => 1,
                ShaderDataType.Int2 => 2,
                ShaderDataType.Int3 => 3,
                ShaderDataType.Int4 => 4,
                ShaderDataType.Bool => 1,
                ShaderDataType.None => 0,
                _ => throw new ApplicationException(Resources.UnknownShaderDataType)
            };
        }
    }
}
