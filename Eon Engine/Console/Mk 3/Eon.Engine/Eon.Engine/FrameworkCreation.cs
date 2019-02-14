/* Created: 04/06/2013
 * Last Updated: 24/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;
using Microsoft.Xna.Framework;

namespace Eon.Engine
{
    /// <summary>
    /// Defines the informaton that Framework needs inorder to be created.
    /// </summary>
    public class FrameworkCreation
    {
        public int TargetFramerate = 166;
        public bool FullScreen = true;
        public int DefaultScreenResolution = 0;
        public int DefaultScreenSize = 0;
        public byte DefaultTextureQuality = 1;
        public string DefaultLanguage = "English";
        public string[] DictionaryFilepaths = new string[] { "NULL" };
        public ParameterCollection[] EngineComponents = null;
        public string[] AssemblyRefferences = new string[] { "NULL" };
        public string AudioMangerFilepath = "NULL";
    }
}
