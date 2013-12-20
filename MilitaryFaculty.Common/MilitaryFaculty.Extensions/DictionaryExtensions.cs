using System;
using System.Collections.Generic;
using System.Linq;

namespace MilitaryFaculty.Extensions
{
    public static class DictionaryExtensions
    {
        public static TValue ValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key)
        {
            TValue result;
            return source.TryGetValue(key, out result) ? result : default(TValue);
        }

        public static TValue FirstOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> source,
                                                          Func<TKey, bool> predicate)
        {
            var result = source.Where(pair => predicate(pair.Key))
                               .ToList();

            return result.Count > 0 ? result[0].Value : default(TValue);
        }
    }
}