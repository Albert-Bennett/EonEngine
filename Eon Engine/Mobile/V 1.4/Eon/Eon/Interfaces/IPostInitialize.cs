/* Created 09/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.Interfaces
{
    /// <summary>
    /// Used to define an object that
    /// initializes after every other
    /// object has been initialized.
    /// </summary>
    public interface IPostInitialize
    {
        void PostInitialize();
    }
}
