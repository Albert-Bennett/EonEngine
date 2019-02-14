/* Created: 09/06/2013
 * Last Updated: 08/09/2014
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
        void PostInitialize(object context);
    }
}
