using System.Collections.Generic;
using NotNull = JetBrains.Annotations.NotNullAttribute;

namespace Unity.Services.Core.Internal
{
    static class DictionaryExtensions
    {
        public static TDictionary MergeNoOverride<TDictionary, TKey, TValue>(
            this TDictionary self, [NotNull] IDictionary<TKey, TValue> dictionary)
            where TDictionary : IDictionary<TKey, TValue>
        {
            foreach (var entry in dictionary)
            {
                if (self.ContainsKey(entry.Key))
                    continue;

                self[entry.Key] = entry.Value;
            }

            return self;
        }

        public static TDictionary MergeAllowOverride<TDictionary, TKey, TValue>(
            this TDictionary self, [NotNull] IDictionary<TKey, TValue> dictionary)
            where TDictionary : IDictionary<TKey, TValue>
        {
            foreach (var entry in dictionary)
            {
                self[entry.Key] = entry.Value;
            }

            return self;
        }
    }
}
