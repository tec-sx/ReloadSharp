using System.Collections.Generic;

namespace Reload.Rendering.Shaders
{
    public abstract class ShaderUniformBufferDeclaration
    {
        protected List<ShaderUniformDeclaration> uniforms;
        public IReadOnlyList<ShaderUniformDeclaration> Uniforms => uniforms;

        public string Name { get; }
        public uint Register { get; protected set; }
        public uint Size { get; protected set; }
        public abstract ShaderUniformDeclaration FindUniform(string name);

        public ShaderUniformBufferDeclaration(string name, uint register, uint size)
        {
            Name = name;
            Register = register;
            Size = size;
        }
    }
}
