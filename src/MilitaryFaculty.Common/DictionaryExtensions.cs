using System;
using System.Collections.Generic;
using System.Linq;

namespace MilitaryFaculty.Common
{
    public static class DictionaryExtensions
    {
        public static TValue FirstOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> source,
                                                          Func<TKey, bool> predicate)
        {
            var result = source.Where(pair => predicate(pair.Key))
                               .ToList();

            return result.Count > 0 ? result[0].Value : default(TValue);
        }
    }
}