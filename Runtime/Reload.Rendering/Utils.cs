using Reload.Rendering.Structures;
using Reload.Rendering.Properties;
using System;
using System.Collections.Generic;
using Silk.NET.OpenGL;

namespace Reload.Rendering
{
    /// <summary>
    /// The renderer utilities class.
    /// </summary>
    public static class Utils
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
                _ => throw new ApplicationException(Resources.UnknownShaderDataType)
            };
        }

        public static Dictionary<string, ShaderType> ShaderTypes = new Dictionary<string, ShaderType>
        {
            {"vertex", ShaderType.VertexShader},
            {"fragment", ShaderType.FragmentShader },
            {"geometry", ShaderType.GeometryShader },
            {"compute", ShaderType.ComputeShader },
            {"tess_control", ShaderType.TessControlShader },
            {"tess_evaluation", ShaderType.TessEvaluationShader }
        };
    }
}
