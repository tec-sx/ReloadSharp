using Reload.Core.Utils;
using Reload.Rendering;
using Silk.NET.OpenGL;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Reload.Platform.Graphics.OpenGl
{
    /// <summary>
    /// The Open Gl shader program used for working with glsl shader files.
    /// </summary>
    public class GlShaderProgram : ShaderProgram
    {

        private readonly GL _gl;

        private readonly List<uint> _shadersTemp;

        private bool _linkingIsComplete;

        private uint _numberOfAttributes;

        /// <summary>
        /// Gets the shader program handle.
        /// </summary>
        public uint ProgramHandle { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GlShaderProgram"/> class.
        /// </summary>
        /// <param name="name">The shader name.</param>
        /// <param name="api">The OpenGl api.</param>
        public GlShaderProgram(string name, GL api)
            :base()
        {
            _gl = api;
            Name = name;
            ProgramHandle = _gl.CreateProgram();
            _shadersTemp = new List<uint>();
            _linkingIsComplete = false;
            _numberOfAttributes = 0;
        }

        /// <summary>
        /// Cleans up the resources.
        /// </summary>
        public override void Dispose()
        {
            _gl.UseProgram(0);
            _gl.DeleteProgram(ProgramHandle);
        }

        /// <summary>
        /// Pre-processes single file shader source. Add "#type [type of shader]" above 
        /// the "#version [openGL version]" directive. For supported types
        /// of shaders see <see cref="GlUtils.ShaderTypes"/>
        /// </summary>
        /// <param name="source">The shader source.</param>
        /// <returns>
        /// A Dictionary with the shader type as Key, and the shader source as Value.
        /// </returns>
        public override Dictionary<ShaderType, string> PreProcessShader(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                Logger.PrintError(Properties.Resources.EmptyShaderSource);
                throw new ApplicationException(Properties.Resources.EmptyShaderSource);
            }

            var shaders = new Dictionary<ShaderType, string>(Utils.ShaderTypes.Count);
            var rawSplitShaders = source.Split("#type", StringSplitOptions.RemoveEmptyEntries);
            
            foreach (string rawShaderString in rawSplitShaders)
            {
                int newLineIndex = rawShaderString.IndexOf(Environment.NewLine);

                if (newLineIndex <= 0)
                {
                    throw new ApplicationException(Properties.Resources.EmptyShaderSource);
                }

                string line = rawShaderString.Substring(0, newLineIndex);
                
                if (string.IsNullOrWhiteSpace(line))
                {
                    Logger.PrintError(Properties.Resources.EmptyShaderSource);
                    throw new ApplicationException(Properties.Resources.EmptyShaderSource);
                }

                if (Utils.ShaderTypes.TryGetValue(line.Trim(), out var shaderType))
                {
                    shaders.Add(shaderType, rawShaderString.Substring(newLineIndex + 1));
                }
            }

            return shaders;
        }

        /// <summary>
        /// Compiles the shader.
        /// </summary>
        /// <param name="type">The shader type.</param>
        /// <param name="shaderSource">The shader source.</param>
        public override void CompileShader(ShaderType type, string shaderSource)
        {
            if (_linkingIsComplete)
            {
                Logger.PrintWarning(Properties.Resources.ShaderCantCompile);
                return;
            }

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
        /// Adds an attribute.
        /// </summary>
        /// <param name="attributeName">The attribute name.</param>
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
        /// Links the shaders.
        /// </summary>
        public override void LinkShaders()
        {
            if (_linkingIsComplete)
            {
                Console.WriteLine(Properties.Resources.ShaderCantLink);
                return;
            }

            foreach (var shaderHandle in _shadersTemp)
            {
                _gl.AttachShader(ProgramHandle, shaderHandle);
            }

            _gl.LinkProgram(ProgramHandle);

            var programInfoLog = _gl.GetProgramInfoLog(ProgramHandle);

            if (!string.IsNullOrWhiteSpace(programInfoLog))
            {
                _shadersTemp.ForEach(shader => _gl.DeleteShader(shader));
                Dispose();

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
        /// Gets the uniform location for the given uniform name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The uniform location.</returns>
        public override int GetUniform(string name)
        {
            if (UniformLocationCache.TryGetValue(name, out var location))
            {
                return location;
            }

            location = _gl.GetUniformLocation(ProgramHandle, name);

            if (location == -1)
            {
                Logger.PrintWarning($"Uniform {name} not found in shader {Name}. ");
            }
            else
            {
                UniformLocationCache.Add(name, location);
            }

            return location;
        }

        /// <summary>
        /// Sets value to an <see cref="int"/> type uniform.
        /// </summary>
        /// <param name="name">The uniform name.</param>
        /// <param name="value">The uniform value.</param>
        public override void SetUniform(string name, int value)
        {
            var location = GetUniform(name);

            Bind();

            _gl.Uniform1(location, value);
        }

        /// <summary>
        /// Sets value to a <see cref="float"/> type uniform.
        /// </summary>
        /// <param name="name">The uniform name.</param>
        /// <param name="value">The uniform value.</param>
        public override void SetUniform(string name, float value)
        {
            var location = GetUniform(name);

            Bind();

            _gl.Uniform1(location, value);
        }

        /// <summary>
        /// Sets value to a <see cref="Matrix4x4"/> type uniform.
        /// </summary>
        /// <param name="name">The uniform name.</param>
        /// <param name="value">The uniform value.</param>
        public unsafe override void SetUniform(string name, Matrix4x4 value)
        {
            var location = GetUniform(name);

            Bind();

            _gl.UniformMatrix4(location, 1, false, (float*)&value);
        }


        /// <summary>
        /// Sets value to a <see cref="Vector3"/> type uniform.
        /// </summary>
        /// <param name="name">The uniform name.</param>
        /// <param name="value">The uniform value.</param>
        public unsafe override void SetUniform(string name, Vector3 value)
        {
            var location = GetUniform(name);

            Bind();

            _gl.Uniform3(location, value.X, value.Y, value.Z);

        }

        /// <summary>
        /// Sets value to a <see cref="Vector4"/> type uniform.
        /// </summary>
        /// <param name="name">The uniform name.</param>
        /// <param name="value">The uniform value.</param>
        public unsafe override void SetUniform(string name, Vector4 value)
        {
            var location = GetUniform(name);

            Bind();

            _gl.Uniform4(location, value.X, value.Y, value.Z, value.W);
        }

        /// <summary>
        /// Uses the shader program.
        /// </summary>
        public override void Bind()
        {
            _gl.UseProgram(ProgramHandle);
        }
    }
}