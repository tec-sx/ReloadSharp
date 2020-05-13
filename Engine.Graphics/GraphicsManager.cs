namespace Engine.Graphics
{
    using Engine.ResourcesPipeline.Shaders.ShaderProgram;
    using Silk.NET.GLFW;
    using Silk.NET.OpenGL;
    using Silk.NET.Windowing.Common;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// The main graphics manager class. Add as singleton in the
    /// service manager.
    /// </summary>
    public sealed class GraphicsManager : IGraphicsManager
    {
        /// <summary>
        /// Main prorogram window.
        /// </summary>
        public IWindow Window { get; private set; }

        /// <summary>
        /// Creates a new Silk.NET window with the provided configuration.
        /// </summary>
        /// <param name="displayConfiguration"></param>
        /// <returns cref="IWindow"></returns>
        /// <exception cref="NotSupportedException"></exception>
        public void CreateWindow(DisplayConfiguration displayConfiguration)
        {
            var options = CreateWindowOptionsFromConfiguration(displayConfiguration);
            Window = Silk.NET.Windowing.Window.Create(options);

            if (Window == null)
            {
                throw new NotSupportedException($"{options.API.API.ToString()} is not supported.");
            }
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

            var shaderProgram = new GlShaderProgram();

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

        /// <summary>
        /// Creates WindowOptions used by Silk.NET Window.Create static method
        ///  from a user defined display configuration.
        /// </summary>
        /// <param name="displayConfiguration"></param>
        /// <returns cref="WindowOptions"></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static WindowOptions CreateWindowOptionsFromConfiguration(DisplayConfiguration displayConfiguration)
        {
            var options = displayConfiguration.EnableVulkan ? WindowOptions.DefaultVulkan : WindowOptions.Default;

            options.Title = displayConfiguration.WindowTitle;
            options.Size = new Size(displayConfiguration.Resolution.X, displayConfiguration.Resolution.Y);
            options.WindowState = displayConfiguration.InFullScreen ? WindowState.Fullscreen : WindowState.Normal;
            options.UpdatesPerSecond = displayConfiguration.TargetFps;
            options.FramesPerSecond = displayConfiguration.TargetFps;
            options.VSync = displayConfiguration.EnableVSync ? VSyncMode.On : VSyncMode.Adaptive;
            options.RunningSlowTolerance = 5;
            options.UseSingleThreadedWindow = false;

            return options;
        }
    }
}