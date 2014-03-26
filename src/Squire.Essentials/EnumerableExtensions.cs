namespace Squire
{
    using Squire.Validation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class EnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> items, int itemsPerBatch)
        {
            items.VerifyParam("items").IsNotNull();
            itemsPerBatch.VerifyParam("itemsPerBatch").IsGreaterThanZero();
            var count = 0;
            var batch = new List<T>();
            foreach (var item in items)
            {
                if (count > 0 &&  count % itemsPerBatch == 0)
                {
                    yield return batch;
                    batch = new List<T>();
                }

                batch.Add(item);
                count++;
            }

            if (batch.Any())
            {
                yield return batch;
            }
        }
    }
}
