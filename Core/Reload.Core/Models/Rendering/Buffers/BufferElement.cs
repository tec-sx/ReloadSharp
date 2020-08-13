using System;
using Reload.Core.Rendering.Shaders;
using Reload.Core.Properties;

namespace Reload.Core.Models.Rendering.Buffers
{
    public record BufferElement : IEquatable<BufferElement>
    {
        public string Name { get; init; }

        public ShaderDataType Type { get; init; }
        
        public uint Size { get; init; }
        
        public bool Normalized { get; init; }

        public uint Offset { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BufferElement"/> class.
        /// </summary>
        /// <param name="type">The shader data type.</param>
        /// <param name="name">The buffer name.</param>
        /// <param name="normalized">If true, the buffer is normalized. (default = false)</param>
        public BufferElement(ShaderDataType type, string name, bool normalized = false)
        {
            Name = name;
            Type = type;
            Size = ShaderUtils.GetShaderDatatypeSize(type);
            Offset = 0;
            Normalized = normalized;
        }

        /// <summary>
        /// Gets the component count.
        /// </summary>
        /// <returns>An int.</returns>
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
                _ => throw new ApplicationException(Resources.InvalidShaderDataType)
            };
        }
    }
}
