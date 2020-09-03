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
using System;

namespace Reload.Core.Graphics.Rendering.Structures
{
    public readonly struct Index : IEquatable<Index>
    {
        /// <summary>
        /// Gets the first point.
        /// </summary>
        public uint V1 { get; }

        /// <summary>
        /// Gets the second point.
        /// </summary>
        public uint V2 { get; }

        /// <summary>
        /// Gets the third point.
        /// </summary>
        public uint V3 { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Index"/> struct.
        /// </summary>
        /// <param name="v1">First point.</param>
        /// <param name="v2">Second point.</param>
        /// <param name="v3">Third point.</param>
        public Index(uint v1, uint v2, uint v3) : this()
        {
            V1 = v1;
            V2 = v2;
            V3 = v3;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(V1, V2, V3);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is Index other && Equals(other);
        }

        /// <inheritdoc/>
        public bool Equals(Index other)
        {
            return other.V1 == V1
                && other.V2 == V2
                && other.V3 == V3;
        }

        /// <inheritdoc/>
        public static bool operator ==(Index left, Index right)
        {
            return left.Equals(right);
        }

        /// <inheritdoc/>
        public static bool operator !=(Index left, Index right)
        {
            return !(left == right);
        }
    }
}
