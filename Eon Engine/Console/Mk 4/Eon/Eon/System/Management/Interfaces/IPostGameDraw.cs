/* Created: 15/01/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Interfaces;

namespace Eon.System.Management.Interfaces
{
    /// <summary>
    /// Defines an interface that is used to draw objects
    /// outside of the normal game loop. ie for fps counters.
    /// </summary>
    public interface IPostGameDraw : IUpdate
    {
        void PostGameDraw();
    }
}
