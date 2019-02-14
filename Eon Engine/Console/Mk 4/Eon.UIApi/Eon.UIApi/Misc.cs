/* Created: 12/03/2014
 * Last Updated: 16/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.UIApi.Controls;

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

    /// <summary>
    /// A delegate that defines if the Controls was selected.
    /// </summary>
    /// <param name="previous">Was the previously selected.</param>
    /// <param name="controlIndex">The index of the control.</param>
    public delegate void OnSelectedEvent(bool previous, int controlIndex);

    /// <summary>
    /// A delegate used to signal when the text of a control has changed. 
    /// </summary>
    /// <param name="controlID">The ID of the control that had it's text changed.</param>
    /// <param name="text">The current text in the text based control.</param>
    public delegate void OnTextChangedEvent(string controlID, string text);

    /// <summary>
    /// A delegate used to signal when an object that can have 
    /// it's value toogled is changed.
    /// </summary>
    /// <param name="controlID">The ID of the control that had it's value toogled.</param>
    /// <param name="check">The new value.</param>
    public delegate void OnCheckToogledEvent(string controlID, bool check);
}
