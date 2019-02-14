/* Created 12/03/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.UIApi.Screens
{
    /// <summary>
    /// Used to define the various states that a MenuScreen can be in.
    /// </summary>
    public enum ScreenStates
    {
        On,
        TransitioningOn,
        TransitioningOff,
    }

    /// <summary>
    /// A delegate used to define what happends 
    /// when the selected index of controls has changed. 
    /// </summary>
    /// <param name="index">The newly selected control index.</param>
    public delegate void SelectedIndexChangedEvent(int index);
}
