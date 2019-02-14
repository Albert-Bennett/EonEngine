/* Created 15/08/2015
 * Last Updated: 16/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.System.Interfaces
{
    /// <summary>
    /// Defines an object that can be activated.
    /// </summary>
    public interface IActive
    {
        bool Activated { get; }
        OnActivatedEvent OnActivate { get; set; }

        void ToogleActive();
    }
}
