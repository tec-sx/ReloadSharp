namespace Reload.Core.Collections
{
    using System;
    using System.Collections.Specialized;

    public class NotifyCollectionChangedEventArgs : EventArgs
    {
        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object item, object oldItem, int index, bool collectionChanged)
        {
            Action = action;
            Item = item;
            this.OldItem = oldItem;
            Index = index;
            this.CollectionChanged = collectionChanged;
        }

        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object key, object item, object oldItem, bool collectionChanged)
        {
            Action = action;
            Item = item;
            this.OldItem = oldItem;
            Key = key;
            Index = -1;
            this.CollectionChanged = collectionChanged;
        }

        /// <summary>
        /// Gets the type of action performed.
        /// Allowed values are <see cref="NotifyCollectionChangedAction.Add"/> and <see cref="NotifyCollectionChangedAction.Remove"/>.
        /// </summary>
        public NotifyCollectionChangedAction Action { get; }

        /// <summary>
        /// Gets the added or removed item (if dictionary, value only).
        /// </summary>
        public object Item { get; }

        /// <summary>
        /// Gets the previous value. Only valid if <see cref="Action"/> is <see cref="NotifyCollectionChangedAction.Add"/> and <see cref=""/>
        /// </summary>
        public object OldItem { get; }

        /// <summary>Gets the added or removed key (if dictionary).</summary>
        public object Key { get; }

        /// <summary>
        /// Gets the index in the collection (if applicable).
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// Gets a value indicating whether [collection changed (not a replacement but real insertion/removal)].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [collection changed]; otherwise, <c>false</c>.
        /// </value>
        public bool CollectionChanged { get; }
    }
}
