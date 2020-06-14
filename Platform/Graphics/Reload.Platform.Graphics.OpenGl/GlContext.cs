namespace Reload.Platform.Graphics.OpenGl
{
    using Reload.Platform.Graphics.OpenGl.Structures;
    using Reload.Rendering;
    using Reload.Rendering.Structures;
    using Silk.NET.OpenGL;
    using Silk.NET.Windowing.Common;
    using System;
    using System.Collections.Generic;

    public class GlContext
    {
        internal GL Api;

        public GlContext(IWindow window)
        {
            Api = GL.GetApi(window);

            var glRenderer = new GlRenderer(Api);

            RenderCommand.Clear += glRenderer.Clear;
            RenderCommand.SetClearColor += glRenderer.SetClearColor;
            RenderCommand.SetViewport += glRenderer.SetViewport;
            RenderCommand.DrawIndexed += glRenderer.DrawIndexed;

            ShaderProgram.Create += CreateShader;

            VertexBuffer.Create += (vertices) => new GlVertexBuffer(vertices, Api);
            IndexBuffer.Create += (indices) => new GlIndexBuffer(indices, Api);
            VertexArray.Create += () => new GlVertexArray(Api);
        }


        private ShaderProgram CreateShader(Dictionary<ShaderType, string> shaderFiles, List<string> attributes)
        {
            if (shaderFiles == null || shaderFiles.Count == 0)
            {
                throw new ApplicationException(Properties.Resources.ShaderDictionaryNullOrEmpty);
            }

            ShaderProgram shaderProgram = new GlShaderProgram(Api);

            foreach (var (shaderType, shaderFile) in shaderFiles)
            {
                shaderProgram.CompileShader(shaderType, shaderFile);
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
