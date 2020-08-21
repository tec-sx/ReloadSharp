using Reload.Core.Graphics.Rendering.Shaders;

namespace Reload.Platform.Graphics.OpenGl.Shaders
{
    /// <summary>
    /// The OpenGl shader uniform buffer declaration implementation.
    /// </summary>
    internal sealed class OpenGlShaderUniformBufferDeclaration : ShaderUniformBufferDeclaration
    {
        /// <summary>
        /// Gets the shader domain.
        /// </summary>
        public ShaderDomain Domain { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGlShaderUniformBufferDeclaration"/> class.
        /// </summary>
        public OpenGlShaderUniformBufferDeclaration()
            : base()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGlShaderUniformBufferDeclaration"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="domain">The domain.</param>
        public OpenGlShaderUniformBufferDeclaration(string name, ShaderDomain domain)
            : base(name, 0, 0)
        {
            Domain = domain;
        }

        /// <summary>
        /// Pushes new uniform to the uniforms list.
        /// </summary>
        /// <param name="uniform">The uniform.</param>
        public void PushUniform(OpenGlShaderUniformDeclaration uniform)
        {
            uint offset = 0;
            if (UniformsInternal.Count > 0)
            {
                var previousUniform = UniformsInternal[UniformsInternal.Count - 1];
                offset = previousUniform.Offset + previousUniform.Size;
            }

            uniform = uniform with { Offset = offset };
            Size += uniform.Size;
            UniformsInternal.Add(uniform);
        }

        /// <summary>
        /// Finds uniform by name.
        /// </summary>
        /// <param name="name">The uniform name.</param>
        /// <returns>A ShaderUniformDeclaration.</returns>
        public override ShaderUniformDeclaration FindUniform(string name)
        {
            foreach(var uniform in UniformsInternal)
            {
                if (uniform.Name == name)
                {
                    return uniform;
                }
            }

            return null;
        }
    }
}
