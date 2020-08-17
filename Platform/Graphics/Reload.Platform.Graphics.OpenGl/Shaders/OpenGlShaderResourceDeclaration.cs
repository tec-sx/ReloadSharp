using Reload.Core.Graphics.Rendering.Shaders;

namespace Reload.Platform.Graphics.OpenGl.Shaders
{
    public class OpenGlShaderResourceDeclaration : ShaderResourceDeclaration
    {
        public enum ResourceType
        {
            None,
            Texture2d,
            TextureCube
        }
    
        public ResourceType Type { get; }

        public OpenGlShaderResourceDeclaration(ResourceType type, string name, uint count)
            : base(name, count, 0)
        {
            Type = type;
        }

        public static ResourceType StringToType(string type)
        {
            return type switch
            {
                "sampler2D" => ResourceType.Texture2d,
                "sampler2DMS" => ResourceType.Texture2d,
                "samplerCube" => ResourceType.TextureCube,
                _ => ResourceType.None
            };
        }

        public static string TypeToString(ResourceType type)
        {
            return type switch
            {
                ResourceType.Texture2d => "sampler2D",
                ResourceType.TextureCube => "samplerCube",
                _ => "Invalid type"
            };
        }
    }
}
