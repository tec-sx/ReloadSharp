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
