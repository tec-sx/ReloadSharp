namespace Reload.Rendering
{
    using Reload.Core.Utils;
    using System.Collections.Generic;

    public class ShaderLibrary : Dictionary<string, ShaderProgram>
    {

        public new void Add(ShaderProgram shader)
        {
            Add(shader.Name, shader);
        }

        public ShaderProgram Load(string fileName, List<string> attributes = null)
        {
            var shader = ShaderProgram.Create(fileName, attributes);
            Add(fileName, shader);

            return shader;
        }
    }
}
