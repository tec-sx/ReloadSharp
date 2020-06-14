namespace Reload.Platform.Graphics.OpenGl
{
    using Reload.Platform.Graphics.OpenGl.Properties;
    using Reload.Rendering.Structures;
    using Silk.NET.OpenGL;
    using System;

    internal static class Utils
    {
        public static GLEnum ShaderDataTypeToGlBaseType(ShaderDataType type)
        {
            return type switch
            {
                ShaderDataType.Float => GLEnum.Float,
                ShaderDataType.Float2 => GLEnum.Float,
                ShaderDataType.Float3 => GLEnum.Float,
                ShaderDataType.Float4 => GLEnum.Float,
                ShaderDataType.Mat3 => GLEnum.Float,
                ShaderDataType.Mat4 => GLEnum.Float,
                ShaderDataType.Int => GLEnum.Int,
                ShaderDataType.Int2 => GLEnum.Int,
                ShaderDataType.Int3 => GLEnum.Int,
                ShaderDataType.Int4 => GLEnum.Int,
                ShaderDataType.Bool => GLEnum.Bool,
                ShaderDataType.None => GLEnum.None,
                _ => throw new ApplicationException(Resources.UnknownShaderDataType)
            };
        }
    }
}
