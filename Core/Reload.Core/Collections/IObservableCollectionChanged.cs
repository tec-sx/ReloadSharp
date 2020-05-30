namespace Reload.Core.Collections
{
    using System;

    public interface IObservableCollectionChanged
    {
        /// <summary>
        /// Occurs when [collection changed].
        /// </summary>
        /// Called as is when adding an item, and in reverse-order when removing an item.
        event EventHandler<NotifyCollectionChangedEventArgs> CollectionChanged;
    }
}