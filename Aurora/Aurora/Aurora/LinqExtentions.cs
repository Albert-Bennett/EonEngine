/* Created: 28/08/2015
 * Last Updated: 29/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using System.Collections.Generic;
using System.Linq;

namespace Aurora
{
    /// <summary>
    /// Defines several Linq expressions.
    /// </summary>
    public static class LinqExtentions
    {
        public static IEnumerable<T> SkipLast<T>(this IEnumerable<T> source, int count)
        {
            Queue<T> queue = new Queue<T>();
            int saved = 0;

            foreach (T item in source)
            {
                if (saved < count)
                {
                    queue.Enqueue(item);
                    saved++;

                    continue;
                }

                queue.Enqueue(item);

                yield return queue.Dequeue();
            }

            yield break;
        }
    }
}
