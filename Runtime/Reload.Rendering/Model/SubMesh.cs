﻿using Silk.NET.Vulkan;
using System.Numerics;

namespace Reload.Rendering.Model
{
    /// <summary>
    /// The sub mesh.
    /// </summary>
    public sealed class SubMesh
    {
        public uint BaseVertex;

        public uint BaseIndex;

        public uint MaterialIndex;

        public uint IndexCount;

        public Matrix4x4 Transform;

        public GeometryAABBNV BoundingBox;

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
