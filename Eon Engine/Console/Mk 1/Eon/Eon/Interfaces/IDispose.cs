/* Created 09/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

namespace Eon.Interfaces
{
    /// <summary>
    /// An interface used to dispose of Components
    /// </summary>
    public interface IDispose
    {
        void Dispose(bool finalize);
    }
}
