namespace Reload.Rendering
{
    using Silk.NET.OpenGL;
    using System;
    using System.Collections.Generic;
    using System.Numerics;

    //public delegate ShaderProgram CreateShaderDelegate(string source, List<string> attributes);

    public abstract class ShaderProgram
    {

        protected readonly Dictionary<string, int> UniformLocationCache;

        public string Name { get; protected set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        protected ShaderProgram()
        {
            UniformLocationCache = new Dictionary<string, int>();
        }

        /// <summary>
        /// Split the shader string in corresponding shader types.
        /// </summary>
        /// <param name="shaderString"></param>
        public abstract Dictionary<ShaderType, string> PreProcessShader(string shaderString);

        /// <summary>
        /// Compiles the shaders and stores them in a temporary list ready for linking.
        /// If linking is already complete for current program it logs a warning and continues
        /// without execution.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="shaderString"></param>
        public abstract void CompileShader(ShaderType type, string shaderString);

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
        /// Gets uniform location.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public abstract int GetUniform(string name);

        /// <summary>
        /// Sets floating point value to an uniform location.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public abstract void SetUniform(string name, float value);

        /// <summary>
        /// Sets Matrix 4x4 value to an uniform location.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public abstract void SetUniform(string name, Matrix4x4 value);

        /// <summary>
        /// Sets Vector 4 value to an uniform location.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public abstract void SetUniform(string name, Vector4 value);

        /// <summary>
        /// Sets Vector 3 value to an uniform location.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public abstract void SetUniform(string name, Vector3 value);

        /// <summary>
        /// Use (Bind) the current program.
        /// </summary>
        public abstract void Use();

        /// <summary>
        /// Clean up the resources.
        /// </summary>
        public abstract void CleanUp();

        /// <summary>
        /// Creates (Compiles, adds attributes and then links) a new shader program from
        /// the shader file and attribute list.
        /// </summary>
        /// <exception cref="ApplicationException"></exception>
        //public static CreateShaderDelegate Create;
        public static Func<string, List<string>, ShaderProgram> Create;
    }
}