using Reload.Core.Audio;
using Reload.Core.Graphics;
using Reload.Core.Input;
using Reload.Core.Windowing;
using System;

namespace Reload.Core.Game
{
    /// <summary>
    /// Represents a core system module. Predefined classes that implement this interface are
    /// <see cref="IProgramWindow"/>, <see cref="GraphicsAPI"/>, <see cref="IInputSystem"/>, <see cref="AudioAPI"/>
    /// </summary>
    /// <remarks></remarks>
    public interface ICoreSystem : IDisposable
    {
        /// <summary>
        /// Configures the core system.
        /// </summary>
        void Configure();

        /// <summary>
        /// Initializes the core system.
        /// </summary>
        void StartUp();

        /// <summary>
        /// Shuts down the core system.
        /// </summary>
        void ShutDown();
    }
}
