/* Created: 20/01/2015
 * Last Updated: 20/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using EEDK.Crosswalk.Docks;

namespace EEDK.Crosswalk.Controls
{
    /// <summary>
    /// A delegate used to singal when a combo 
    /// box has had it's selected item changed. 
    /// </summary>
    /// <param name="dock">The parent control.</param>
    public delegate void ComboBoxSelectionChangedEvent(SelectionDock dock);

    /// <summary>
    /// A delegate used to signal when a request 
    /// to remove an AddableDock has been made.
    /// </summary>
    /// <param name="dock">The AddableDock to be removed.</param>
    public delegate void ClickRelayEvent(AddableDock dock);
}
