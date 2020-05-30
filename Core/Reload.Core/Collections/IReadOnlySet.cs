namespace Reload.Core.Collections
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a strongly-typed, read-only set of element.
    /// </summary>
    /// <typeparam name="T">The type of the elements.</typeparam>
    public interface IReadOnlySet<T> : IReadOnlyCollection<T>
    {
        /// <summary>
        /// Determines whether the set [contains] [the specified item].
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        ///   <c>true</c> if the set [contains] [the specified item]; otherwise, <c>false</c>.
        /// </returns>
        bool Contains(T item);
    }
}
