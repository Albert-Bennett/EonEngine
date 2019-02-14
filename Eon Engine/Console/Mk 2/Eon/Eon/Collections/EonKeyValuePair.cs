/* Created 09/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using System;

namespace Eon.Collections
{
    /// <summary>
    /// Used to define a Key value pair in a EonDictionary.
    /// </summary>
    /// <typeparam name="KeyType">The type of value the key of this EonKeyValuePair will be.</typeparam>
    /// <typeparam name="ValueType">The type of value the value of this EonKeyValuePair will be.</typeparam>
    [Serializable]
    public struct EonKeyValuePair<KeyType, ValueType>
    {
        public KeyType Key;
        public ValueType Value;
    }
}
