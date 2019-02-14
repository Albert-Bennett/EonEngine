/* Created: 03/10/2015
 * Last Updated: 17/10/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.System.Interfaces.Base
{
    /// <summary>
    /// Used to define an object as being able to be destroyed.
    /// </summary>
    public interface IDestructable
    {
        bool IsDestroyed { get; }
        void Destroy();
    }
}
