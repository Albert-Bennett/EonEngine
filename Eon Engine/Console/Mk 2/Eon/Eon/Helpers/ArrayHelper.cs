/* Created 09/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using System.Collections.Generic;

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

        /// <summary>
        /// Removes an item at a given index.
        /// </summary>
        /// <typeparam name="T">The type of array.</typeparam>
        /// <param name="index">The index of the item.</param>
        /// <param name="array">The array to be modified.</param>
        /// <returns>The result of the modification.</returns>
        public static T[] RemoveAt<T>(int index, T[] array)
        {
            List<T> list = new List<T>(array);
            list.RemoveAt(index);

            return list.ToArray();
        }

        /// <summary>
        /// Remove an item from an array.
        /// </summary>
        /// <typeparam name="T">The type of array to be used.</typeparam>
        /// <param name="item">The item to be removed.</param>
        /// <param name="array">The array to be modified.</param>
        /// <returns>The modified array.</returns>
        public static T[] RemoveItem<T>(T item, T[] array)
        {
            List<T> list = new List<T>(array);
            list.Remove(item);

            return list.ToArray();
        }

        /// <summary>
        /// Adds an item to an array.
        /// </summary>
        /// <typeparam name="T">The type of array to be used.</typeparam>
        /// <param name="item">The item to be added.</param>
        /// <param name="array">The array to be modified.</param>
        /// <returns>The modified array.</returns>
        public static T[] AddItem<T>(T item, T[] array)
        {
            if (array.Length == 0)
            {
                array = new T[] { item };
                return array;
            }

            List<T> list = new List<T>(array);
            list.Add(item);

            return list.ToArray();
        }

        /// <summary>
        /// Adds a number of items to an array.
        /// </summary>
        /// <typeparam name="T">The type of items to be added.</typeparam>
        /// <param name="array">The array to have items added in to it.</param>
        /// <param name="items">The item to be added.</param>
        /// <returns>A new array containing all of the items.</returns>
        public static T[] AddItems<T>(T[] array, T[] items)
        {
            List<T> a = new List<T>(array);

            for (int i = 0; i < items.Length; i++)
                a.Add(items[i]);

            return a.ToArray();
        }

        /// <summary>
        /// Used to check to see if an item exists in the given array.
        /// </summary>
        /// <typeparam name="T">The type of item to be checked for.</typeparam>
        /// <param name="item">The item to be searched for.</param>
        /// <param name="array">The array to check.</param>
        /// <returns>The result of the check.</returns>
        public static bool Contains<T>(T item, T[] array)
        {
            bool contains = false;

            int idx = 0;

            while (idx < array.Length && !contains)
            {
                if (array[idx].Equals(item))
                    contains = true;

                idx++;
            }

            return contains;
        }
    }
}
