namespace Core.ResourcesPipeline.Shaders.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Engine.Utilities.IO;
    using Silk.NET.OpenGL;

    public class GlShaderProgram : IShaderProgram
    {
        private const string SHADER_EXT = "glsl";

        private readonly GL _gl;
        private readonly List<uint> _shadersTemp;

        private bool _linkingIsComplete;
        private uint _numberOfAttributes;

        public uint Handle { get; }

        public GlShaderProgram(GL gl)
        {
            _gl = gl;
            Handle = _gl.CreateProgram();
            _shadersTemp = new List<uint>();
            _linkingIsComplete = false;
            _numberOfAttributes = 0;
        }

        public GlShaderProgram(GL gl, uint handle)
        {
            _gl = gl;
            Handle = handle;
            _linkingIsComplete = true;
        }

        public void Dispose()
        {
            _gl.UseProgram(0);
            _gl.DeleteProgram(Handle);
        }

        /// <summary>
        /// Compiles the shaders and stores them in a temporary list ready for linking.
        /// If linking is already complete for current program it logs a warning and continues
        /// without execution.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="shaderName"></param>
        /// <exception cref="Exception"></exception>
        public void CompileShader(ShaderType type, string shaderName)
        {
            if (_linkingIsComplete)
            {
                Console.WriteLine("Can't compile shader. Linking already complete.");
                return;
            }

            var shaderNameWithExt = $".{shaderName}.{SHADER_EXT}";

            var shaderResourceName = GetType().Assembly
                .GetManifestResourceNames()
                .First(resourceName => resourceName.EndsWith(shaderNameWithExt));

            var assembly = Assembly.GetExecutingAssembly();
            var shaderSource = EmbeddedResourceUtility.LoadEmbeddedResourceString(assembly, shaderResourceName);

            var handle = _gl.CreateShader(type);

            _gl.ShaderSource(handle, shaderSource);
            _gl.CompileShader(handle);

            var infoLog = _gl.GetShaderInfoLog(handle);

            if (!string.IsNullOrWhiteSpace(infoLog))
            {
                _gl.DeleteShader(handle);
                throw new Exception($"Error compiling shader of type {type}, failed with error {infoLog}");
            }

            _shadersTemp.Add(handle);
        }

        /// <summary>
        /// Adds an attribute to our shader. Should be called between compiling and linking.
        /// If linking is already complete for current program it logs a warning and continues
        /// without execution.
        /// </summary>
        /// <param name="attributeName"></param>
        public void AddAttribute(string attributeName)
        {
            if (_linkingIsComplete)
            {
                Console.WriteLine("Can't add attribute. Linking already complete.");
                return;
            }

            _gl.BindAttribLocation(Handle, _numberOfAttributes++, attributeName);
        }

        /// <summary>
        /// Links the shaders stored in the shader temporary list. After successful
        /// linking, 'CompileShader()' and 'AddAttribute()' methods can no longer be called
        /// for the current program.
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void LinkShaders()
        {
            if (_linkingIsComplete)
            {
                Console.WriteLine("Can't link shaders. Linking already complete.");
                return;
            }

            _shadersTemp.ForEach(shaderHandle => _gl.AttachShader(Handle, shaderHandle));
            _gl.LinkProgram(Handle);

            var programInfoLog = _gl.GetProgramInfoLog(Handle);

            if (!string.IsNullOrWhiteSpace(programInfoLog))
            {
                _shadersTemp.ForEach(shader => _gl.DeleteShader(shader));
                Dispose();

                throw new Exception($"Program failed to link with error: {programInfoLog}");
            }

            _shadersTemp.ForEach(shaderHandle =>
            {
                _gl.DetachShader(Handle, shaderHandle);
                _gl.DeleteShader(shaderHandle);
            });

            _linkingIsComplete = true;
        }

        /// <summary>
        /// Sets integer value to an uniform location.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <exception cref="Exception"></exception>
        public void SetUniform(string name, int value)
        {
            var location = _gl.GetUniformLocation(Handle, name);

            if (location == -1)
            {
                throw new Exception($"{name} uniform not found on shader.");
            }

            Use();

            _gl.Uniform1(location, value);
        }

        /// <summary>
        /// Sets floating point value to an uniform location.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <exception cref="Exception"></exception>
        public void SetUniform(string name, float value)
        {
            var location = _gl.GetUniformLocation(Handle, name);

            if (location == -1)
            {
                throw new Exception($"{name} uniform not found on shader.");
            }

            Use();

            _gl.Uniform1(location, value);
        }

        /// <summary>
        /// Use the current program.
        /// </summary>
        public void Use()
        {
            _gl.UseProgram(Handle);
        }
    }
}