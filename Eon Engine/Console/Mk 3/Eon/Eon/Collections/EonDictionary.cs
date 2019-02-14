/* Created: 09/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using System;
using System.Collections.Generic;

namespace Eon.Collections
{
    /// <summary>
    /// Defines a doctionary where their can be multiples of the key in it.
    /// </summary>
    /// <typeparam name="KeyType">The type of value that every key will hold.</typeparam>
    /// <typeparam name="ValueType">The type of value every value will have.</typeparam>
    public class EonDictionary<KeyType, ValueType>
    {
        List<KeyType> keys = new List<KeyType>();
        List<ValueType> values = new List<ValueType>();

        /// <summary>
        /// The keys in the EonDictionary.
        /// </summary>
        public List<KeyType> Keys 
        {  get { return keys; } }

        /// <summary>
        /// The values of the index keys in the EonDictionary.
        /// </summary>
        public List<ValueType> Values { get { return values; } }

        /// <summary>
        /// The number of key value pairs in the EonDictionary.
        /// </summary>
        public int Count { get { return keys.Count; } }

        /// <summary>
        /// Gets the EonKeyValuePair at a particular index.
        /// </summary>
        /// <param name="index">The index of the EonKeyValuePair.</param>
        /// <returns>A found EonKeyValuePair.</returns>
        public EonKeyValuePair<KeyType, ValueType> this[int index]
        {
            get
            {
                return new EonKeyValuePair<KeyType, ValueType>
                {
                    Key = keys[index],
                    Value = values[index]
                };
            }
            set
            {
                keys[index] = value.Key;
                values[index] = value.Value;
            }
        }

        /// <summary>
        /// Gets the value of an object at a given key.
        /// </summary>
        /// <param name="key">The key of the object to get the value of.</param>
        /// <returns>The result of the search.</returns>
        public ValueType this[KeyType key]
        {
            get
            {
                for (int i = 0; i < keys.Count; i++)
                    if (keys[i].Equals(key))
                        return values[i];

                return default(ValueType);
            }
            set
            {
                for (int i = 0; i < keys.Count; i++)
                    if (keys[i].Equals(key))
                        values[i] = value;
            }
        }

        /// <summary>
        /// Adds a EonKeyValuePair to this.
        /// </summary>
        /// <param name="key">The key for the value.</param>
        /// <param name="value">The value for the key.</param>
        public void Add(KeyType key, ValueType value)
        {
            bool added = false;

            for (int i = 0; i < keys.Count; i++)
                if (keys[i].Equals(key) && values[i].Equals(value))
                    added = true;

            if (!added)
            {
                keys.Add(key);
                values.Add(value);
            }
        }

        /// <summary>
        /// Adds a EonKeyValuePair to this.
        /// </summary>
        /// <param name="kvp">The EonKeyValuePair to be added.</param>
        public void Add(EonKeyValuePair<KeyType, ValueType> keyValuePair)
        {
            bool added = false;

            for (int i = 0; i < keys.Count; i++)
                if (!keys[i].Equals(keyValuePair.Key) || !values[i].Equals(keyValuePair.Value))
                    added = true;

            if (!added)
            {
                keys.Add(keyValuePair.Key);
                values.Add(keyValuePair.Value);
            }
        }

        /// <summary>
        /// Gets the value of a EonKeyValuePair by using it's index.
        /// </summary>
        /// <param name="index"The index of the value to get.></param>
        /// <returns>The found value.</returns>
        public EonKeyValuePair<KeyType, ValueType> GetValueByIndex(int index)
        {
            if (index < keys.Count)
                return new EonKeyValuePair<KeyType, ValueType>()
                {
                    Key = keys[index],
                    Value = values[index]
                };

            throw new ArgumentOutOfRangeException("The given index: " + index +
                " is out of the range of the collection.");
        }

        /// <summary>
        /// Removes a EonKeyValuePair from this using the key as a reference.
        /// </summary>
        /// <param name="key">The key to be used as reference.</param>
        /// <returns>The value of the EonKeyValuePair that was removed.</returns>
        public ValueType GetRemove(KeyType key)
        {
            object obj = null;
            ValueType val = (ValueType)obj;

            bool removed = false;

            if (Contains(key))
                for (int i = 0; i < Count; i++)
                    if (!removed)
                        if (keys[i].Equals(key))
                        {
                            removed = true;
                            keys.Remove(keys[i]);

                            val = values[i];
                            values.Remove(values[i]);
                        }

            return val;
        }

        /// <summary>
        /// Removes a EonKeyValuePair from this.
        /// </summary>
        /// <param name="key">The key of the EonKeyValuePair.</param>
        public void Remove(KeyType key)
        {
            bool removed = false;

            if (Contains(key))
                for (int i = 0; i < Count; i++)
                    if (!removed)
                        if (keys[i].Equals(key))
                        {
                            removed = true;
                            keys.Remove(keys[i]);
                            values.Remove(values[i]);
                        }
        }

        /// <summary>
        /// A check to see if a particular 
        /// key exists in the EonDictionary.
        /// </summary>
        /// <param name="key">The key from the EonKeyValuePair.</param>
        /// <returns>The result of the check.</returns>
        public bool Contains(KeyType key)
        {
            for (int i = 0; i < Count; i++)
                if (keys[i].Equals(key))
                    return true;

            return false;
        }

        /// <summary>
        /// Clears all items from the EonDictionary.
        /// </summary>
        public void Clear()
        {
            keys.Clear();
            values.Clear();
        }
    }
}
