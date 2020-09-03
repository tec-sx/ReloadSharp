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
using System.Numerics;

namespace Reload.Core.Graphics.Rendering.Structures
{
    public readonly struct Vertex : IEquatable<Vertex>
    {
        /// <summary>
        /// Gets the vertex position.
        /// </summary>
        public Vector3 Position { get; }

        /// <summary>
        /// Gets the vertex normal.
        /// </summary>
        public Vector3 Normal { get; }

        /// <summary>
        /// Gets the vertex tangent.
        /// </summary>
        public Vector3 Tangent { get; }

        /// <summary>
        /// Gets the vertex bi-normal.
        /// </summary>
        public Vector3 BiNormal { get; }

        /// <summary>
        /// Gets the vertex texture coordinate.
        /// </summary>
        public Vector2 TexCoord { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vertex"/> struct.
        /// </summary>
        /// <param name="position">The vertex position.</param>
        /// <param name="normal">The vertex normal.</param>
        /// <param name="tangent">The vertex tangent.</param>
        /// <param name="biNormal">The vertex bi-normal.</param>
        /// <param name="texCoord">The vertex texture coordinate.</param>
        public Vertex(
            Vector3 position, 
            Vector3 normal, 
            Vector3 tangent, 
            Vector3 biNormal, 
            Vector2 texCoord)
        {
            Position = position;
            Normal = normal;
            Tangent = tangent;
            BiNormal = biNormal;
            TexCoord = texCoord;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Position, Normal, Tangent, BiNormal, TexCoord);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is Vertex other && Equals(other);
        }

        /// <inheritdoc/>
        public bool Equals(Vertex other)
        {
            return other.Position == Position
                && other.Normal == Normal
                && other.Tangent == Tangent
                && other.BiNormal == BiNormal
                && other.TexCoord == TexCoord;
        }

        /// <inheritdoc/>
        public static bool operator ==(Vertex left, Vertex right)
        {
            return left.Equals(right);
        }

        /// <inheritdoc/>
        public static bool operator !=(Vertex left, Vertex right)
        {
            return !(left == right);
        }
    }
}
