namespace Reload.Platform.Graphics.OpenGl
{
    using Reload.Core.IO;
    using Reload.Core.Utils;
    using Reload.Rendering;
    using Silk.NET.OpenGL;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Reflection;

    /// <inheritdoc/>
    public class GlShaderProgram : ShaderProgram
    {
        /// <inheritdoc/>
        private const string SHADER_EXT = "glsl";

        /// <inheritdoc/>
        private readonly GL _gl;

        /// <inheritdoc/>
        private readonly List<uint> _shadersTemp;

        /// <inheritdoc/>
        private bool _linkingIsComplete;

        /// <inheritdoc/>
        private uint _numberOfAttributes;

        /// <inheritdoc/>
        public uint ProgramHandle { get; }

        /// <inheritdoc/>
        public GlShaderProgram(GL api)
        {
            _gl = api;
            ProgramHandle = _gl.CreateProgram();
            _shadersTemp = new List<uint>();
            _linkingIsComplete = false;
            _numberOfAttributes = 0;
        }

        /// <inheritdoc/>
        public override void CleanUp()
        {
            _gl.UseProgram(0);
            _gl.DeleteProgram(ProgramHandle);
        }

        /// <inheritdoc/>
        public override void CompileShader(ShaderType type, string shaderName)
        {
            if (_linkingIsComplete)
            {
                Console.WriteLine(Properties.Resources.ShaderCantCompile);
                return;
            }

            shaderFileName = $".{shaderName}.{SHADER_EXT}";

            var shaderResourceName = GetType().Assembly
                .GetManifestResourceNames()
                .First(resourceName => resourceName.EndsWith(shaderFileName, StringComparison.InvariantCulture));

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

        /// <inheritdoc/>
        public override int GetUniform(string name)
        {
            if (uniformLocationCache.TryGetValue(name, out var location))
            {
                return location;
            }

            location = _gl.GetUniformLocation(ProgramHandle, name);

            if (location == -1)
            {
                Logger.PrintWarning($"Uniform {name} not found in shader {shaderFileName}. ");
            }
            else
            {
                uniformLocationCache.Add(name, location);
            }

            return location;
        }

        /// <inheritdoc/>
        public override void SetUniform(string name, int value)
        {
            var location = GetUniform(name);

            Use();

            _gl.Uniform1(location, value);
        }

        /// <inheritdoc/>
        public override void SetUniform(string name, float value)
        {
            var location = GetUniform(name);

            Use();

            _gl.Uniform1(location, value);
        }

        /// <inheritdoc/>
        public unsafe override void SetUniform(string name, Matrix4x4 value)
        {
            var location = GetUniform(name);

            Use();

            _gl.UniformMatrix4(location, 1, false, (float*)&value);
        }


        /// <inheritdoc/>
        public unsafe override void SetUniform(string name, Vector4 value)
        {
            var location = GetUniform(name);

            Use();

            _gl.Uniform4(location, value);
        }

        /// <inheritdoc/>
        public override void Use()
        {
            _gl.UseProgram(ProgramHandle);
        }
    }
}