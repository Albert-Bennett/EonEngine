/* Created 04/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Engine
{
    /// <summary>
    /// Defines the informaton that Framework needs inorder to be created.
    /// </summary>
    public class FrameworkCreation
    {
        public int DefaultScreenResolution = 0;
        public string DefaultLanguage = "English";
        public string[] DictionaryFilepaths = new string[0];
        public string[] EngineComponents = new string[0];
        public string[] AssemblyRefferences = new string[0];
    }
}
