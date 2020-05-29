﻿namespace Reload.Core.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Runtime.InteropServices;

  /// <summary>
    /// Similar to <see cref="List{T}"/>, with direct access to underlying array.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    public class FastList<T> : IList<T>, IReadOnlyList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
    {
        // Fields
        private const int DefaultCapacity = 4;

        /// <summary>
        /// Gets the items.
        /// </summary>
        public T[] Items { get; private set; }

        public FastList()
        {
            Items = new T[0];
        }

        public FastList(IEnumerable<T> collection)
        {
            if (collection is ICollection<T> is2)
            {
                var count = is2.Count;
                Items = new T[count];
                is2.CopyTo(Items, 0);
                Count = count;
            }
            else
            {
                Count = 0;
                Items = new T[DefaultCapacity];
                using (var enumerator = collection.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        Add(enumerator.Current);
                    }
                }
            }
        }

        public FastList(int capacity)
        {
            Items = new T[capacity];
        }

        public int Capacity
        {
            get => Items.Length;
            set
            {
                if (value != Items.Length)
                {
                    if (value > 0)
                    {
                        var destinationArray = new T[value];
                        if (Count > 0)
                        {
                            Array.Copy(Items, 0, destinationArray, 0, Count);
                        }
                        Items = destinationArray;
                    }
                    else
                    {
                        Items = new T[0];
                    }
                }
            }
        }

        #region IList<T> Members

        public void Add(T item)
        {
            if (Count == Items.Length)
            {
                EnsureCapacity(Count + 1);
            }
            Items[Count++] = item;
        }

        public void IncreaseCapacity(int index)
        {
            EnsureCapacity(Count + index);
            Count += index;
        }

        public void Clear()
        {
            Clear(false);
        }

        public bool Contains(T item)
        {
            if (item == null)
            {
                for (var j = 0; j < Count; j++)
                {
                    if (Items[j] == null)
                    {
                        return true;
                    }
                }
                return false;
            }
            var comparer = EqualityComparer<T>.Default;
            for (var i = 0; i < Count; i++)
            {
                if (comparer.Equals(Items[i], item))
                {
                    return true;
                }
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Array.Copy(Items, 0, array, arrayIndex, Count);
        }

        public int IndexOf(T item)
        {
            return Array.IndexOf(Items, item, 0, Count);
        }

        public void Insert(int index, T item)
        {
            if (Count == Items.Length)
            {
                EnsureCapacity(Count + 1);
            }
            if (index < Count)
            {
                Array.Copy(Items, index, Items, index + 1, Count - index);
            }
            Items[index] = item;
            Count++;
        }

        public bool Remove(T item)
        {
            var index = IndexOf(item);
            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count) throw new ArgumentOutOfRangeException(nameof(index));
            Count--;
            if (index < Count)
            {
                Array.Copy(Items, index + 1, Items, index, Count - index);
            }
            Items[Count] = default(T);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        public int Count { get; private set; }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count) throw new ArgumentOutOfRangeException(nameof(index));
                return Items[index];
            }
            set
            {
                if (index < 0 || index >= Count) throw new ArgumentOutOfRangeException(nameof(index));
                Items[index] = value;
            }
        }

        bool ICollection<T>.IsReadOnly => false;

        #endregion

        /// <summary>
        /// Clears this list with a fast-clear option.
        /// </summary>
        /// <param name="fastClear">if set to <c>true</c> this method only resets the count elements but doesn't clear items referenced already stored in the list.</param>
        public void Clear(bool fastClear)
        {
            Resize(0, fastClear);
        }

        public void Resize(int newSize, bool fastClear)
        {
            if (Count < newSize)
            {
                EnsureCapacity(newSize);
            }
            else if (!fastClear && Count - newSize > 0)
            {
                Array.Clear(Items, newSize, Count - newSize);
            }

            Count = newSize;
        }

        public void AddRange(IEnumerable<T> collection)
        {
            InsertRange(Count, collection);
        }

        public ReadOnlyCollection<T> AsReadOnly()
        {
            return new ReadOnlyCollection<T>(this);
        }

        public int BinarySearch(T item)
        {
            return BinarySearch(0, Count, item, null);
        }

        public int BinarySearch(T item, IComparer<T> comparer)
        {
            return BinarySearch(0, Count, item, comparer);
        }

        public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
        {
            return Array.BinarySearch(Items, index, count, item, comparer);
        }

        public void CopyTo(T[] array)
        {
            CopyTo(array, 0);
        }

        public void CopyTo(int index, T[] array, int arrayIndex, int count)
        {
            Array.Copy(Items, index, array, arrayIndex, count);
        }

        public void EnsureCapacity(int min)
        {
            if (Items.Length < min)
            {
                var num = (Items.Length == 0) ? DefaultCapacity : (Items.Length * 2);
                if (num < min)
                {
                    num = min;
                }
                Capacity = num;
            }
        }

        public bool Exists(Predicate<T> match)
        {
            return FindIndex(match) != -1;
        }

        public T Find(Predicate<T> match)
        {
            for (var i = 0; i < Count; i++)
            {
                if (match(Items[i]))
                {
                    return Items[i];
                }
            }
            return default(T);
        }

        public FastList<T> FindAll(Predicate<T> match)
        {
            var list = new FastList<T>();
            for (var i = 0; i < Count; i++)
            {
                if (match(Items[i]))
                {
                    list.Add(Items[i]);
                }
            }
            return list;
        }

        public int FindIndex(Predicate<T> match)
        {
            return FindIndex(0, Count, match);
        }

        public int FindIndex(int startIndex, Predicate<T> match)
        {
            return FindIndex(startIndex, Count - startIndex, match);
        }

        public int FindIndex(int startIndex, int count, Predicate<T> match)
        {
            var num = startIndex + count;
            for (var i = startIndex; i < num; i++)
            {
                if (match(Items[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        public T FindLast(Predicate<T> match)
        {
            for (var i = Count - 1; i >= 0; i--)
            {
                if (match(Items[i]))
                {
                    return Items[i];
                }
            }
            return default(T);
        }

        public int FindLastIndex(Predicate<T> match)
        {
            return FindLastIndex(Count - 1, Count, match);
        }

        public int FindLastIndex(int startIndex, Predicate<T> match)
        {
            return FindLastIndex(startIndex, startIndex + 1, match);
        }

        public int FindLastIndex(int startIndex, int count, Predicate<T> match)
        {
            var num = startIndex - count;
            for (var i = startIndex; i > num; i--)
            {
                if (match(Items[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        public void ForEach(Action<T> action)
        {
            for (var i = 0; i < Count; i++)
            {
                action(Items[i]);
            }
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        public FastList<T> GetRange(int index, int count)
        {
            var list = new FastList<T>(count);
            Array.Copy(Items, index, list.Items, 0, count);
            list.Count = count;
            return list;
        }

        public int IndexOf(T item, int index)
        {
            return Array.IndexOf(Items, item, index, Count - index);
        }

        public int IndexOf(T item, int index, int count)
        {
            return Array.IndexOf(Items, item, index, count);
        }

        public void InsertRange(int index, IEnumerable<T> collection)
        {
            var is2 = collection as ICollection<T>;
            if (is2 != null)
            {
                var count = is2.Count;
                if (count > 0)
                {
                    EnsureCapacity(Count + count);
                    if (index < Count)
                    {
                        Array.Copy(Items, index, Items, index + count, Count - index);
                    }
                    if (this == is2)
                    {
                        Array.Copy(Items, 0, Items, index, index);
                        Array.Copy(Items, index + count, Items, index * 2, Count - index);
                    }
                    else
                    {
                        is2.CopyTo(Items, index);
                    }
                    Count += count;
                }
            }
            else
            {
                using (var enumerator = collection.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        Insert(index++, enumerator.Current);
                    }
                }
            }
        }

        private static bool IsCompatibleObject(object value)
        {
            return value is T || (value == null && default(T) == null);
        }

        public int LastIndexOf(T item)
        {
            if (Count == 0)
            {
                return -1;
            }
            return LastIndexOf(item, Count - 1, Count);
        }

        public int LastIndexOf(T item, int index)
        {
            return LastIndexOf(item, index, index + 1);
        }

        public int LastIndexOf(T item, int index, int count)
        {
            if (Count == 0)
            {
                return -1;
            }
            return Array.LastIndexOf(Items, item, index, count);
        }

        public int RemoveAll(Predicate<T> match)
        {
            var index = 0;
            while ((index < Count) && !match(Items[index]))
            {
                index++;
            }
            if (index >= Count)
            {
                return 0;
            }
            var num2 = index + 1;
            while (num2 < Count)
            {
                while ((num2 < Count) && match(Items[num2]))
                {
                    num2++;
                }
                if (num2 < Count)
                {
                    Items[index++] = Items[num2++];
                }
            }
            Array.Clear(Items, index, Count - index);
            var num3 = Count - index;
            Count = index;
            return num3;
        }

        public void RemoveRange(int index, int count)
        {
            if (count > 0)
            {
                Count -= count;
                if (index < Count)
                {
                    Array.Copy(Items, index + count, Items, index, Count - index);
                }
                Array.Clear(Items, Count, count);
            }
        }

        public void Reverse()
        {
            Reverse(0, Count);
        }

        public void Reverse(int index, int count)
        {
            Array.Reverse(Items, index, count);
        }

        public void Sort()
        {
            Sort(0, Count, null);
        }

        public void Sort(IComparer<T> comparer)
        {
            Sort(0, Count, comparer);
        }

        //public void Sort(Comparison<T> comparison)
        //{
        //    if (this._size > 0)
        //    {
        //        IComparer<T> comparer = new Array.FunctorComparer<T>(comparison);
        //        Array.Sort<T>(this.Items, 0, this._size, comparer);
        //    }
        //}

        public void Sort(int index, int count, IComparer<T> comparer)
        {
            Array.Sort(Items, index, count, comparer);
        }

        public T[] ToArray()
        {
            var destinationArray = new T[Count];
            Array.Copy(Items, 0, destinationArray, 0, Count);
            return destinationArray;
        }

        public void TrimExcess()
        {
            var num = (int)(Items.Length * 0.9);
            if (Count < num)
            {
                Capacity = Count;
            }
        }

        public bool TrueForAll(Predicate<T> match)
        {
            for (var i = 0; i < Count; i++)
            {
                if (!match(Items[i]))
                {
                    return false;
                }
            }
            return true;
        }

        #region Nested type: Enumerator

        [StructLayout(LayoutKind.Sequential)]
        public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
        {
            private readonly FastList<T> list;
            private int index;
            private T current;

            internal Enumerator(FastList<T> list)
            {
                this.list = list;
                index = 0;
                current = default(T);
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                var list = this.list;
                if (index < list.Count)
                {
                    current = list.Items[index];
                    index++;
                    return true;
                }
                return MoveNextRare();
            }

            private bool MoveNextRare()
            {
                index = list.Count + 1;
                current = default(T);
                return false;
            }

            public T Current => current;

            object IEnumerator.Current => Current;

            void IEnumerator.Reset()
            {
                index = 0;
                current = default(T);
            }
        }

        #endregion
    }
}
