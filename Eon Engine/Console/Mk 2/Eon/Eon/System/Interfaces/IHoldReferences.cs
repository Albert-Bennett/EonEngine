/* Created 11/01/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.System.Interfaces
{
    /// <summary>
    /// Used to define an object that 
    /// holds references to other object.
    /// </summary>
    public interface IHoldReferences
    {
        void Remove(object obj);
    }
}
