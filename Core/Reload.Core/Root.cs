using DryIoc;
using Reload.Core.Game;
using System;

namespace Reload.Core
{
    /// <summary>
    /// The composition root class.
    /// </summary>
    public sealed class Root : IDisposable
    {
        private readonly IContainer _components;

        /// <summary>
        /// Prevents a default instance of the <see cref="Root"/> class from being created.
        /// </summary>
        private Root()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Root"/> class.
        /// </summary>
        public Root(IContainer components)
        {
            _components = components;

            _components.RegisterInitializer<ISubSystem>((subSystem, resolver) => subSystem.StartUp());
            _components.RegisterDisposer<ISubSystem>(subSystem => subSystem.ShutDown());
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _components.Dispose();
        }
    }
}
