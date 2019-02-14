/* Created 30/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.Game.Misc.Interfaces
{
    /// <summary>
    /// Defines an interface that is used to end levels.
    /// </summary>
    public interface IExit : ILevelAsset
    {
        string NextLevel { get; }

        void ExitLevel();
    }
}
