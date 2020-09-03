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
