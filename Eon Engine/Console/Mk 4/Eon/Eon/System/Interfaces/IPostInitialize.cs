/* Created: 09/06/2013
 * Last Updated: 16/06/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.System.Interfaces
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
