namespace Engine.Graphics
{
    using Engine.ResourcesPipeline.Shaders.ShaderProgram;
    using Silk.NET.OpenGL;
    using Silk.NET.Windowing.Common;
    using System.Collections.Generic;

    public interface IGraphicsManager
    {
        /// <summary>
        /// Creates a new Silk.NET window with the provided configuration.
        /// </summary>
        /// <param name="displayConfiguration"></param>
        /// <returns cref="IWindow"></returns>
        IWindow CreateWindow(DisplayConfiguration displayConfiguration);

        /// <summary>
        /// Creates (Compiles and links) a new shader program from
        /// the shader files dictionary.
        /// </summary>
        /// <param name="shaderFiles"></param>
        /// <returns cref="IShaderProgram"></returns>
        public IShaderProgram CreateShader(Dictionary<ShaderType, string> shaderFiles);

        /// <summary>
        /// Creates (Compiles adds attributes and then links) a new shader program from
        /// the shader files dictionary and attribute list.
        /// </summary>
        /// <param name="shaderFiles"></param>
        /// <param name="attributes"></param>
        /// <returns cref="IShaderProgram"></returns>
        public IShaderProgram CreateShader(Dictionary<ShaderType, string> shaderFiles, List<string> attributes);
    }
}
