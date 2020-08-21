using Reload.Core.Graphics.Rendering.Shaders;

namespace Reload.Platform.Graphics.OpenGl.Shaders
{
    public enum ResourceType
    {
        None,
        Texture2d,
        TextureCube
    }

    /// <summary>
    /// The OpenGl shader resource declaration implementation.
    /// </summary>
    internal sealed class OpenGlShaderResourceDeclaration : ShaderResourceDeclaration
    {
        /// <summary>
        /// Gets the resource type.
        /// </summary>
        public ResourceType Type { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGlShaderResourceDeclaration"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="name">The name.</param>
        /// <param name="count">The count.</param>
        public OpenGlShaderResourceDeclaration(ResourceType type, string name, uint count)
            : base(name, count, 0)
        {
            Type = type;
        }

        /// <summary>
        /// Converts string to resource type.
        /// </summary>
        /// <param name="type">The resource type string.</param>
        /// <returns>A ResourceType.</returns>
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

        /// <summary>
        /// Convertes resource type enum to string.
        /// </summary>
        /// <param name="type">The type enum value.</param>
        /// <returns>A string.</returns>
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
