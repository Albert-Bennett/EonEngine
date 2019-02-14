/* Created: 13/08/2014
 * Last Updated: 07/06/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;
using Eon.Helpers;
using Eon.System.Management;
using System.Collections.Generic;

namespace Eon.Engine.Languages
{
    /// <summary>
    /// Used to define a manager class for dictionaries.
    /// </summary>
    public class DictionaryManager : EngineModule
    {
        static EonDictionary<string, Dictionary> dictionaries =
            new EonDictionary<string, Dictionary>();

        static List<string> languages = new List<string>();

        static string currentLanguage;

        /// <summary>
        /// The languages available.
        /// </summary>
        public static List<string> Languages
        {
            get { return languages; }
        }

        /// <summary>
        /// The current language of the Game.
        /// </summary>
        public static string CurrentLanguage
        {
            get { return currentLanguage; }
        }

        public DictionaryManager()
            : base("DictionaryManager")
        {
            DictionaryManager.currentLanguage = "";
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
                Dictionary dict = SerializationHelper.Deserialize<Dictionary>(dictionaryFilepath, true, ".Dict");

                if (!languages.Contains(dict.Language))
                {
                    dictionaries.Add(dict.Language, dict);
                    languages.Add(dict.Language);

                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Finds the translation of a word.
        /// </summary>
        /// <param name="word">The word to find the translation of.</param>
        /// <returns>The result of the translation.</returns>
        public static string FindTranslation(string word)
        {
            if (currentLanguage != "")
            {
                Dictionary dictionary = dictionaries.GetValue(currentLanguage);

                return dictionary.Find(word);
            }

            return word;
        }

        /// <summary>
        /// Finds the translation of a word.
        /// </summary>
        /// <param name="language">The language to be translated to.</param>
        /// <param name="word">The word to find the translation of.</param>
        /// <returns>The result of the translation.</returns>
        public static string FindTranslation(string language, string word)
        {
            if (dictionaries.Contains(language))
            {
                Dictionary dictionary = dictionaries.GetValue(language);

                return dictionary.Find(word);
            }

            return word;
        }

        /// <summary>
        /// Changes the current language of the Game.
        /// </summary>
        /// <param name="language">The current language.</param>
        public static void ChangeLanguage(string language)
        {
            if (languages.Contains(language))
                currentLanguage = language;
        }
    }
}
