using System;

namespace Reload.Core.DA.Entities
{
    /// <summary>
    /// The base database entity. All data that has to be written
    /// in database has to inherit from this class.
    /// </summary>
    public abstract class EntityBase
    {
        /// <summary>
        /// Gets or sets the entity id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the entity unique identifier.
        /// </summary>
        public Guid Uid { get; set; }

        /// <summary>
        /// Gets or sets the date the entity was saved in database.
        /// </summary>
        public DateTime CreatedOn { get; set; }

    }
}
