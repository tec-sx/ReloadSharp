namespace Reload.Platform.Graphics.OpenGl.Structures
{
    using Reload.Rendering.Structures;
    using System.Collections.Generic;

    public class GlShaderUniformBufferDeclaration : ShaderUniformBufferDeclaration
    {
        public ShaderDomain Domain { get; }

        public GlShaderUniformBufferDeclaration(string name, ShaderDomain domain)
            : base(name, 0, 0)
        {
            Domain = domain;
        }

        public void PushUniform(GlShaderUniformDeclaration uniform)
        {
            uint offset = 0;
            if (uniforms.Count > 0)
            {
                var previousUniform = uniforms[uniforms.Count - 1];
                offset = previousUniform.Offset + previousUniform.Size;
            }

            uniform.Offset = offset;
            Size += uniform.Size;
            uniforms.Add(uniform);
        }

        public override ShaderUniformDeclaration FindUniform(string name)
        {
            foreach(var uniform in uniforms)
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
