namespace Reload.Rendering
{
    using Silk.NET.OpenGL;
    using System;
    using System.Collections.Generic;

    public delegate ShaderProgram CreateShaderDelegate(Dictionary<ShaderType, string> shaderFiles, List<string> attributes);

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
        /// Creates (Compiles adds attributes and then links) a new shader program from
        /// the shader files dictionary and attribute list.
        /// </summary>
        /// <param name="shaderFiles"></param>
        /// <param name="attributes"></param>
        /// <returns cref="IShaderProgram"></returns>
        /// <exception cref="ApplicationException"></exception>
        public static CreateShaderDelegate Create;
    }
}