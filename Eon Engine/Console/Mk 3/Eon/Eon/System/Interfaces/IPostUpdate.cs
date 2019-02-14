/* Created: 11/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.System.Interfaces
{
    /// <summary>
    /// An interface used by ObjectComponent to 
    /// preform actions after the inintial update phase. 
    /// </summary>
    public interface IPostUpdate
    {
        void _PostUpdate();
    }
}
