/* Created 09/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.System.Interfaces
{
    /// <summary>
    /// Used to define an object
    /// that can be updated.
    /// </summary>
    public interface IUpdate
    {
        int Priority { get; }
        void _Update();
    }
}
