/* Created 13/08/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using System.Linq;

namespace Eon.Engine.Languages
{
    /// <summary>
    /// Used to define a dictionary of words.
    /// </summary>
    public class Dictionary
    {
        public string Langauage;
        public Entry[] Entries;

        /// <summary>
        /// Finds the translation of a word or phrase in the Dictionary.
        /// </summary>
        /// <param name="word">The word to find the translation of.</param>
        /// <returns>The translated word.</returns>
        internal string Find(string word)
        {
            Entry entry = (from e in Entries
                           where e.Word == word
                           select e).FirstOrDefault();

            return entry.Translation;
        }
    }
}
