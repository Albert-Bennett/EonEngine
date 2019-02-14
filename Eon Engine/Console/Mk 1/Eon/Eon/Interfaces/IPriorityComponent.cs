/* Created 25/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

namespace Eon.Interfaces
{
    /// <summary>
    /// An interface used to define which order 
    /// should EngineComponents be updated.
    /// </summary>
    public interface IPriorityComponent : IUpdate
    {
        int Priority { get; }
    }
}
