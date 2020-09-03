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

namespace Reload.Core.Models.Physics.Collision
{
    public readonly struct AABB : IEquatable<AABB>
    {
        public readonly Vector3 Min;

        public readonly Vector3 Max;

        /// <summary>
        /// Initializes a new instance of the <see cref="AABB"/> class
        /// (Axis-aligned bounding box) used for collision detection.
        /// </summary>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        public AABB(Vector3 min, Vector3 max)
        {
            Min = min;
            Max = max;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return Equals((AABB)obj);
        }

        /// <inheritdoc/>
        public bool Equals(AABB other)
        {
            return other != null 
                && Min.Equals(Min)
                && Max.Equals(Max);
        }

        /// <inheritdoc/>
        public static bool operator ==(AABB left, AABB right)
        {
            return left.Equals(right);
        }

        /// <inheritdoc/>
        public static bool operator !=(AABB left, AABB right)
        {
            return !(left == right);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Min, Max);
        }
    }
}
