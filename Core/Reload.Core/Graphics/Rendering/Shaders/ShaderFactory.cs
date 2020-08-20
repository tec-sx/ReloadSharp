using System.Collections.Generic;

namespace Reload.Core.Graphics.Rendering.Shaders
{
    /// <summary>
    /// The shader factory implementation.
    /// </summary>
    public abstract class ShaderFactory
    {

        /// <summary>
        /// Creates a new shader from the passed file.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <param name="attributes">The attributes.</param>
        /// <returns>A ShaderProgram.</returns>
        protected internal abstract ShaderProgram CreateShaderProgram(string fileName, List<string> attributes);
    }
}
