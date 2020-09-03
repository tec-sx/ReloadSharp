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
using System.Collections.Generic;
using Reload.Core.Properties;

namespace Reload.Core.Graphics.Rendering.Shaders
{

    /// <summary>
    /// The renderer utilities class.
    /// </summary>
    public static class ShaderUtilities
    {
        /// <summary>
        /// Gets the shader types.
        /// </summary>
        public static Dictionary<string, ShaderType> ShaderTypes { get; } = new Dictionary<string, ShaderType>
        {
            {"vertex", ShaderType.VertexShader},
            {"fragment", ShaderType.FragmentShader },
            {"geometry", ShaderType.GeometryShader },
            {"compute", ShaderType.ComputeShader },
            {"tess_control", ShaderType.TessControlShader },
            {"tess_evaluation", ShaderType.TessEvaluationShader }
        };

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
