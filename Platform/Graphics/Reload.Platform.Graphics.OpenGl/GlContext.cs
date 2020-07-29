using System;
using Silk.NET.Core.Contexts;
using Silk.NET.Windowing.Common;

namespace Reload.Platform.Graphics.OpenGl
{
    using Reload.Configuration;
    using Reload.Platform.Graphics.OpenGl.Structures;
    using Reload.Rendering;
    using Reload.Rendering.Structures;
    using Silk.NET.OpenGL;
    using System.Collections.Generic;
    using System.IO;

    public class GlContext
    {
        public GL Api { get; }

        public GlContext(IWindow window)
            : this(window.GLContext)
        { }
        
        public GlContext(INativeContext context)
        {
            Api = GL.GetApi(context);
            SetupDelegates(new GlRenderer(Api));
        }

        public GlContext(Func<string, IntPtr> getProcAddress)
        {
            Api = GL.GetApi(getProcAddress);
            SetupDelegates(new GlRenderer(Api));
        }

        private void SetupDelegates(GlRenderer glRenderer)
        {
            
            //----------------------------------------------------------------------------------------------------------
            // Render commands
            //----------------------------------------------------------------------------------------------------------
            RenderCommand.Initialize += glRenderer.Initialize;
            RenderCommand.Clear += glRenderer.Clear;
            RenderCommand.SetClearColor += glRenderer.SetClearColor;
            RenderCommand.SetViewportSize += glRenderer.SetViewport;
            RenderCommand.SetViewportSizeAndLocation += glRenderer.SetViewport;
            RenderCommand.DrawIndexed += glRenderer.DrawIndexed;

            //----------------------------------------------------------------------------------------------------------
            // Shaders
            //----------------------------------------------------------------------------------------------------------
            ShaderProgram.Create += CreateShader;

            //----------------------------------------------------------------------------------------------------------
            // Buffers
            //----------------------------------------------------------------------------------------------------------
            VertexBuffer.Create += (vertices) => new GlVertexBuffer(vertices, Api);
            IndexBuffer.Create += (indices) => new GlIndexBuffer(indices, Api);
            VertexArray.Create += () => new GlVertexArray(Api);

            //----------------------------------------------------------------------------------------------------------
            // Textures
            //----------------------------------------------------------------------------------------------------------
            Texture2D.CreateBlank += (width, height) => new GlTexture2D(width, height, Api);
            Texture2D.CreateFromFile += (filePath) => new GlTexture2D(filePath, Api);
            TextureCube.CreateBlank += (format, width, height) => new GlTextureCube(format, width, height, Api);
            TextureCube.CreateFromFile += (filepath) => new GlTextureCube(filepath, Api);
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
