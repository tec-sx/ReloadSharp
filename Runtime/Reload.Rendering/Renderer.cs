using System;
using System.Drawing;
using System.Numerics;
using Reload.Core.Models.Physics.Collision;
using Reload.Core.Models;
using Reload.Core.Graphics.Rendering.Primitives;
using Reload.Core.Game;
using Reload.Rendering.Shaders;
using Reload.Core.Graphics.Rendering.Buffers;
using Reload.Core.Graphics.Rendering;
using Reload.Rendering.Model;

namespace Reload.Rendering
{
    public struct SceneData
    {
        public Matrix4x4 ViewProjectionMatrix;
    }

    /// <summary>
    /// The main 3D renderer.
    /// </summary>
    public class Renderer : ISubSystem
    {
        /// <summary>
        /// Gets the render command queue.
        /// </summary>
        public RenderCommandQueue CommandQueue { get; init; }

        /// <summary>
        /// Gets the shader library.
        /// </summary>
        public static ShaderLibrary ShaderLibrary { get; set; }

        /// <summary>
        /// Gets the active render pass.
        /// </summary>
        public RenderPass ActiveRenderPass { get; private set; }

        /// <summary>
        /// Gets the full screen quad vertex array.
        /// </summary>
        public VertexArray FullScreenQuadVertexArray { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Renderer"/> class.
        /// </summary>
        public Renderer()
        {
            CommandQueue = new RenderCommandQueue();
            ShaderLibrary = new ShaderLibrary();
        }

        /// <inheritdoc/>
        public void Initialize()
        {
            
        }

        /// <inheritdoc/>
        public void ShutDown()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Begins the render pass.
        /// </summary>
        /// <param name="renderPass">The render pass.</param>
        /// <param name="clear">If true, clear.</param>
        public void BeginRenderPass(RenderPass renderPass, bool clear = true)
        {
            ActiveRenderPass = renderPass;

        }

        /// <summary>
        /// Submits the action passed.
        /// </summary>
        /// <param name="action">The action.</param>
        public void Submit(Action action)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Waits the and render.
        /// </summary>
        public void WaitAndRender()
        {
            CommandQueue.Execute();
        }

        public void EndRenderPass()
        {
            throw new NotImplementedException();
        }

        public void Clear(Color color)
        {
            throw new NotImplementedException();
        }

        public void DrawAABB(in AABB aabb, Matrix4x4 transform, Color color = default)
        {
            throw new NotImplementedException();
        }

        public void DrawAABB(MeshBase mesh, Matrix4x4 transform, Color color = default)
        {
            throw new NotImplementedException();
        }

        public void DrawIndexed(uint count, PrimitiveType type, bool dephTest = true)
        {
            throw new NotImplementedException();
        }

        public void SubmitFullScreenQuad(MaterialInstance material)
        {
            throw new NotImplementedException();
        }

        public void SubmitMesh(MeshBase mesh, Matrix4x4 transform, MaterialInstance material = null)
        {
            throw new NotImplementedException();
        }

        public void SubmitQuad(MaterialInstance material, Matrix4x4 transform)
        {
            throw new NotImplementedException();
        }
    }

}
