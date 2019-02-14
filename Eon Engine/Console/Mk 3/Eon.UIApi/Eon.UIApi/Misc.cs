/* Created: 12/03/2014
 * Last Updated: 18/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.UIApi.Screens
{
    /// <summary>
    /// A delegate used to define what hapends when a MenuItem has been clicked.
    /// </summary>
    /// <param name="controlID">The ID of the MenuItem.</param>
    public delegate void OnClickedEvent(string controlID);

    /// <summary>
    /// A delegate used to signal the closing of a PopUpMenu.
    /// </summary>
    /// <param name="menuID">The ID of the PopUpMenu being closed.</param>
    public delegate void OnPopUpCloseEvent(string menuID);
}
