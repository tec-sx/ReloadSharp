using Reload.Core.Graphics.Rendering.Shaders;
using Reload.Core.Utils;
using Silk.NET.OpenGL;
using System;
using System.Collections.Generic;
using System.Numerics;

using ReloadShaderType = Reload.Core.Graphics.Rendering.Shaders.ShaderType;
using OpenGlShaderType = Silk.NET.OpenGL.ShaderType;

namespace Reload.Platform.Graphics.OpenGl.Shaders
{
    /// <summary>
    /// The Open Gl shader program used for working with glsl shader files.
    /// </summary>
    public class OpenGlShaderProgram : ShaderProgram
    {

        private readonly GL _gl;

        private readonly List<uint> _shadersTemp;

        private bool _linkingIsComplete;

        private uint _numberOfAttributes;

        private bool _isDisposed;

        /// <summary>
        /// Gets the shader program handle.
        /// </summary>
        public uint ProgramHandle { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGlShaderProgram"/> class.
        /// </summary>
        /// <param name="name">The shader name.</param>
        /// <param name="api">The OpenGl api.</param>
        public OpenGlShaderProgram(string name, GL api)
            :base()
        {
            _gl = api;
            Name = name;
            ProgramHandle = _gl.CreateProgram();
            _shadersTemp = new List<uint>();
            _linkingIsComplete = false;
            _numberOfAttributes = 0;
        }

        /// <inheritdoc/>
        public override Dictionary<ReloadShaderType, string> PreProcessShader(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                Logger.PrintError(Properties.Resources.EmptyShaderSource);
                throw new ApplicationException(Properties.Resources.EmptyShaderSource);
            }

            var shaders = new Dictionary<ReloadShaderType, string>(ShaderUtils.ShaderTypes.Count);
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

                if (ShaderUtils.ShaderTypes.TryGetValue(line.Trim(), out var shaderType))
                {
                    shaders.Add(shaderType, rawShaderString.Substring(newLineIndex + 1));
                }
            }

            return shaders;
        }

        /// <inheritdoc/>
        public override void CompileShader(ReloadShaderType type, string shaderSource)
        {
            if (_linkingIsComplete)
            {
                Logger.PrintWarning(Properties.Resources.ShaderCantCompile);

                return;
            }

            OpenGlShaderType openGlShaderType = OpenGlShaderUtilities.ShaderTypeToOpenGl(type);
            uint handle = _gl.CreateShader(openGlShaderType);

            _gl.ShaderSource(handle, shaderSource);
            _gl.CompileShader(handle);

            string infoLog = _gl.GetShaderInfoLog(handle);

            if (!string.IsNullOrWhiteSpace(infoLog))
            {
                _gl.DeleteShader(handle);
                throw new ApplicationException($"Error compiling shader of type {type}, failed with error {infoLog}");
            }

            _shadersTemp.Add(handle);
        }

        /// <inheritdoc/>
        public override void AddAttribute(string attributeName)
        {
            if (_linkingIsComplete)
            {
                Console.WriteLine(Properties.Resources.ShaderCantAddAttribute);
                return;
            }

            _gl.BindAttribLocation(ProgramHandle, _numberOfAttributes++, attributeName);
        }

        /// <inheritdoc/>
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

            string programInfoLog = _gl.GetProgramInfoLog(ProgramHandle);

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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public override void SetInt(string name, int value)
        {
            var location = GetUniform(name);

            Bind();

            _gl.Uniform1(location, value);
        }

        /// <inheritdoc/>
        public override void SetFloat(string name, float value)
        {
            var location = GetUniform(name);

            Bind();

            _gl.Uniform1(location, value);
        }

        /// <inheritdoc/>
        public unsafe override void SetMatrix4(string name, Matrix4x4 value)
        {
            var location = GetUniform(name);

            Bind();

            _gl.UniformMatrix4(location, 1, false, (float*)&value);
        }


        /// <inheritdoc/>
        public unsafe override void SetVector3(string name, Vector3 value)
        {
            var location = GetUniform(name);

            Bind();

            _gl.Uniform3(location, value.X, value.Y, value.Z);

        }

        /// <inheritdoc/>
        public unsafe override void SetVector4(string name, Vector4 value)
        {
            var location = GetUniform(name);

            Bind();

            _gl.Uniform4(location, value.X, value.Y, value.Z, value.W);
        }

        /// <inheritdoc/>
        public override void Bind()
        {
            _gl.UseProgram(ProgramHandle);
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            { }

            _gl.UseProgram(0);
            _gl.DeleteProgram(ProgramHandle);

            _isDisposed = true;
        }
    }
}