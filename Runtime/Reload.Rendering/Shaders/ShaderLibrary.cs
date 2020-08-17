using Reload.Core.Graphics.Rendering.Shaders;
using System.Collections.Generic;

namespace Reload.Rendering.Shaders
{
    /// <summary>
    /// The shader library.
    /// </summary>
    public class ShaderLibrary : Dictionary<string, ShaderProgram>
    {
        /// <summary>
        /// Adds a <see cref="ShaderProgram"/> to the library.
        /// </summary>
        /// <param name="shader">The shader program.</param>
        public new void Add(ShaderProgram shader)
        {
            base.Add(shader.Name, shader);
        }

        /// <summary>
        /// Loads new shader program from file and adds it to the library.
        /// </summary>
        /// <param name="fileName">The shader file name.</param>
        /// <param name="attributes">The shader attributes.</param>
        /// <returns>A ShaderProgram.</returns>
        public ShaderProgram Load(string fileName, List<string> attributes = null)
        {
            var shader = ShaderProgram.Create(fileName, attributes);
            base.Add(fileName, shader);

            return shader;
        }
    }
}
