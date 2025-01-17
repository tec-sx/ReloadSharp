﻿#region copyright
/*
-----------------------------------------------------------------------------
Copyright (c) 2020 Ivan Trajchev

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
-----------------------------------------------------------------------------
*/
#endregion
using Reload.Core.Common;
using Reload.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Reload.Core.Graphics.Rendering.Shaders
{
    /// <summary>
    /// The shader program abstract class.
    /// </summary>
    public abstract class ShaderProgram : IBindable, IDisposable
    {
        /// <summary>
        /// Gets the uniform location cache.
        /// </summary>
        protected Dictionary<string, int> UniformLocationCache { get; }

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
        /// the source code for the specific shader. For available shader types see
        /// <see cref="ShaderUtilities.ShaderTypes"/>
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
        /// Binds the user defined vertex attribute variable to the next index.
        /// The attribute index is auto incremented with each binding internaly.
        /// </summary>
        /// <param name="attributeName">The name of the vertex shader attribute variabl to be bounde</param>
        public abstract void AddAttribute(string attributeName);

        /// <summary>
        /// Links the shaders stored in the shader temporary list. After successful
        /// linking, 'CompileShader()' and 'AddAttribute()' methods can no longer be called
        /// for the current program.
        /// </summary>
        public abstract void LinkShaders();

        /// <summary>
        /// Gets uniform location.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public abstract int GetUniform(string name);

        /// <summary>
        /// Sets <see cref="int"/> value to an gpu location.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public abstract void SetInt(string name, int value);

        /// <summary>
        /// Sets <see cref="float"/> value to a gpu location.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public abstract void SetFloat(string name, float value);

        /// <summary>
        /// Sets <see cref="Matrix4x4"/> value to a gpu location.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public abstract void SetMatrix4(string name, Matrix4x4 value);

        /// <summary>
        /// Sets <see cref="Vector4"/> value to a gpu location.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public abstract void SetVector4(string name, Vector4 value);

        /// <summary>
        /// Sets <see cref="Vector3"/> value to an gpu location.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public abstract void SetVector3(string name, Vector3 value);

        /// <summary>
        /// Binds the current program for using.
        /// </summary>
        public abstract void Bind();

        /// <summary>
        /// Unbinds the current program.
        /// </summary>
        public abstract void Unbind();

        /// <summary>
        /// Creates (Compiles, adds attributes and then links) a new shader program from
        /// the shader file and attribute list.
        /// </summary>
        /// <exception cref="ApplicationException"></exception>
        public static ShaderProgram Create(string fileName, List<string> attributes)
        {
            return GraphicsAPI.ShaderFactory?.CreateShaderProgram(fileName, attributes)
                ?? throw new ReloadFactoryNotImplementedException(typeof(ShaderFactory).ToString());
        }

        /// <summary>
        /// Protected dispose method overload with disposing parameter that indicates 
        /// whether the method call comes from a Dispose method (value is true) or
        /// from a finalizer (value is false)
        /// </summary>
        /// <param name="disposing"></param>
        protected abstract void Dispose(bool disposing);

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
