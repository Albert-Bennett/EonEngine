/* Created 04/09/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine;
using Eon.System.Interfaces;
using Eon.UIApi.Screens;

namespace Eon.UIApi.Controls
{
    /// <summary>
    /// Defines an absrtract class for which all menu controls will inherit from. 
    /// </summary>
    public abstract class MenuItem : ObjectComponent, IUpdate
    {
        int idx;

        public OnClickedEvent OnClicked;

        bool prevClicked = true;

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

        public int Priority
        {
            get { return 0; }
        }

        public MenuItem(string id) : base(id) { }

        public void _Update()
        {
            bool clicked = false;

            if (OnClicked != null)
                if (IsMouseInside() || HoveredOver)
                    if (!prevClicked && (InputManager.IsButtonStroked(MouseBtns.Left) || InputManager.IsKeyStroked(Keys.Enter)))
                    {
                        clicked = true;
                        OnClicked(ID);
                    }

            prevClicked = clicked;

            Update();
        }

        protected virtual void Update() { }

        internal void _SetIndex(int index)
        {
            this.idx = index;
        }

        protected abstract bool IsMouseInside();
    }
}
