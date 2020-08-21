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
