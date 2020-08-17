using System.Collections.Generic;

namespace Reload.Core.Graphics.Rendering.Shaders
{
    /// <summary>
    /// The shader factory implementation.
    /// </summary>
    public abstract class ShaderFactoryImplementation
    {

        /// <summary>
        /// Creates a new shader from the passed file.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <param name="attributes">The attributes.</param>
        /// <returns>A ShaderProgram.</returns>
        public abstract ShaderProgram ShaderProgram(string fileName, List<string> attributes);
    }
}
