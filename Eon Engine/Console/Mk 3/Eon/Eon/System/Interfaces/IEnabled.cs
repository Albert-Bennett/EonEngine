/* Created: 09/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.System.Interfaces
{
    /// <summary>
    /// Used to define an object 
    /// that can be enabled and disabled.
    /// </summary>
    public interface IEnabled
    {
        bool Enabled { get; }

        void ToogleEnable();
        void Disable();
        void Enable();
    }
}
