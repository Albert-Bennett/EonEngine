/* Created: 13/08/2014
 * Last Updated: 07/06/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;
using System.Linq;

namespace Eon.Engine.Languages
{
    /// <summary>
    /// Used to define a dictionary of words.
    /// </summary>
    public sealed class Dictionary
    {
        public string Language;
        public EonDictionary<string, string> Entries;

        /// <summary>
        /// Finds the translation of a word or phrase in the Dictionary.
        /// </summary>
        /// <param name="word">The word to find the translation of.</param>
        /// <returns>The translated word.</returns>
        internal string Find(string word)
        {
            if (Entries.Contains(word))
                return Entries[word];

            return word;
        }
    }
}
