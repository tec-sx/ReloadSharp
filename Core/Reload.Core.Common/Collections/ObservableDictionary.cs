namespace Reload.Core.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;

    [Serializable]
    public class ObservableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IDictionary, IObservableCollectionChanged
    {
        private readonly Dictionary<TKey, TValue> innerDictionary;

        private EventHandler<NotifyCollectionChangedEventArgs> itemAdded;
        private EventHandler<NotifyCollectionChangedEventArgs> itemRemoved;

        /// <inheritdoc/>
        public event EventHandler<NotifyCollectionChangedEventArgs> CollectionChanged
        {
            add
            {
                itemAdded = (EventHandler<NotifyCollectionChangedEventArgs>)Delegate.Combine(itemAdded, value);
                itemRemoved = (EventHandler<NotifyCollectionChangedEventArgs>)Delegate.Combine(value, itemRemoved);
            }
            remove
            {
                itemAdded = (EventHandler<NotifyCollectionChangedEventArgs>)Delegate.Remove(itemAdded, value);
                itemRemoved = (EventHandler<NotifyCollectionChangedEventArgs>)Delegate.Remove(itemRemoved, value);
            }
        }

        /// <inheritdoc/>
        public int Count => innerDictionary.Count;

        /// <inheritdoc/>
        public ICollection<TValue> Values => innerDictionary.Values;

        /// <inheritdoc/>
        ICollection IDictionary.Values => ((IDictionary)innerDictionary).Values;



        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionary{TKey, TValue}"/> class.
        /// </summary>
        public ObservableDictionary()
        {
            innerDictionary = new Dictionary<TKey, TValue>();
        }


        #region Add implementations

        /// <inheritdoc/>
        public void Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);

        /// <inheritdoc/>
        void IDictionary.Add(object key, object value) => Add((TKey)key, (TValue)value);

        /// <inheritdoc/>
        public void Add(TKey key, TValue value)
        {
            innerDictionary.Add(key, value);
            itemAdded?.Invoke(
                this,
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, key, value, null, true));
        }
        #endregion

        /// <inheritdoc/>
        public bool ContainsKey(TKey key) => innerDictionary.ContainsKey(key);

        /// <inheritdoc/>
        public ICollection<TKey> Keys => innerDictionary.Keys;


        /// <inheritdoc/>
        void IDictionary.Remove(object key) => Remove((TKey)key);

        /// <inheritdoc/>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            var collectionChanged = itemRemoved;
            if (collectionChanged != null && innerDictionary.Contains(item))
            {
                return innerDictionary.Remove(item.Key);
            }

            return ((IDictionary<TKey, TValue>)innerDictionary).Remove(item);
        }

        /// <inheritdoc/>
        public bool Remove(TKey key)
        {
            var collectionChanged = itemRemoved;

            if (collectionChanged != null && innerDictionary.TryGetValue(key, out var dictValue))
            {
                collectionChanged(
                    this,
                    new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, key, dictValue, null, true));
            }

            return innerDictionary.Remove(key);
        }

        /// <inheritdoc/>
        public bool TryGetValue(TKey key, out TValue value) => innerDictionary.TryGetValue(key, out value);


        /// <inheritdoc/>
        object IDictionary.this[object key]
        {
            get => this[(TKey)key];
            set => this[(TKey)key] = (TValue)value;
        }

        /// <inheritdoc/>
        public TValue this[TKey key]
        {
            get => innerDictionary[key];
            set
            {
                var collectionChangedRemoved = itemRemoved;
                if (collectionChangedRemoved != null)
                {
                    TValue oldValue;
                    var alreadyExisting = innerDictionary.TryGetValue(key, out oldValue);

                    if (alreadyExisting)
                    {
                        collectionChangedRemoved(
                            this,
                            new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, key, oldValue, null, false));
                    }

                    innerDictionary[key] = value;

                    itemAdded(
                        this,
                        new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, key, innerDictionary[key], oldValue, !alreadyExisting));
                }
                else
                {
                    innerDictionary[key] = value;
                }
            }
        }

        /// <inheritdoc/>
        public void Clear()
        {
            var collectionChanged = itemRemoved;
            if (collectionChanged != null)
            {
                foreach (var key in innerDictionary.Keys.ToArray())
                {
                    Remove(key);
                }
            }
            else
            {
                innerDictionary.Clear();
            }
        }

        /// <inheritdoc/>
        public bool Contains(KeyValuePair<TKey, TValue> item) => innerDictionary.Contains(item);

        /// <inheritdoc/>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ((IDictionary<TKey, TValue>)innerDictionary).CopyTo(array, arrayIndex);
        }

        /// <inheritdoc/>
        public bool IsReadOnly => ((IDictionary<TKey, TValue>)innerDictionary).IsReadOnly;

        /// <inheritdoc/>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => innerDictionary.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => innerDictionary.GetEnumerator();

        /// <inheritdoc/>
        bool IDictionary.Contains(object key) => ((IDictionary)innerDictionary).Contains(key);

        /// <inheritdoc/>
        IDictionaryEnumerator IDictionary.GetEnumerator() => ((IDictionary)innerDictionary).GetEnumerator();

        /// <inheritdoc/>
        bool IDictionary.IsFixedSize => ((IDictionary)innerDictionary).IsFixedSize;

        /// <inheritdoc/>
        ICollection IDictionary.Keys => ((IDictionary)innerDictionary).Keys;

        /// <inheritdoc/>
        void ICollection.CopyTo(Array array, int index) => ((IDictionary)innerDictionary).CopyTo(array, index);

        /// <inheritdoc/>
        bool ICollection.IsSynchronized => ((IDictionary)innerDictionary).IsSynchronized;
        /// <inheritdoc/>
        object ICollection.SyncRoot => ((IDictionary)innerDictionary).SyncRoot;
    }
}
