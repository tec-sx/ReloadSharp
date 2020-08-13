using Reload.Core.Rendering.Buffers;
using Reload.Core.Structures;
using System.Collections.Generic;
using System.Numerics;

namespace Reload.Core.Models
{
    public abstract class MeshBase
    {
        private List<BoneInfo> _bonesInfo;

        private VertexArray _vertexArray;

        private List<Vertex> _staticVertices;

        private List<AnimatedVertex> _animatedVertices;

        private List<Index> _indices;

        private Dictionary<string, uint> _boneMappings;

        private List<Matrix4x4> _boneTransforms;

        private Gltf::Scene _scene;

        private bool _isAnimated;

        private float _animationTime;

        private float _worldTime;

        private float _timeMultiplier;

        private bool _animationPlaying;
    }
}
