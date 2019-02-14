/* Created 05/03/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using System.Collections.Generic;

namespace Eon.Engine.Audio
{
    /// <summary>
    /// Defines a J-Jax file.
    /// </summary>
    public class JJax
    {
        //Names of all of the songs in use.
        public List<CueInfo> Info = new List<CueInfo>();

        /// <summary>
        /// Sound categories for all of the songs
        /// </summary>
        public List<string> SoundCategories = new List<string>();
    }
}
