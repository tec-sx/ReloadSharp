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

    public static class Renderer
    {
        private static SceneData _sceneData = new SceneData();

        public static void Initialize()
        {
            RenderCommand.Initialize();
        }

        public static void ShutDown()
        {

        }

        public static void OnWindowResize(Size size)
        {
            RenderCommand.SetViewport(size);
        }

        public static void BeginScene(OrtographicCamera camera)
        {
            _sceneData.ViewProjectionMatrix = camera.ViewProjectionMatrix;
        }

        public static void BeginScene(PerspectiveCamera camera)
        {
            _sceneData.ViewProjectionMatrix = camera.ModelViewProjectionMatrix;
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
    }

}
