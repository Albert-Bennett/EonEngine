/* Created 04/09/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.UIAPI.Screens;

namespace Eon.UIAPI.Controls
{
    /// <summary>
    /// Defines an absrtract class for which all menu controls will inherit from. 
    /// </summary>
    public abstract class MenuItem : ObjectComponent
    {
        int idx;

        /// <summary>
        /// Is this D2Control hovered over?
        /// </summary>
        public bool HoveredOver
        {
            get
            {
                if (Enabled)
                    if (IsMouseInside())
                        return true;
                    else
                        return ((MenuScreen)Owner).Selectedindex == idx;
                else
                    return false;
            }
        }

        public MenuItem(string id) : base(id) { }

        internal void _SetIndex(int index)
        {
            this.idx = index;
        }

        protected abstract bool IsMouseInside();
    }
}
