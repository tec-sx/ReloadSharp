using Reload.Configuration;
using Reload.Core.Math3D.Primitives;
using Reload.Core.Math3D.Vertices;
using Reload.Rendering.Structures;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

using Gltf = SharpGLTF.Schema2;

namespace Reload.Rendering.Model
{
    /// <summary>
    /// The mesh.
    /// </summary>
    public sealed class Mesh
    {
        /// <summary>
        /// The default time multiplier.
        /// </summary>
        private const float TimeMultiplier = 1.0f;

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

        private float _animationPlaying;

        /// <summary>
        /// Gets the file path.
        /// </summary>
        public string FilePath { get; }

        /// <summary>
        /// Gets the mesh shader.
        /// </summary>
        public ShaderProgram MeshShader { get; }

        /// <summary>
        /// Gets the base material.
        /// </summary>
        public Material BaseMaterial { get; }

        /// <summary>
        /// Gets the list of sub meshes.
        /// </summary>
        public List<SubMesh> SubMeshes { get; }

        /// <summary>
        /// Gets the list of materials.
        /// </summary>
        public List<MaterialInstance> Materials { get; }

        /// <summary>
        /// Gets the list of textures.
        /// </summary>
        public List<Texture2D> Textures { get; }

        /// <summary>
        /// Gets the list of normal maps.
        /// </summary>
        public List<Texture2D> NormalMaps { get; }

        /// <summary>
        /// Gets the triangle cache.
        /// </summary>
        public List<Triangle> TriangleCache { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mesh"/> class
        /// from a GLTF2 model file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public Mesh(string filename)
        {
            FilePath = Path.Combine(ContentPaths.Models, filename);
        }

        /// <summary>
        /// Updets the mesh. should be called every cycle
        /// of the loop before rendering.
        /// </summary>
        /// <param name="deltaTime">The delta time.</param>
        public void OnUpdate(double deltaTime)
        {

        }

        /// <summary>
        /// Dumps the vertex buffer.
        /// </summary>
        public void DumpVertexBuffer()
        {

        }

        /// <summary>
        /// Applies transform to the bones.
        /// </summary>
        /// <param name="time">The time.</param>
        private void BoneTransform(float time)
        {

        }

        /// <summary>
        /// Reads the node hierarchy of the mesh beginnig from the passed node.
        /// </summary>
        /// <param name="animationTime">The animation time.</param>
        /// <param name="node">The starting node.</param>
        /// <param name="parentTranform">The parent tranform.</param>
        private void ReadNodeHierarchy(float animationTime, Gltf::Node node, Matrix4x4 parentTranform)
        {

        }

        /// <summary>
        /// Traverses the nodes beginning from the passed node.
        /// </summary>
        /// <param name="node">The starting node.</param>
        /// <param name="parentTransform">The parent transform.</param>
        /// <param name="level">The level.</param>
        private void TraverseNodes(Gltf::Node node, Matrix4x4 parentTransform, uint level = 0)
        {

        }

        /// <summary>
        /// Finds the position of an animated node.
        /// </summary>
        /// <param name="animationTime">The animation time.</param>
        /// <param name="animation">The animation.</param>
        /// <param name="node">The node.</param>
        /// <returns>An uint.</returns>
        private uint FindPosition(float animationTime, Gltf::Animation animation, Gltf::Node node)
        {

        }

        /// <summary>
        /// Finds the rotation of an animated node.
        /// </summary>
        /// <param name="animationTime">The animation time.</param>
        /// <param name="animation">The animation.</param>
        /// <param name="node">The node.</param>
        /// <returns>An uint.</returns>
        private uint FindRotation(float animationTime, Gltf::Animation animation, Gltf::Node node)
        {

        }

        /// <summary>
        /// Finds the scaling of an animated node.
        /// </summary>
        /// <param name="animationTime">The animation time.</param>
        /// <param name="animation">The animation.</param>
        /// <param name="node">The node.</param>
        /// <returns>An uint.</returns>
        private uint FindScaling(float animationTime, Gltf::Animation animation, Gltf::Node node)
        {

        }

        /// <summary>
        /// Interpolates the translation of an animated node.
        /// </summary>
        /// <param name="animationTime">The animation time.</param>
        /// <param name="animation">The animation.</param>
        /// <param name="node">The node.</param>
        /// <returns>An uint.</returns>
        private Vector3 InterpolateTranslation(float animationTime, Gltf::Animation animation, Gltf::Node node)
        {

        }

        /// <summary>
        /// Interpolates the rotation of an animated node.
        /// </summary>
        /// <param name="animationTime">The animation time.</param>
        /// <param name="animation">The animation.</param>
        /// <param name="node">The node.</param>
        /// <returns>An uint.</returns>
        private Vector3 InterpolateRotation(float animationTime, Gltf::Animation animation, Gltf::Node node)
        {

        }

        /// <summary>
        /// Interpolates the scale of an animated node.
        /// </summary>
        /// <param name="animationTime">The animation time.</param>
        /// <param name="animation">The animation.</param>
        /// <param name="node">The node.</param>
        /// <returns>An uint.</returns>
        private Vector3 InterpolateScale(float animationTime, Gltf::Animation animation, Gltf::Node node)
        {

        }
    }
}