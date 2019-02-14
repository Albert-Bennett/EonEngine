/* Created 30/10/2013
 * Last Updated: 23/03/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.Game.Misc.Interfaces
{
    /// <summary>
    /// Defines an interface that is used to end levels.
    /// </summary>
    public interface IChunkExit
    {
        /// <summary>
        /// Used to signal when the next layer should be loaded.
        /// </summary>
        NextChunkLoadEvent OnLoadRequested { get; set; }

        /// <summary>
        /// The next LevelChunk to be loaded.
        /// [END] = end of the Level and the next one gets loaded.
        /// </summary>
        string NextChunk { get; }

        void LoadNextChunk();
    }
}
