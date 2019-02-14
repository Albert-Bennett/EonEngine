/* Created 13/08/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;
using Eon.System.Management;
using System.Collections.Generic;

namespace Eon.Engine.Languages
{
    /// <summary>
    /// Used to define a manager class for dictionaries.
    /// </summary>
    public class DictionaryManager : EngineComponent
    {
        static List<Dictionary> dictionaries = new List<Dictionary>();
        static List<string> languages = new List<string>();

        static string defaultLanguage;

        /// <summary>
        /// The languages available.
        /// </summary>
        public static List<string> Languages
        {
            get { return languages; }
        }

        /// <summary>
        /// The default language of the Game.
        /// </summary>
        public string DefaultLanguage
        {
            get { return defaultLanguage; }
        }

        public DictionaryManager(string defaultLanguage) : base("DictionaryManager")
        {
            DictionaryManager.defaultLanguage = defaultLanguage;
        }

        /// <summary>
        /// Used to load a dictionary.
        /// </summary>
        /// <param name="dictionaryFilepath">The dictionary's filepath.</param>
        /// <returns>true if a dictionary has been loaded.</returns>
        internal static bool LoadDictionary(string dictionaryFilepath)
        {
            try
            {
                Dictionary dict = XmlHelper.DeserializeContent<Dictionary>(dictionaryFilepath + ".Dict");
                dictionaries.Add(dict);

                languages.Add(dict.Langauage);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Finds the translation of a word.
        /// </summary>
        /// <param name="language">The language to be translated to.</param>
        /// <param name="word">The word to find the translation of.</param>
        /// <returns>The result of the translation.</returns>
        public static string FindTranslation(string language, string word)
        {
            bool found = false;
            int idx = 0;

            while (!found && idx < languages.Count)
            {
                if (languages[idx] == language)
                    found = true;

                idx++;
            }

            if (found)
                return dictionaries[idx].Find(word);

            return word;
        }

        /// <summary>
        /// Finds the translation of a word.
        /// </summary>
        /// <param name="languageIndex">The index of the language to be used.</param>
        /// <param name="word">The word to find the translation of.</param>
        /// <returns>The result of the translation.</returns>
        public static string FindTranslation(int languageIndex, string word)
        {
            if (languageIndex < languages.Count)
            {
                bool found = false;
                int idx = 0;

                while (!found && idx < languages.Count)
                {
                    if (idx == languageIndex)
                        found = true;

                    idx++;
                }

                if (found)
                    return dictionaries[idx].Find(word);
            }

            return word;
        }
    }
}
