using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AccessAllAgents.MicroService.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool ScrambledEquals<T>(this List<T> list1, List<T> list2)
        {
            var deletedItems = list1.Except(list2).Any();
            var newItems = list2.Except(list1).Any();
            return !newItems && !deletedItems;
        }

        public static string ToFriendlyString(this IEnumerable enumerable, string separator = ",")
        {
            return string.Join(separator, enumerable);
        }

        public static string ToFriendlyString<T>(this IEnumerable<T> enumerable, string separator = ",")
        {
            return string.Join(separator, enumerable);
        }

        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> collection, int batchSize)
        {
            var nextBatch = new List<T>(batchSize);
            foreach (T item in collection)
            {
                nextBatch.Add(item);
                if (nextBatch.Count == batchSize)
                {
                    yield return nextBatch;
                    nextBatch = new List<T>(batchSize);
                }
            }

            if (nextBatch.Count > 0)
                yield return nextBatch;
        }
    }
}