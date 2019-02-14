/* Created: 09/06/2013
 * Last Updated: 05/04/2015
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
        void _PostUpdate();
    }
}
