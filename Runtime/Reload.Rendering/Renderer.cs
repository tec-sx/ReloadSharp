using System;

namespace Reload.Rendering
{
    using Reload.Rendering.Camera;
    using Reload.Rendering.Structures;
    using System.Drawing;
    using System.Numerics;

    public struct SceneData
    {
        public Matrix4x4 ViewProjectionMatrix;
    }

    public struct RendererData
    {
        public RenderPass ActiveRenderPass;
        public RenderCommandQueue CommandQueue;
        public ShaderLibrary ShaderLibrary;
        public VertexArray FullScreenQuadVertexArray;
    }

    public static class Renderer
    {
        private static SceneData _sceneData;
        private static RendererData _data;

        private static PerspectiveCamera _perspectiveCamera;
        private static OrthographicCamera _orthographicCamera;

        public static void Initialize()
        {
            _data.ShaderLibrary = new ShaderLibrary();
            _data.CommandQueue = new RenderCommandQueue();
            
            RenderCommand.Initialize();
        }

        public static void ShutDown()
        { }

        public static void OnWindowResize(Size size)
        {
            RenderCommand.SetViewportSize(size);
        }

        public static void BeginScene(OrthographicCamera camera)
        {
            _orthographicCamera = camera;
            _sceneData.ViewProjectionMatrix = _orthographicCamera.ViewProjectionMatrix;
        }

        public static void BeginScene(PerspectiveCamera camera)
        {
            _perspectiveCamera = camera;
            _sceneData.ViewProjectionMatrix = _perspectiveCamera.ViewProjectionMatrix;
        }
        
        public static void EndScene()
        {
        
        }
        
        public static void Submit(ShaderProgram shader, VertexArray vertexArray, Matrix4x4 transform)
        {
            shader.Use();
            
            shader.SetUniform("u_viewProjection", _sceneData.ViewProjectionMatrix);
            shader.SetUniform("u_Transform", transform);
        
            vertexArray.Bind();
        
            RenderCommand.DrawIndexed(vertexArray);
        }
        
        public static void Submit(ShaderProgram shader, VertexArray vertexArray)
        {
            Submit(shader, vertexArray, Matrix4x4.Identity);
        }
        
        public static void Submit(Action action) => _data.CommandQueue.Enqueue(action);

        public static void WaitAndRender()
        {
            
        }

        public static void BeginRenderPass(RenderPass renderPass, bool clear = true)
        {
            
        }

        public static void EndRenderPass()
        {
            
        }

        public static void SubmitQuad(MaterialInstance material, Matrix4x4 transform)
        {
            
        }

        public static void SubmitFullScreenQuad(MaterialInstance material)
        {
            
        }

        public static void SubmitMesh(Mesh mesh, Matrix4x4 transform, MaterialInstance material = null)
        {
            
        }
        
    }

}
