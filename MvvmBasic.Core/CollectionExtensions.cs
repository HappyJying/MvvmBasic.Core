using System.Collections.Generic;

namespace System.Collections.ObjectModel
{
    public static class CollectionExtensions
    {
        public static Collection<T> AddRange<T>(this Collection<T> collection, IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                collection.Add(item);
            }
            return collection;
        }
    }
}
