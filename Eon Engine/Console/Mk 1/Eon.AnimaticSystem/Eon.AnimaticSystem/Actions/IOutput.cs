/* Created 16/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

namespace Eon.AnimaticSystem.Actions
{
    /// <summary>
    /// An interface that is used to define 
    /// Actions that can return an object.
    /// </summary>
    public interface IOutput
    {
        string Target { get; }

        object Output();
    }
}
