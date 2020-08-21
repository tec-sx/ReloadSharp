using System.Collections.Generic;
using System.Linq;

namespace Reload.Core.Graphics.Rendering.Shaders
{
    /// <summary>
    /// The shader uniform buffer declaration base.
    /// </summary>
    public abstract class ShaderUniformBufferDeclaration
    {
        /// <summary>
        /// Gets the uniforms internal list.
        /// </summary>
        protected List<ShaderUniformDeclaration> UniformsInternal { get; }

        /// <summary>
        /// Gets the uniforms list.
        /// </summary>
        public IReadOnlyList<ShaderUniformDeclaration> Uniforms => UniformsInternal.AsReadOnly();

        /// <summary>
        /// Gets the uniform name.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Gets the uniform register.
        /// </summary>
        public uint Register { get; init; }

        /// <summary>
        /// Gets or sets the uniform size.
        /// </summary>
        public uint Size { get; protected set; }

        /// <summary>
        /// Finds the uniform by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>A ShaderUniformDeclaration.</returns>
        public abstract ShaderUniformDeclaration FindUniform(string name);

        /// <summary>
        /// Initializes a new instance of the <see cref="ShaderUniformBufferDeclaration"/> class.
        /// </summary>
        public ShaderUniformBufferDeclaration()
        {
            UniformsInternal = new List<ShaderUniformDeclaration>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShaderUniformBufferDeclaration"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="register">The register.</param>
        /// <param name="size">The size.</param>
        public ShaderUniformBufferDeclaration(string name, uint register, uint size)
            : base()
        {
            Name = name;
            Register = register;
            Size = size;
        }
    }
}
