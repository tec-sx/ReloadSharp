using Reload.Resources;
using Reload.RenderingShaders;
using Silk.NET.OpenGL;
using System;
using System.Drawing;
using System.Numerics;

namespace Reload.Rendering
{
    public struct SceneData
    {
        public Matrix4x4 ViewProjectionMatrix;
    }

    public static class Renderer
    {
        private static SceneData _sceneData;
        private static RendererData _data;

        //public static void Initialize()
        //{
        //    _data.ShaderLibrary = new ShaderLibrary();
        //    _data.CommandQueue = new RenderCommandQueue();

        //    RenderCommand.Initialize();
        //    Renderer2D.Initialize();
        //}

        //public static void OnWindowResize(Size size)
        //{
        //    RenderCommand.SetViewportSize(size);
        //}

        //public static void BeginScene(OrthographicCamera camera)
        //{
        //    _orthographicCamera = camera;
        //    _sceneData.ViewProjectionMatrix = _orthographicCamera.ViewProjectionMatrix;
        //}

        //public static void BeginScene(PerspectiveCamera camera)
        //{
        //    _perspectiveCamera = camera;
        //    _sceneData.ViewProjectionMatrix = _perspectiveCamera.ViewProjectionMatrix;
        //}

        //public static void EndScene()
        //{

        //}

        //public static void Submit(ShaderProgram shader, VertexArray vertexArray, Matrix4x4 transform)
        //{
        //    shader.Bind();

        //    shader.SetMatrix4("u_ViewProjection", _sceneData.ViewProjectionMatrix);
        //    shader.SetMatrix4("u_Transform", transform);

        //    vertexArray.Bind();

        //    RenderCommand.DrawIndexed(vertexArray);
        //}

        //public static void Submit(ShaderProgram shader, VertexArray vertexArray)
        //{
        //    Submit(shader, vertexArray, Matrix4x4.Identity);
        //}

        private static RenderCommandQueue _renderCommandQueue;

        public static ShaderLibrary ShaderLibrary { get; }

        public static void Clear(Color color)
        {

        }

        public static void DrawIndexed(uint count, PrimitiveType type, bool dephTest = true)
        {

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
