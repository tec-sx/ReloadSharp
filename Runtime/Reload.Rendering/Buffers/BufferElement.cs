using System;
using Reload.Rendering.Shaders; 

namespace Reload.Rendering.Buffers
{
    using Reload.Rendering.Properties;

    public struct BufferElement : IEquatable<BufferElement>
    {
        public readonly string Name;

        public readonly ShaderDataType Type;
        
        public readonly uint Size;
        
        public readonly bool Normalized;

        public uint Offset;

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
                _ => throw new ApplicationException(Resources.UnknownShaderDataType)
            };
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is BufferElement && Equals((BufferElement)obj);
        }

        /// <inheritdoc/>
        public bool Equals(BufferElement other)
        {
            return other != null
                && other.Name == Name
                && other.Type == Type
                && other.Offset == Offset
                && other.Size == Size
                && other.Normalized == Normalized;
        }

        /// <inheritdoc/>
        public static bool operator ==(BufferElement left, BufferElement right)
        {
            return left.Equals(right);
        }

        /// <inheritdoc/>
        public static bool operator !=(BufferElement left, BufferElement right)
        {
            return !(left == right);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Type, Offset, Size, Normalized);
        }
    }
}
