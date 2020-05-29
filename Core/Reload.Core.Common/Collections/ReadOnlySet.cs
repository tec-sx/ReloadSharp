namespace Reload.Core.Collections
{
    using System.Collections.Generic;

    public class ReadOnlySet<T> : IReadOnlySet<T>
    {
        private readonly ISet<T> innerSet;

        public ReadOnlySet(ISet<T> innerSet)
        {
            this.innerSet = innerSet;
        }

        public bool Contains(T item)
        {
            return innerSet.Contains(item);
        }

        public int Count => innerSet.Count;

        public IEnumerator<T> GetEnumerator()
        {
            return innerSet.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return innerSet.GetEnumerator();
        }
    }
}
