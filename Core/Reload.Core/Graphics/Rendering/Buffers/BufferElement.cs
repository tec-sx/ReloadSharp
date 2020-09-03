#region copyright
/*
-----------------------------------------------------------------------------
Copyright (c) 2020 Ivan Trajchev

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
-----------------------------------------------------------------------------
*/
#endregion
using System;
using Reload.Core.Graphics.Rendering.Shaders;
using Reload.Core.Properties;

namespace Reload.Core.Graphics.Rendering.Buffers
{
    /// <summary>
    /// Represents one shader element used in the buffer layout.
    /// </summary>
    public record BufferElement : IEquatable<BufferElement>
    {
        /// <summary>
        /// Gets the name of the buffer layout element.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Gets the shader data type.
        /// </summary>
        public ShaderDataType Type { get; init; }

        /// <summary>
        /// Gets the element size.
        /// </summary>
        public uint Size { get; init; }

        /// <summary>
        /// Gets a value indicating whether the element is normalized.
        /// </summary>
        public bool Normalized { get; init; }

        /// <summary>
        /// Gets the offset of the element.
        /// </summary>
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
            Size = ShaderUtilities.GetShaderDatatypeSize(type);
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
