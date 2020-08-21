namespace Reload.Core.Graphics.Rendering.Shaders
{
    /// <summary>
    /// The shader resource declaration base.
    /// </summary>
    public abstract class ShaderResourceDeclaration
    {
        /// <summary>
        /// Gets the resource name.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Gets the resource register.
        /// </summary>
        public uint Register { get; init; }


        /// <summary>
        /// Gets the resource count.
        /// </summary>
        public uint Count { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShaderResourceDeclaration"/> class.
        /// </summary>
        public ShaderResourceDeclaration()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShaderResourceDeclaration"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="count">The count.</param>
        /// <param name="register">The register.</param>
        public ShaderResourceDeclaration(string name, uint count, uint register)
        {
            Name = name;
            Count = count;
            Register = register;
        }
    }
}
