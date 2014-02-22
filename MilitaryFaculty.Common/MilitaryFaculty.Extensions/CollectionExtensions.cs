using System;
using System.Collections.Generic;
using System.Linq;

namespace MilitaryFaculty.Extensions
{
    public static class CollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            if (collection == null)
            {
                throw new ArgumentNullException();
            }

            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            foreach (var item in items)
            {
                collection.Add(item);
            }
        }

        public static void RemoveSingle<T>(this ICollection<T> collection, Func<T, bool> predicate)
        {
            if (collection == null)
            {
                throw new ArgumentNullException();
            }

            var target = collection.Single(predicate);
            collection.Remove(target);
        }

        public static void RemoveAll<T>(this ICollection<T> collection, Func<T, bool> predicate)
        {
            if (collection == null)
            {
                throw new ArgumentNullException();
            }

            var items = collection.Where(predicate);

            foreach (var item in items)
            {
                collection.Remove(item);
            }
        }
    }
}