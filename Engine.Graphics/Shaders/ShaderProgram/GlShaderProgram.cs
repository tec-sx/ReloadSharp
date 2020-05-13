namespace Engine.ResourcesPipeline.Shaders.ShaderProgram
{
    using Engine.Utilities.IO;
    using Silk.NET.OpenGL;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// OpenGL shader program class.
    /// </summary>
    public class GlShaderProgram : IShaderProgram
    {
        /// <summary>
        /// Shader file extension.
        /// </summary>
        private const string SHADER_EXT = "glsl";

        /// <summary>
        /// OpenGL native api handle.
        /// </summary>
        private readonly GL gl;

        /// <summary>
        /// A temporary shader list to store compiled shaders ready
        /// for linking.
        /// </summary>
        private readonly List<uint> shadersTemp;

        /// <summary>
        /// Blocks <see cref="CompileShader(ShaderType, string)"/>
        /// and <see cref="AddAttribute(string)"/> methods
        /// to be called after linkig is complete (<see cref="LinkShaders()"/>
        /// is called).
        /// </summary>
        private bool linkingIsComplete;

        /// <summary>
        /// The attribute indexer used to bind
        /// to the correct location.
        /// </summary>
        private uint numberOfAttributes;

        /// <summary>
        /// The shader program handle.
        /// </summary>
        public uint ProgramHandle { get; }

        /// <summary>
        /// Creates new shader program <see cref="ProgramHandle"/> and intializes
        /// temporary list <see cref="shadersTemp"/> to store compiled shaders before
        /// linking.
        /// </summary>
        /// <param name="glApi"></param>
        public GlShaderProgram()
        {
            gl = GL.GetApi();
            ProgramHandle = gl.CreateProgram();
            shadersTemp = new List<uint>();
            linkingIsComplete = false;
            numberOfAttributes = 0;
        }

        /// <summary>
        /// Clean up managed resources.
        /// </summary>
        public void CleanUp()
        {
            gl.UseProgram(0);
            gl.DeleteProgram(ProgramHandle);
        }

        /// <summary>
        /// Compiles the shaders and stores them in a temporary list ready for linking.
        /// If linking is already complete for current program it logs a warning and continues
        /// without execution.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="shaderName"></param>
        /// <exception cref="ApplicationException"></exception>
        public void CompileShader(ShaderType type, string shaderName)
        {
            if (linkingIsComplete)
            {
                Console.WriteLine(Graphics.Properties.Resources.ShaderCantCompile);
                return;
            }

            var shaderNameWithExt = $".{shaderName}.{SHADER_EXT}";

            var shaderResourceName = GetType().Assembly
                .GetManifestResourceNames()
                .First(resourceName => resourceName.EndsWith(shaderNameWithExt, StringComparison.InvariantCulture));

            var assembly = Assembly.GetExecutingAssembly();
            var shaderSource = EmbeddedResourceUtility.LoadEmbeddedResourceString(assembly, shaderResourceName);

            var handle = gl.CreateShader(type);

            gl.ShaderSource(handle, shaderSource);
            gl.CompileShader(handle);

            var infoLog = gl.GetShaderInfoLog(handle);

            if (!string.IsNullOrWhiteSpace(infoLog))
            {
                gl.DeleteShader(handle);
                throw new ApplicationException($"Error compiling shader of type {type}, failed with error {infoLog}");
            }

            shadersTemp.Add(handle);
        }

        /// <summary>
        /// Adds an attribute to our shader. Should be called between compiling and linking.
        /// If linking is already complete for current program it logs a warning and continues
        /// without execution.
        /// </summary>
        /// <param name="attributeName"></param>
        public void AddAttribute(string attributeName)
        {
            if (linkingIsComplete)
            {
                Console.WriteLine(Graphics.Properties.Resources.ShaderCantAddAttribute);
                return;
            }

            gl.BindAttribLocation(ProgramHandle, numberOfAttributes++, attributeName);
        }

        /// <summary>
        /// Links the shaders stored in the shader temporary list. After successful
        /// linking, 'CompileShader()' and 'AddAttribute()' methods can no longer be called
        /// for the current program.
        /// </summary>
        /// <exception cref="ApplicationException"></exception>
        public void LinkShaders()
        {
            if (linkingIsComplete)
            {
                Console.WriteLine(Graphics.Properties.Resources.ShaderCantLink);
                return;
            }

            shadersTemp.ForEach(shaderHandle => gl.AttachShader(ProgramHandle, shaderHandle));
            gl.LinkProgram(ProgramHandle);

            var programInfoLog = gl.GetProgramInfoLog(ProgramHandle);

            if (!string.IsNullOrWhiteSpace(programInfoLog))
            {
                shadersTemp.ForEach(shader => gl.DeleteShader(shader));
                CleanUp();

                throw new ApplicationException($"Program failed to link with error: {programInfoLog}");
            }

            shadersTemp.ForEach(shaderHandle =>
            {
                gl.DetachShader(ProgramHandle, shaderHandle);
                gl.DeleteShader(shaderHandle);
            });

            linkingIsComplete = true;
        }

        /// <summary>
        /// Sets integer value to an uniform location.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <exception cref="ApplicationException"></exception>
        public void SetUniform(string name, int value)
        {
            var location = gl.GetUniformLocation(ProgramHandle, name);

            if (location == -1)
            {
                throw new ApplicationException($"{name} uniform not found on shader.");
            }

            Use();

            gl.Uniform1(location, value);
        }

        /// <summary>
        /// Sets floating point value to an uniform location.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <exception cref="ApplicationException"></exception>
        public void SetUniform(string name, float value)
        {
            var location = gl.GetUniformLocation(ProgramHandle, name);

            if (location == -1)
            {
                throw new ApplicationException($"{name} uniform not found on shader.");
            }

            Use();

            gl.Uniform1(location, value);
        }

        /// <summary>
        /// Use the current program.
        /// </summary>
        public void Use()
        {
            gl.UseProgram(ProgramHandle);
        }
    }
}