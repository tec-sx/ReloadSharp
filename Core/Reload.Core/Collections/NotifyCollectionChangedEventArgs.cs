#region copyright
/*
-----------------------------------------------------------------------------
Copyright (c) 2020 Ivan Trajchev

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
-----------------------------------------------------------------------------
*/
#endregion
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
            OldItem = oldItem;
            Index = index;
            CollectionChanged = collectionChanged;
        }

        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object key, object item, object oldItem, bool collectionChanged)
        {
            Action = action;
            Item = item;
            OldItem = oldItem;
            Key = key;
            Index = -1;
            CollectionChanged = collectionChanged;
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
