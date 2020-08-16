using System;
using Silk.NET.Core.Contexts;
using Silk.NET.Windowing.Common;
using Reload.Configuration;
using Reload.Platform.Graphics.OpenGl.Structures;
using Silk.NET.OpenGL;
using System.Collections.Generic;
using System.IO;
using Reload.Core.Graphics.Rendering.Buffers;
using Reload.Core.Graphics;
using Reload.Rendering.Shaders;
using Reload.Rendering;
using Reload.Resources.Model;

namespace Reload.Platform.Graphics.OpenGl
{
    public class OpenGLBackend : IGraphicsBackend
    {
        public GraphicsBackendType Type { get; init; } = GraphicsBackendType.OpenGL;

        public GL Api { get; init; }

        public GraphicsAPIVersion Version => throw new NotImplementedException();

        public OpenGLBackend()
        { }
        
        public OpenGLBackend(IWindow window)
            : this(window.GLContext)
        { }
        
        public OpenGLBackend(INativeContext context)
        {
            Api = GL.GetApi(context);
        }

        public OpenGLBackend(Func<string, IntPtr> getProcAddress)
        {
            Api = GL.GetApi(getProcAddress);
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

        public void Initialize()
        {
            #region Render commands

            RenderCommand.Initialize += glRenderer.Initialize;

            #endregion


            #region Shaders

            ShaderProgram.Create += CreateShader;

            #endregion

            #region Buffers

            VertexBuffer.Create += (vertices, layout, usage) => new OpenGlVertexBuffer(vertices, layout, Api);
            IndexBuffer.Create += (indices) => new GlIndexBuffer(indices, Api);
            VertexArray.Create += () => new GlVertexArray(Api);

            #endregion

            #region Textures

            Texture2D.CreateBlank += (width, height) => new GlTexture2D(width, height, Api);
            Texture2D.CreateFromFile += (filePath) => new GlTexture2D(filePath, Api);

            #endregion
        }

        public void ShutDown()
        {
            throw new NotImplementedException();
        }
    }
}
