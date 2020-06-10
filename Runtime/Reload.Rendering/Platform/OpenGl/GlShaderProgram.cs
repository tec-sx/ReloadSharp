namespace Reload.Rendering.Platform.OpenGl
{
    using Reload.Core.IO;
    using Silk.NET.OpenGL;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// OpenGL shader program class.
    /// </summary>
    public class GlShaderProgram : ShaderProgram
    {
        /// <summary>
        /// Shader file extension.
        /// </summary>
        private const string SHADER_EXT = "glsl";

        /// <summary>
        /// OpenGL native api handle.
        /// </summary>
        private readonly GL _gl;

        /// <summary>
        /// A temporary shader list to store compiled shaders ready
        /// for linking.
        /// </summary>
        private readonly List<uint> _shadersTemp;

        /// <summary>
        /// Blocks <see cref="CompileShader(ShaderType, string)"/>
        /// and <see cref="AddAttribute(string)"/> methods
        /// to be called after linkig is complete (<see cref="LinkShaders()"/>
        /// is called).
        /// </summary>
        private bool _linkingIsComplete;

        /// <summary>
        /// The attribute indexer used to bind
        /// to the correct location.
        /// </summary>
        private uint _numberOfAttributes;

        /// <summary>
        /// The shader program handle.
        /// </summary>
        public uint ProgramHandle { get; }

        /// <summary>
        /// Creates new shader program <see cref="ProgramHandle"/> and intializes
        /// temporary list <see cref="_shadersTemp"/> to store compiled shaders before
        /// linking.
        /// </summary>
        /// <param name="glApi"></param>
        public GlShaderProgram()
        {
            _gl = GlRenderer.Gl;
            ProgramHandle = _gl.CreateProgram();
            _shadersTemp = new List<uint>();
            _linkingIsComplete = false;
            _numberOfAttributes = 0;
        }

        /// <summary>
        /// Clean up managed resources.
        /// </summary>
        public override void CleanUp()
        {
            _gl.UseProgram(0);
            _gl.DeleteProgram(ProgramHandle);
        }

        /// <summary>
        /// Compiles the shaders and stores them in a temporary list ready for linking.
        /// If linking is already complete for current program it logs a warning and continues
        /// without execution.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="shaderName"></param>
        /// <exception cref="ApplicationException"></exception>
        public override void CompileShader(ShaderType type, string shaderName)
        {
            if (_linkingIsComplete)
            {
                Console.WriteLine(Properties.Resources.ShaderCantCompile);
                return;
            }

            var shaderNameWithExt = $".{shaderName}.{SHADER_EXT}";

            var shaderResourceName = GetType().Assembly
                .GetManifestResourceNames()
                .First(resourceName => resourceName.EndsWith(shaderNameWithExt, StringComparison.InvariantCulture));

            var assembly = Assembly.GetExecutingAssembly();
            var shaderSource = EmbeddedResources.LoadResourceString(assembly, shaderResourceName);

            var handle = _gl.CreateShader(type);

            _gl.ShaderSource(handle, shaderSource);
            _gl.CompileShader(handle);

            var infoLog = _gl.GetShaderInfoLog(handle);

            if (!string.IsNullOrWhiteSpace(infoLog))
            {
                _gl.DeleteShader(handle);
                throw new ApplicationException($"Error compiling shader of type {type}, failed with error {infoLog}");
            }

            _shadersTemp.Add(handle);
        }

        /// <summary>
        /// Adds an attribute to our shader. Should be called between compiling and linking.
        /// If linking is already complete for current program it logs a warning and continues
        /// without execution.
        /// </summary>
        /// <param name="attributeName"></param>
        public override void AddAttribute(string attributeName)
        {
            if (_linkingIsComplete)
            {
                Console.WriteLine(Properties.Resources.ShaderCantAddAttribute);
                return;
            }

            _gl.BindAttribLocation(ProgramHandle, _numberOfAttributes++, attributeName);
        }

        /// <summary>
        /// Links the shaders stored in the shader temporary list. After successful
        /// linking, 'CompileShader()' and 'AddAttribute()' methods can no longer be called
        /// for the current program.
        /// </summary>
        /// <exception cref="ApplicationException"></exception>
        public override void LinkShaders()
        {
            if (_linkingIsComplete)
            {
                Console.WriteLine(Properties.Resources.ShaderCantLink);
                return;
            }

            _shadersTemp.ForEach(shaderHandle => _gl.AttachShader(ProgramHandle, shaderHandle));
            _gl.LinkProgram(ProgramHandle);

            var programInfoLog = _gl.GetProgramInfoLog(ProgramHandle);

            if (!string.IsNullOrWhiteSpace(programInfoLog))
            {
                _shadersTemp.ForEach(shader => _gl.DeleteShader(shader));
                CleanUp();

                throw new ApplicationException($"Program failed to link with error: {programInfoLog}");
            }

            _shadersTemp.ForEach(shaderHandle =>
            {
                _gl.DetachShader(ProgramHandle, shaderHandle);
                _gl.DeleteShader(shaderHandle);
            });

            _linkingIsComplete = true;
        }

        /// <summary>
        /// Sets integer value to an uniform location.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <exception cref="ApplicationException"></exception>
        public override void SetUniform(string name, int value)
        {
            var location = _gl.GetUniformLocation(ProgramHandle, name);

            if (location == -1)
            {
                throw new ApplicationException($"{name} uniform not found on shader.");
            }

            Use();

            _gl.Uniform1(location, value);
        }

        /// <summary>
        /// Sets floating point value to an uniform location.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <exception cref="ApplicationException"></exception>
        public override void SetUniform(string name, float value)
        {
            var location = _gl.GetUniformLocation(ProgramHandle, name);

            if (location == -1)
            {
                throw new ApplicationException($"{name} uniform not found on shader.");
            }

            Use();

            _gl.Uniform1(location, value);
        }

        /// <summary>
        /// Use the current program.
        /// </summary>
        public override void Use()
        {
            _gl.UseProgram(ProgramHandle);
        }
    }
}