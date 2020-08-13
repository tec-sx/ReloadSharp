
using System;

namespace Reload.Core.Game
{
    public interface ISubSystem
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; init; }

        /// <summary>
        /// Initializes the graphick backend.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Shuts down the graphick backend.
        /// </summary>
        void ShutDown();
    }
}
