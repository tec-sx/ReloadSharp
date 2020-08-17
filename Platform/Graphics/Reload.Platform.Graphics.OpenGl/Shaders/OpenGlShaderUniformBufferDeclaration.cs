using Reload.Core.Graphics.Rendering.Shaders;

namespace Reload.Platform.Graphics.OpenGl.Shaders
{
    public class OpenGlShaderUniformBufferDeclaration : ShaderUniformBufferDeclaration
    {
        public ShaderDomain Domain { get; }

        public OpenGlShaderUniformBufferDeclaration(string name, ShaderDomain domain)
            : base(name, 0, 0)
        {
            Domain = domain;
        }

        public void PushUniform(OpenGlShaderUniformDeclaration uniform)
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
