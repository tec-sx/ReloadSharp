#region copyright
/*
-----------------------------------------------------------------------------
Copyright (c) 2020 Ivan Trajchev

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
-----------------------------------------------------------------------------
*/
#endregion
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
