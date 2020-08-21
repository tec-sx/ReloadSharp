namespace Reload.Core.Graphics.Rendering.Shaders
{
    public enum ShaderDomain
    {
        None = 0,
        Vertex = 0,
        Pixel = 1
    }

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

    /// <summary>
    /// The shader uniform declaration base.
    /// </summary>
    public abstract record ShaderUniformDeclaration
    {
        /// <summary>
        /// Gets or sets the shader uniform offset.
        /// </summary>
        public abstract uint Offset { get;  init; }

        /// <summary>
        /// Gets the shader uniform name.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Gets the uniform size.
        /// </summary>
        public uint Size { get; init; }

        /// <summary>
        /// Gets the uniform count.
        /// </summary>
        public uint Count { get; init; }

        /// <summary>
        /// Gets the shader domain.
        /// </summary>
        public ShaderDomain Domain { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShaderUniformDeclaration"/> class.
        /// </summary>
        public ShaderUniformDeclaration()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShaderUniformDeclaration"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="size">The size.</param>
        /// <param name="count">The count.</param>
        /// <param name="domain">The domain.</param>
        public ShaderUniformDeclaration(string name, uint size, uint count, ShaderDomain domain)
        {
            Name = name;
            Size = size;
            Count = count;
            Domain = domain;
        }
    }
}
