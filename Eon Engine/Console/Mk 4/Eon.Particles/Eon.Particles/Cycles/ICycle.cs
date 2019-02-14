/* Created: 19/01/2015
 * Last Updated: 19/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.Particles.Cycles
{
    /// <summary>
    /// Used to define generation cycles.
    /// </summary>
    public interface ICycle
    {
        int TotalSpawned { get; }
        int ToBeSpawned { get; }

        void Update();
        void Reset();
    }
}
