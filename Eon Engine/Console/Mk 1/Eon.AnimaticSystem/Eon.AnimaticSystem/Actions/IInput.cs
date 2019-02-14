/* Created 16/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

namespace Eon.AnimaticSystem.Actions
{
    /// <summary>
    /// Used to define an action that can 
    /// intake objects in order to preform an action.
    /// </summary>
    public interface IInput
    {
        void Input(object obj);
    }
}
