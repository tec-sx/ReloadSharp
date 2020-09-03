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
namespace Reload.Core.Graphics.Rendering.Textures
{
    using System;

    public enum TextureFormat
    {
        None = 0,
        Rgb,
        Rgba,
        Float16
    }

    public enum TextureWrap
    {
        None = 0,
        Clamp,
        Repeat
    }

    /// <summary>
    /// The texture base class.
    /// </summary>
    public interface ITexture
    {
        /// <summary>
        /// Gets the width of the texture.
        /// </summary>
        uint Width { get; }

        /// <summary>
        /// Gets the height of the texture.
        /// </summary>
        uint Height { get; }

        /// <summary>
        /// Uploads given texture data to the gpu.
        /// </summary>
        /// <param name="data">The data.</param>
        void SetData(Span<byte> data);

        /// <summary>
        /// Binds the texture to a texture target.
        /// Default slot is 0.
        /// </summary>
        /// <param name="slot">The texture slot.</param>
        void Bind(uint slot = 0);
    }
}
