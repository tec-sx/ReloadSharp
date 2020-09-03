#region copyright
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

using Reload.Core.Game;
using Reload.Core.Graphics.Rendering;
using Reload.Core.Graphics.Rendering.Buffers;
using Reload.Core.Graphics.Rendering.Shaders;
using Reload.Core.Graphics.Rendering.Textures;
using Reload.Core.Windowing;
using System;

namespace Reload.Core.Graphics
{
    /// <summary>
    /// The graphics API base.
    /// </summary>
    public abstract class GraphicsAPI : ICoreSystem, IDisposable
    {
        #region Core factories

        /// <summary>
        /// Gets or sets the buffer service.
        /// </summary>
        internal protected static BufferFactory BufferFactory { get; protected set; }

        /// <summary>
        /// Gets or sets the shaders service.
        /// </summary>
        internal protected static ShaderFactory ShaderFactory { get; protected set; }

        /// <summary>
        /// Gets or sets the textures service.
        /// </summary>
        internal protected static TextureFactory TextureFactory { get; protected set; }

        /// <summary>
        /// Gets or sets the renderer service.
        /// </summary>
        internal protected static RenderFactory RendererFactory { get; protected set; }

        #endregion

        /// <summary>
        /// Gets the graphics backend type.
        /// </summary>
        public GraphicsAPIType Type { get; protected init; }

        /// <summary>
        /// Gets the backend API version.
        /// </summary>
        public GraphicsAPIVersion Version { get; init; }

        /// <summary>
        /// Prevents a default instance of the <see cref="GraphicsAPI"/> class from being created.
        /// </summary>
        private GraphicsAPI()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicsAPI"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="version">The version.</param>
        public GraphicsAPI(GraphicsAPIType type, GraphicsAPIVersion version)
        {
            Type = type;
            Version = version;
        }

        /// <inheritdoc/>
        public abstract void Configure(IProgramWindow window);

        /// <inheritdoc/>
        public abstract void StartUp();

        /// <inheritdoc/>
        public abstract void ShutDown();

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
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
