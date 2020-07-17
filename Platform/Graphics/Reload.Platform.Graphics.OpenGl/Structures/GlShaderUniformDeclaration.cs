namespace Reload.Platform.Graphics.OpenGl.Structures
{
    using Reload.Rendering.Structures;

    public enum UniformType
    {
        None,
        Float32,
        Vec2,
        Vec3,
        Vec4,
        Mat3,
        Mat4,
        Int32,
        Struct
    }

    public class GlShaderUniformDeclaration : ShaderUniformDeclaration
    {
        public int Location { get; }

        public override uint Offset
        {
            get => offset;
            set
            {
                if (Type == UniformType.Struct)
                {
                    Struct.Offset = value;
                }

                offset = value;
            }
        }
        public UniformType Type { get; }
        public ShaderStruct Struct { get; }
        public uint AbsoluteOffset => Struct != null ? Struct.Offset + Offset : Offset;
        public bool IsArray => Count > 1;

        public GlShaderUniformDeclaration(ShaderDomain domain, UniformType type, string name, uint count)
            : base(name, SizeOfUniformType(type), count, domain)
        {
            Type = type;
        }

        public GlShaderUniformDeclaration(ShaderDomain domain, ShaderStruct uniformStruct, string name, uint count)
            : base(name, uniformStruct.Size * count, count, domain)
        {
            Type = UniformType.Struct;
            Struct = uniformStruct;
        }

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
