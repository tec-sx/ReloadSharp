using System;
using System.Collections.Generic;

namespace Reload.Core.Rendering.Shaders
{
    using Reload.Core.Properties;

    /// <summary>
    /// The renderer utilities class.
    /// </summary>
    public static class ShaderUtils
    {
        /// <summary>
        /// Gets the shader data type size.
        /// </summary>
        /// <param name="type">The shader data type.</param>
        /// <returns>A siz as uint value.</returns>
        public static uint GetShaderDatatypeSize(ShaderDataType type)
        {
            return type switch
            {
                ShaderDataType.Float => 4,
                ShaderDataType.Float2 => 4 * 2,
                ShaderDataType.Float3 => 4 * 3,
                ShaderDataType.Float4 => 4 * 4,
                ShaderDataType.Mat3 => 4 * 3 * 3,
                ShaderDataType.Mat4 => 4 * 4 * 4,
                ShaderDataType.Int => 4,
                ShaderDataType.Int2 => 4 * 2,
                ShaderDataType.Int3 => 4 * 3,
                ShaderDataType.Int4 => 4 * 4,
                ShaderDataType.Bool => 1,
                ShaderDataType.None => 0,
                _ => throw new ApplicationException(Resources.InvalidShaderDataType)
            };
        }
    }
}
