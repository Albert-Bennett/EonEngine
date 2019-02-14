/* Created 15/01/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Interfaces;

namespace Eon.EngineComponents.Interfaces.System
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
