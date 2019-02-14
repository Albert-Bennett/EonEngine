/* Created 14/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

namespace Eon.AnimaticSystem
{
    /// <summary>
    /// A delegate used to signal the end of an Action's use.
    /// </summary>
    internal delegate void FinishedActionEvent();

    /// <summary>
    /// A delegate used to signal the end of an AnimaticStream.
    /// </summary>
    internal delegate void EndOfStreamEvent(int streamNumber);

    /// <summary>
    /// A delegate used to signal the natural end of an Animatic.
    /// </summary>
    internal delegate void FinishedAnimaticEvent();
}
