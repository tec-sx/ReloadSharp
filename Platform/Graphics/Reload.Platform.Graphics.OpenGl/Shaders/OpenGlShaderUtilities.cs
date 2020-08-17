using ReloadShaderType = Reload.Core.Graphics.Rendering.Shaders.ShaderType;
using OpenGlShaderType = Silk.NET.OpenGL.ShaderType;
using Reload.Core.Exceptions;

namespace Reload.Platform.Graphics.OpenGl.Shaders
{
    /// <summary>
    /// The OpenGl shader utilities.
    /// </summary>
    public static class OpenGlShaderUtilities
    {
        /// <summary>
        /// Converts the <see cref="ReloadShaderType"/> type 
        /// to <see cref="OpenGlShaderType"/>
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>An OpenGlShaderType.</returns>
        public static OpenGlShaderType ShaderTypeToOpenGl(ReloadShaderType type)
        {
            return type switch
            {
                ReloadShaderType.FragmentShader => OpenGlShaderType.FragmentShader,
                ReloadShaderType.FragmentShaderArb => OpenGlShaderType.FragmentShaderArb,
                ReloadShaderType.VertexShader => OpenGlShaderType.VertexShader,
                ReloadShaderType.VertexShaderArb => OpenGlShaderType.VertexShaderArb,
                ReloadShaderType.GeometryShader => OpenGlShaderType.GeometryShader,
                ReloadShaderType.TessEvaluationShader => OpenGlShaderType.TessEvaluationShader,
                ReloadShaderType.TessControlShader => OpenGlShaderType.TessControlShader,
                ReloadShaderType.ComputeShader => OpenGlShaderType.ComputeShader,
                _ => throw new ReloadInvalidEnumValueException()
            };
        }
    }
}
