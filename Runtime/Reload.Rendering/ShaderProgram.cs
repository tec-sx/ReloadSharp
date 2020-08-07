using Silk.NET.OpenGL;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Reload.Rendering
{
    /// <summary>
    /// The shader program abstract class.
    /// </summary>
    public abstract class ShaderProgram: IDisposable
    {

        protected readonly Dictionary<string, int> UniformLocationCache;

        /// <summary>
        /// Gets or sets shader the name.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShaderProgram"/> class.
        /// </summary>
        protected ShaderProgram()
        {
            UniformLocationCache = new Dictionary<string, int>();
        }

        /// <summary>
        /// Pre-processes single file shader source. Add "#type [shader type]" above 
        /// the source code for the specific shader. Available shader types are:
        /// "vertex", "fragment", "geometry", "compute", "tess_control" and "tess_evaluation"
        /// </summary>
        /// <param name="source">The shader source.</param>
        /// <returns>
        /// A Dictionary with the shader type as Key, and the shader source as Value.
        /// </returns>
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
        /// Binds the current program for using.
        /// </summary>
        public abstract void Bind();

        /// <summary>
        /// Clean up the resources.
        /// </summary>
        public abstract void Dispose();

        /// <summary>
        /// Creates (Compiles, adds attributes and then links) a new shader program from
        /// the shader file and attribute list.
        /// </summary>
        /// <exception cref="ApplicationException"></exception>
        //public static CreateShaderDelegate Create;
        public static Func<string, List<string>, ShaderProgram> Create;
    }
}