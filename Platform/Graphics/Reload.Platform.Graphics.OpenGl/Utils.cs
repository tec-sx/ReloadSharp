namespace Reload.Platform.Graphics.OpenGl
{
    using Reload.Platform.Graphics.OpenGl.Properties;
    using Reload.Rendering.Structures;
    using Silk.NET.OpenGL;
    using System;
    using System.Collections.Generic;

    public static class Utils
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

        public static TextureUnit TextureSlotIdToTextureUnit(uint slot)
        {
            return slot switch
            {
                0 => TextureUnit.Texture0,
                1 => TextureUnit.Texture1,
                2 => TextureUnit.Texture2,
                3 => TextureUnit.Texture3,
                4 => TextureUnit.Texture4,
                5 => TextureUnit.Texture5,
                6 => TextureUnit.Texture6,
                7 => TextureUnit.Texture7,
                8 => TextureUnit.Texture8,
                9 => TextureUnit.Texture9,
                10 => TextureUnit.Texture10,
                11 => TextureUnit.Texture11,
                12 => TextureUnit.Texture12,
                13 => TextureUnit.Texture13,
                14 => TextureUnit.Texture14,
                15 => TextureUnit.Texture15,
                16 => TextureUnit.Texture16,
                17 => TextureUnit.Texture17,
                18 => TextureUnit.Texture18,
                19 => TextureUnit.Texture19,
                20 => TextureUnit.Texture20,
                21 => TextureUnit.Texture21,
                22 => TextureUnit.Texture22,
                23 => TextureUnit.Texture23,
                24 => TextureUnit.Texture24,
                25 => TextureUnit.Texture25,
                26 => TextureUnit.Texture26,
                27 => TextureUnit.Texture27,
                28 => TextureUnit.Texture28,
                29 => TextureUnit.Texture29,
                30 => TextureUnit.Texture30,
                31 => TextureUnit.Texture31,
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
