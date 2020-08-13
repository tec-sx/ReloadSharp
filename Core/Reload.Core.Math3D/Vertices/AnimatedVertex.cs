using Reload.Core.Utils;
using Reload.Core.Math3D.Properties;
using System;
using System.Globalization;
using System.Numerics;
using System.Collections.Generic;

namespace Reload.Core.Math3D.Vertices
{
    public readonly struct AnimatedVertex : IEquatable<AnimatedVertex>
    {
        /// <summary>
        /// The <see cref="_boneIDs"/> and <see cref="_boneWeighs"/> array size.
        /// </summary>
        private const int BoneDataSize = 4;

        private readonly uint[] _boneIDs;
        
        private readonly float[] _boneWeights;

        public readonly Vector3 Position;

        public readonly Vector3 Normal;

        public readonly Vector3 Tangent;

        public readonly Vector3 BiNormal;

        public readonly Vector2 TexCoord;

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
                Logger.PrintWarning(message);
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
