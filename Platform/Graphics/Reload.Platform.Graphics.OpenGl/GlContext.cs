namespace Reload.Platform.Graphics.OpenGl
{
    using Reload.Configuration;
    using Reload.Platform.Graphics.OpenGl.Structures;
    using Reload.Rendering;
    using Reload.Rendering.Structures;
    using Silk.NET.OpenGL;
    using Silk.NET.Windowing.Common;
    using System.Collections.Generic;
    using System.IO;

    public class GlContext
    {
        public GL Api { get; }

        public GlContext(IWindow window)
        {
            Api = GL.GetApi(window);

            var glRenderer = new GlRenderer(Api);

            RenderCommand.Initialize += glRenderer.Initialize;
            RenderCommand.Clear += glRenderer.Clear;
            RenderCommand.SetClearColor += glRenderer.SetClearColor;
            RenderCommand.SetViewport += glRenderer.SetViewport;
            RenderCommand.DrawIndexed += glRenderer.DrawIndexed;

            ShaderProgram.Create += CreateShader;

            VertexBuffer.Create += (vertices) => new GlVertexBuffer(vertices, Api);
            IndexBuffer.Create += (indices) => new GlIndexBuffer(indices, Api);
            VertexArray.Create += () => new GlVertexArray(Api);

            Texture2D.CreateBlank += (width, height) => new GlTexture2D(width, height, Api);
            Texture2D.CreateFromFile += (filePath) => new GlTexture2D(filePath, Api);
        }


        public ShaderProgram CreateShader(string fileName, List<string> attributes)
        {
            string shaderFile = Path.Combine(ContentPaths.Shaders, $"{fileName}.glsl");
            string shaderString = File.ReadAllText(shaderFile);

            ShaderProgram shaderProgram = new GlShaderProgram(fileName, Api);

            var shaderSources = shaderProgram.PreProcessShader(shaderString);

            foreach (var (shaderType, shaderSource) in shaderSources)
            {
                shaderProgram.CompileShader(shaderType, shaderSource);
            }

            if (attributes != null && attributes.Count > 0)
            {
                attributes.ForEach(attribute => shaderProgram.AddAttribute(attribute));
            }

            shaderProgram.LinkShaders();

            return shaderProgram;
        }
    }
}
