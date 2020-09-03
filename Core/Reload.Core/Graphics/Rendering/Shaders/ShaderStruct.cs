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
    using Reload.Core.Exceptions;
    using System.Collections.Generic;

    /// <summary>
    /// The shader struct base.
    /// </summary>
    public abstract class ShaderStruct
    {
        private List<ShaderUniformDeclaration> _fields;

        /// <summary>
        /// Gets the shader fields.
        /// </summary>
        public IReadOnlyList<ShaderUniformDeclaration> Fields => _fields;

        /// <summary>
        /// Gets the shader name.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Gets the shader size.
        /// </summary>
        public uint Size { get; private set; }

        /// <summary>
        /// Gets or sets the shader offset.
        /// </summary>
        public uint Offset { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShaderStruct"/> class.
        /// </summary>
        public ShaderStruct()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShaderStruct"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ShaderStruct(string name)
        {
            Name = name;
            Size = 0;
            Offset = 0;
            _fields = new List<ShaderUniformDeclaration>(8);
        }

        /// <summary>
        /// Adds a shader uniform field.
        /// </summary>
        /// <param name="field">The field.</param>
        public void AddField(ShaderUniformDeclaration field)
        {
            if (field == null)
            {
                throw new ReloadArgumentNullException();
            }

            Size += field.Size;

            uint offset = 0;

            if (_fields.Count > 0)
            {
                var previousField = _fields[_fields.Count - 1];
                offset = previousField.Offset + previousField.Size;
            }

            field = field with { Offset = offset };
            _fields.Add(field);
        }
    }
}
