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
using Reload.Core.Utilities;
using Reload.Core.Properties;
using System;
using System.Globalization;
using System.Numerics;
using System.Collections.Generic;

namespace Reload.Core.Graphics.Rendering.Structures
{
    public readonly struct AnimatedVertex : IEquatable<AnimatedVertex>
    {
        /// <summary>
        /// The <see cref="_boneIDs"/> and <see cref="_boneWeighs"/> array size.
        /// </summary>
        private const int BoneDataSize = 4;

        private readonly uint[] _boneIDs;
        
        private readonly float[] _boneWeights;

        /// <summary>
        /// Gets the vartex position.
        /// </summary>
        public Vector3 Position { get; }

        /// <summary>
        /// Gets the vartex normal.
        /// </summary>
        public Vector3 Normal { get; }

        /// <summary>
        /// Gets the vartex tangent.
        /// </summary>
        public Vector3 Tangent { get; }

        /// <summary>
        /// Gets the vartex bi-normal.
        /// </summary>
        public Vector3 BiNormal { get; }

        /// <summary>
        /// Gets the vartex texture coordinate.
        /// </summary>
        public Vector2 TexCoord { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimatedVertex"/> struct.
        /// </summary>
        /// <param name="position">The vertex position.</param>
        /// <param name="normal">The vertex normal.</param>
        /// <param name="tangent">The vertex tangent.</param>
        /// <param name="biNormal">The vertex bi-normal.</param>
        /// <param name="texCoord">The vertex texture coordinate.</param>
        public AnimatedVertex(
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

            _boneIDs = new uint[BoneDataSize] { 0, 0, 0, 0 }; ;
            _boneWeights = new float[BoneDataSize] { 0.0f, 0.0f, 0.0f, 0.0f };
        }

        /// <summary>
        /// Adds bone data for the vertex.
        /// </summary>
        /// <param name="boneID">The bone i d.</param>
        /// <param name="boneWeight">The bone weight.</param>
        public void AddBoneData(uint boneID, float boneWeight)
        {
            for (int i = 0; i < BoneDataSize; i++)
            {
                if (_boneWeights[i] == 0.0f)
                {
                    _boneIDs[i] = boneID;
                    _boneWeights[i] = boneWeight;

                    return;
                }

                string message = string.Format(CultureInfo.InvariantCulture, Resources.VertexHasMoreThanFourBones, boneID, boneWeight);
                Logger.Log().Warning(message);
            }
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Position, Normal, Tangent, BiNormal, TexCoord);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is AnimatedVertex other && Equals(other);
        }

        /// <inheritdoc/>
        public bool Equals(AnimatedVertex other)
        {
            return other.Position == Position
                && other.Normal == Normal
                && other.Tangent == Tangent
                && other.BiNormal == BiNormal
                && other.TexCoord == TexCoord
                && EqualityComparer<uint[]>.Default.Equals(_boneIDs, _boneIDs)
                && EqualityComparer<float[]>.Default.Equals(_boneWeights, _boneWeights); ;
        }

        /// <inheritdoc/>
        public static bool operator ==(AnimatedVertex left, AnimatedVertex right)
        {
            return left.Equals(right);
        }

        /// <inheritdoc/>
        public static bool operator !=(AnimatedVertex left, AnimatedVertex right)
        {
            return !(left == right);
        }
    }
}
