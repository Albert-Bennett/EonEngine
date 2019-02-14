/* Created 10/04/2015
 * Last Updated: 10/04/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Collections;

namespace Eon.Game.LevelManagement
{
    /// <summary>
    /// Defines a Level.
    /// </summary>
    public class LevelInfo
    {
        public string Name;
        public EonDictionary<string, LevelChunk> LevelChunks;
        public ParameterCollection PlayerStart;
    }
}
