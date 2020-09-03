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
using Reload.Core.Properties;
using Reload.Core.Utilities;
using System;

namespace Reload.Core.Graphics.Rendering.Textures.NullTextures
{
    /// <summary>
    /// The null texture cube.
    /// </summary>
    public class NullTextureCube : TextureCube
    {
        private readonly string _type = "Texture Cube";

        /// <inheritdoc/>
        public override void Bind(uint slot = 0)
        {
#if DEBUG
            Logger.Log().Warning(Resources.AccessingNullInstanceMessage, _type);
#endif
        }

        /// <inheritdoc/>
        public override void Unbind()
        {
#if DEBUG
            Logger.Log().Warning(Resources.AccessingNullInstanceMessage, _type);
#endif
        }

        /// <inheritdoc/>
        public override string GetPath()
        {
#if DEBUG
            Logger.Log().Warning(Resources.AccessingNullInstanceMessage, _type);
#endif
            return string.Empty;
        }

        /// <inheritdoc/>
        public override void SetData(Span<byte> data)
        {
#if DEBUG
            Logger.Log().Warning(Resources.AccessingNullInstanceMessage, _type);
#endif
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
#if DEBUG
            Logger.Log().Warning(Resources.AccessingNullInstanceMessage, _type);
#endif
        }
    }
}
