namespace Reload.Rendering
{
    using Reload.Rendering.Platform.OpenGl;
    using Silk.NET.OpenGL;
    using Silk.NET.Windowing.Common;
    using System;
    using System.Collections.Generic;

    public abstract class ShaderProgram
    {
        /// <summary>
        /// Compiles the shaders and stores them in a temporary list ready for linking.
        /// If linking is already complete for current program it logs a warning and continues
        /// without execution.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="shaderName"></param>
        public abstract void CompileShader(ShaderType type, string shaderName);

        /// <summary>
        /// Adds an attribute to our shader. Should be called between compiling and linking.
        /// If linking is already complete for current program it logs a warning and continues
        /// without execution.
        /// </summary>
        /// <param name="attributeName"></param>
        public abstract void AddAttribute(string attributeName);

        /// <summary>
        /// Links the shaders stored in the shader temporary list. After successful
        /// linking, 'CompileShader()' and 'AddAttribute()' methods can no longer be called
        /// for the current program.
        /// </summary>
        public abstract void LinkShaders();

        /// <summary>
        /// Sets integer value to an uniform location.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public abstract void SetUniform(string name, int value);

        /// <summary>
        /// Sets floating point value to an uniform location.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public abstract void SetUniform(string name, float value);

        /// <summary>
        /// Use the current program.
        /// </summary>
        public abstract void Use();

        /// <summary>
        /// Clean up the resources.
        /// </summary>
        public abstract void CleanUp();

        /// <summary>
        /// Creates (Compiles and links) a new shader program from
        /// the shader files dictionary.
        /// </summary>
        /// <param name="shaderFiles"></param>
        /// <returns cref="IShaderProgram"></returns>
        /// <exception cref="ApplicationException"></exception>
        public static ShaderProgram CreateShader(Dictionary<ShaderType, string> shaderFiles)
        {
            return CreateShader(shaderFiles, null);
        }

        /// <summary>
        /// Creates (Compiles adds attributes and then links) a new shader program from
        /// the shader files dictionary and attribute list.
        /// </summary>
        /// <param name="shaderFiles"></param>
        /// <param name="attributes"></param>
        /// <returns cref="IShaderProgram"></returns>
        /// <exception cref="ApplicationException"></exception>
        public static ShaderProgram CreateShader(Dictionary<ShaderType, string> shaderFiles, List<string> attributes)
        {
            ShaderProgram shaderProgram;

            if (shaderFiles == null || shaderFiles.Count == 0)
            {
                throw new ApplicationException(Properties.Resources.ShaderDictionaryNullOrEmpty);
            }

            if (RendererApi.Api == ContextAPI.OpenGL)
            {
                shaderProgram = new GlShaderProgram();
            }
            else
            {
                throw new ApplicationException(Properties.Resources.BackendNotSupportedError);
            }

            foreach (var (shaderType, shaderFile) in shaderFiles)
            {
                shaderProgram.CompileShader(shaderType, shaderFile);
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