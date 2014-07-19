using System;
using System.Collections.Generic;
using System.Linq;

namespace MilitaryFaculty.Common
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            foreach (var item in collection)
            {
                action(item);
            }
        }

        public static bool IsEmpty<T>(this IEnumerable<T> @this)
        {
            if (@this == null)
            {
                throw new ArgumentNullException("this");
            }

            return !@this.Any();
        }
    }
}