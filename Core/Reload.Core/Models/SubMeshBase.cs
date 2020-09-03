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
using Reload.Core.Models.Physics.Collision;
using System.Numerics;

namespace Reload.Core.Models
{
    /// <summary>
    /// The sub mesh.
    /// </summary>
    public sealed class SubMeshBase
    {
        public int BaseVertex;

        public int BaseIndex;

        public int MaterialIndex;

        public int IndexCount;

        public Matrix4x4 Transform;

        public AABB BoundingBox;

        /// <summary>
        /// Gets or sets the node name.
        /// </summary>
        public string NodeName { get; set; }

        /// <summary>
        /// Gets or sets the mesh name.
        /// </summary>
        public string MeshName { get; set; }
    }
}
