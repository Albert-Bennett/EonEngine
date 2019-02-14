/* Created 15/04/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using System.Collections;
using System.Collections.Generic;

namespace Eon.Collections
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">The type of items to be held in this HashSet.</typeparam>
    public class HashSet<T> : IEnumerable<T>, ICollection<T>
    {
        object nullObj = new object();

        Dictionary<T, object> objects;

        public int Count
        {
            get { return objects.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public HashSet()
        {
            objects = new Dictionary<T, object>();
        }

        public HashSet(IEnumerable<T> source)
        {
            objects = new Dictionary<T, object>();
            Add(source);
        }

        /// <summary>
        /// Adds an item to this HashSet.
        /// </summary>
        /// <param name="item">The item to be added.</param>
        /// <returns>Wheather or not that item was added.</returns>
        public bool Add(T item)
        {
            int count = objects.Count;

            objects[item] = nullObj;

            if (count == objects.Count)
                return false;

            return true;
        }

        void ICollection<T>.Add(T item)
        {
            Add(item);
        }

        /// <summary>
        /// Adds a collection of items to this HashSet.
        /// </summary>
        /// <param name="collection"></param>
        public void Add(IEnumerable<T> collection)
        {
            foreach (T item in collection)
                Add(item);
        }

        /// <summary>
        /// A check to see if an item is in the HashSet.
        /// </summary>
        /// <param name="item">The item to be searched for.</param>
        /// <returns>The result of the check.</returns>
        public bool Contains(T item)
        {
            return objects.ContainsKey(item);
        }

        /// <summary>
        /// Removes an item from this HashSet.
        /// </summary>
        /// <param name="item">The item to be removed.</param>
        /// <returns>Wheather or not the item was removed.</returns>
        public bool Remove(T item)
        {
            if (objects.ContainsKey(item))
            {
                objects.Remove(item);
                return true;
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return objects.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return objects.Keys.GetEnumerator();
        }

        /// <summary>
        /// Copies the items in the HashSet to an array. 
        /// </summary>
        /// <param name="array">The array that will hold the copied items.</param>
        /// <param name="arrayIndex">Index in the arry to start copping to.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            objects.Keys.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Clears all items from the HashSet.
        /// </summary>
        public void Clear()
        {
            objects.Clear();
        }
    }
}
