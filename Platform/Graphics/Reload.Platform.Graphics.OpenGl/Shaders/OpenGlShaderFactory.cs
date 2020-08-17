using Reload.Configuration;
using Reload.Core.Graphics.Rendering.Shaders;
using Silk.NET.OpenGL;
using System.Collections.Generic;
using System.IO;

namespace Reload.Platform.Graphics.OpenGl.Shaders
{
    /// <summary>
    /// The OpenGL shader factory implementation.
    /// </summary>
    public class OpenGlShaderFactory : ShaderFactoryImplementation
    {
        private readonly GL _api;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGlShaderFactory"/> class.
        /// </summary>
        /// <param name="api">The api.</param>
        public OpenGlShaderFactory(GL api)
        {
            _api = api;
        }

        /// <inheritdoc/>
        public override ShaderProgram ShaderProgram(string fileName, List<string> attributes)
        {
            string shaderFile = Path.Combine(ContentPaths.Shaders, $"{fileName}.glsl");
            string shaderString = File.ReadAllText(shaderFile);

            ShaderProgram shaderProgram = new OpenGlShaderProgram(fileName, _api);

            var shaderSources = shaderProgram.PreProcessShader(shaderString);

            foreach (var (shaderType, shaderSource) in shaderSources)
            {
                shaderProgram.CompileShader(shaderType, shaderSource);
            }

            if (attributes != null && attributes.Count > 0)
            {
                attributes.ForEach(attribute => shaderProgram.AddAttribute(attribute));
            }

            shaderProgram.LinkShaders();

            return shaderProgram;
        }
    }
}
