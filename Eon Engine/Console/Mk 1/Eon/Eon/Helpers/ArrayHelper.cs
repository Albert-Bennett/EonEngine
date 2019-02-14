/* Created 09/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

namespace Eon.Helpers
{
    /// <summary>
    /// Defines a class that is used to help with arrays.
    /// </summary>
    public sealed class ArrayHelper
    {
        /// <summary>
        /// Used to insert an item into an array 
        /// at a specific index other items are equal
        /// to the item at the previous index.
        /// </summary>
        /// <typeparam name="T">The type of array.</typeparam>
        /// <param name="index">The index of the item to be inserted.</param>
        /// <param name="item">The item to be inserted.</param>
        /// <param name="array">The array to have the item inserted into.</param>
        /// <returns>The modified array.</returns>
        public static T[] InsertAt<T>(int index, T item, T[] array)
        {
            int len = array.Length;
            len++;

            T[] a2 = new T[len];

            for (int i = 0; i < a2.Length; i++)
            {
                if (i < index)
                    a2[i] = array[i];
                else if (i == index)
                    a2[i] = item;
                else
                {
                    int idx = i;
                    idx--;

                    a2[i] = array[idx];
                }
            }

            return a2;
        }
    }
}
