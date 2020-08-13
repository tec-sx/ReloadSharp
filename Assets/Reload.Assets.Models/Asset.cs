using System;
using System.Collections.Generic;

namespace Reload.Assets.Models
{
    /// <summary>
    /// The asset base.
    /// </summary>
    public abstract class Asset
    {
        /// <summary>
        /// Gets the unique id.
        /// </summary>
        protected Guid Id { get; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Asset"/> class
        /// with new Id and empty tags list. Used when creating new
        /// asset from file.
        /// </summary>
        protected Asset()
        {
            Id = new Guid();
        }
    }
}
