namespace Reload.Graphics
{
    using Shaders.ShaderProgram;
    using Silk.NET.OpenGL;
    using Silk.NET.Windowing.Common;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using Silk.NET.Vulkan;
    using System.Runtime.InteropServices;
    using Reload.Graphics.Properties;
    using Silk.NET.GLFW;

    /// <summary>
    /// The main graphics manager class. Add as singleton in the
    /// service manager.
    /// </summary>
    public sealed class GraphicsManager : IDisposable
    {
        public GL Gl { get; private set; }
        public Vk VkApi { get; private set; }

        public Glfw Glfw { get; private set; }

        /// <summary>
        /// Creates a new Silk.NET window with the provided configuration.
        /// </summary>
        /// <param name="displayConfiguration"></param>
        /// <returns cref="IWindow"></returns>
        /// <exception cref="NotSupportedException"></exception>
        public unsafe GameWindow CreateWindow(DisplayConfiguration displayConfiguration)
        {
            //Glfw.SetWindowAspectRatio(window.Handle, 16, 9);
            Glfw = Glfw.GetApi();
            return new GameWindow(displayConfiguration);
        }

        /// <summary>
        /// Creates (Compiles and links) a new shader program from
        /// the shader files dictionary.
        /// </summary>
        /// <param name="shaderFiles"></param>
        /// <returns cref="IShaderProgram"></returns>
        /// <exception cref="ApplicationException"></exception>
        public IShaderProgram CreateShader(Dictionary<ShaderType, string> shaderFiles)
        {
            return CreateShader(shaderFiles, null);
        }

        /// <summary>
        /// Creates (Compiles adds attributes and then links) a new shader program from
        /// the shader files dictionary and attribute list.
        /// </summary>
        /// <param name="shaderFiles"></param>
        /// <param name="attributes"></param>
        /// <returns cref="IShaderProgram"></returns>
        /// <exception cref="ApplicationException"></exception>
        public IShaderProgram CreateShader(Dictionary<ShaderType, string> shaderFiles, List<string> attributes)
        {
            if (shaderFiles == null || shaderFiles.Count == 0)
            {
                throw new ApplicationException(Properties.Resources.ShaderDictionaryNullOrEmpty);
            }

            var shaderProgram = new GlShaderProgram(Gl);

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

        public unsafe void SetupOpenGl()
        {
#if DEBUG
            Gl.Enable(GLEnum.DebugOutput);
            Gl.Enable(GLEnum.DebugOutputSynchronous);
            Gl.DebugMessageCallback(OnDebug, null);
#endif
        }

        private static void OnDebug(
            GLEnum source,
            GLEnum type,
            int id,
            GLEnum severity,
            int length,
            IntPtr message,
            IntPtr userparam)
        {
            Console.WriteLine(
                Resources.GraphicsManager_OnDebug,
                severity.ToString().Substring(13),
                type.ToString().Substring(9),
                id,
                Marshal.PtrToStringAnsi(message));
        }

        public void Dispose()
        {
            Gl?.Dispose();
            VkApi?.Dispose();
            Glfw?.Dispose();
        }
    }
}