/* Created 09/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using System;
using System.Collections.Generic;

namespace Eon.Collections
{
    /// <summary>
    /// Used to define a type of list that when used wastes 
    /// less memory then the standard list collection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class EonList<T>
    {
        T[] items = null;
        int capicity = 0;

        public const int MinimumSize = 8;

        /// <summary>
        /// The number of items in the collection.
        /// </summary>
        public int Count { get { return capicity; } }

        /// <summary>
        /// Finds an item at a particular index.
        /// </summary>
        /// <param name="index">The index of the item to be found.</param>
        /// <returns>The found item.</returns>
        public T this[int index]
        {
            get { return items[index]; }
            set { items[index] = value; }
        }

        /// <summary>
        /// Creates a new EonList of a given size.
        /// </summary>
        /// <param name="capacity">The size of the EonList to be created.</param>
        public EonList(int capacity)
        {
            Clear(capacity);
        }

        /// <summary>
        /// Creates a new EonList with a capacity equal to the minimum capacity (8). 
        /// </summary>
        public EonList() : this(MinimumSize) { }

        /// <summary>
        /// Adds an item to the EonList.
        /// </summary>
        /// <param name="item">The item to be added.</param>
        public void Add(T item)
        {
            capicity++;

            if (capicity >= items.Length)
                Array.Resize<T>(ref items, items.Length << 1);

            items[capicity] = item;
        }

        /// <summary>
        /// Removes an item from the EonList.
        /// </summary>
        /// <param name="item">The item to be removed.</param>
        /// <returns>Wheather or not the item has been removed.</returns>
        public bool Remove(T item)
        {
            int idx = GetIndex(item);

            if (idx >= 0)
            {
                RemoveAt(idx);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Used to remove an item at a given index.
        /// </summary>
        /// <param name="index">The index of the item to be removed.</param>
        public void RemoveAt(int index)
        {
            capicity--;

            if (index < capicity)
                Array.Copy(items, index + 1, items, index, capicity - index);

            if (capicity < 0)
                capicity = 0;

            items[capicity] = default(T);
        }

        /// <summary>
        /// A check to see if the EonList contains a particular item.
        /// </summary>
        /// <param name="item">The item to check for.</param>
        /// <returns>The result of the check.</returns>
        public bool Contains(T item)
        {
            if (item == null)
            {
                for (int i = 0; i < capicity; i++)
                    if (items[i] == null)
                        return true;
            }
            else
            {
                EqualityComparer<T> compare = EqualityComparer<T>.Default;

                for (int i = 0; i < capicity; i++)
                    if (compare.Equals(items[i], item))
                        return true;
            }

            return false;
        }

        /// <summary>
        /// Clears the EonList. 
        /// </summary>
        public void ClearAll()
        {
            Clear(MinimumSize);
        }

        /// <summary>
        /// Clears the list to a minimum size.
        /// </summary>
        /// <param name="capacity">The siez of the EonList to clear to.</param>
        public void Clear(int capacity)
        {
            items = new T[capacity];
            capacity = 0;
        }

        /// <summary>
        /// Gets the index of a gicen item.
        /// </summary>
        /// <param name="item">The item to get the index of.</param>
        /// <returns>The index of the item.</returns>
        public int GetIndex(T item)
        {
            return Array.IndexOf(items, item);
        }

        /// <summary>
        /// Converts this Eonlist to an array of the same type.
        /// </summary>
        /// <returns>The EonList as an array.</returns>
        public T[] ToArray()
        {
            if (Count == items.Length)
                return items;

            T[] array = new T[Count];
            Array.Copy(items, array, Count);
            return array;
        }
    }
}
