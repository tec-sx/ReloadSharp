using Reload.Core.Graphics.Rendering.Shaders;

namespace Reload.Platform.Graphics.OpenGl.Shaders
{
    /// <summary>
    /// The OpenGl shader uniform declaration implementation.
    /// </summary>
    internal sealed record OpenGlShaderUniformDeclaration : ShaderUniformDeclaration
    {
        private uint _offset;

        /// <summary>
        /// Gets the uniform location.
        /// </summary>
        public int Location { get; }

        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        public override uint Offset
        {
            get => _offset;
            init
            {
                if (Type == UniformType.Struct)
                {
                    Struct.Offset = value;
                }

                _offset = value;
            }
        }

        /// <summary>
        /// Gets the uniform type.
        /// </summary>
        public UniformType Type { get; }

        /// <summary>
        /// Gets the shader struct.
        /// </summary>
        public ShaderStruct Struct { get; }

        /// <summary>
        /// Gets the absolute offset.
        /// </summary>
        public uint AbsoluteOffset => Struct != null ? Struct.Offset + Offset : Offset;

        /// <summary>
        /// Gets a value indicating whether the uniform is array.
        /// </summary>
        public bool IsArray => Count > 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGlShaderUniformDeclaration"/> class.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <param name="type">The type.</param>
        /// <param name="name">The name.</param>
        /// <param name="count">The count.</param>
        public OpenGlShaderUniformDeclaration(ShaderDomain domain, UniformType type, string name, uint count)
            : base(name, SizeOfUniformType(type), count, domain)
        {
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGlShaderUniformDeclaration"/> class.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <param name="uniformStruct">The uniform struct.</param>
        /// <param name="name">The name.</param>
        /// <param name="count">The count.</param>
        public OpenGlShaderUniformDeclaration(ShaderDomain domain, ShaderStruct uniformStruct, string name, uint count)
            : base(name, uniformStruct.Size * count, count, domain)
        {
            Type = UniformType.Struct;
            Struct = uniformStruct;
        }

        /// <summary>
        /// Gets the size of the uniform type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>An uint.</returns>
        public static uint SizeOfUniformType(UniformType type)
        {
            return type switch
            {
                UniformType.Int32 => 4,
                UniformType.Float32 => 4,
                UniformType.Vec2 => 4 * 2,
                UniformType.Vec3 => 4 * 3,
                UniformType.Vec4 => 4 * 4,
                UniformType.Mat3 => 4 * 3 * 3,
                UniformType.Mat4 => 4 * 4 * 4,
                _ => 0
            };
        }

        /// <summary>
        /// Converts the uniform type string the to uniform type enum value.
        /// </summary>
        /// <param name="type">The string uniform type.</param>
        /// <returns>An UniformType.</returns>
        public static UniformType StringToType(string type)
        {
            return type switch
            {
                "int" => UniformType.Int32,
                "float" => UniformType.Float32,
                "vec2" => UniformType.Vec2,
                "vec3" => UniformType.Vec3,
                "vec4" => UniformType.Vec4,
                "mat3" => UniformType.Mat3,
                "mat4" => UniformType.Mat4,
                _ => UniformType.None
            };
        }

        /// <summary>
        /// Converts an uniform type enum value to string.
        /// </summary>
        /// <param name="type">The uniform type enum value.</param>
        /// <returns>A string.</returns>
        public static string TypeToString(UniformType type)
        {
            return type switch
            {
                UniformType.Int32 => "int32",
                UniformType.Float32 => "float",
                UniformType.Vec2 => "vec2",
                UniformType.Vec3 => "vec3",
                UniformType.Vec4 => "vec4",
                UniformType.Mat3 => "mat3",
                UniformType.Mat4 => "mat4",
                _ => "Invalid type"
            };
        }
    }
}
