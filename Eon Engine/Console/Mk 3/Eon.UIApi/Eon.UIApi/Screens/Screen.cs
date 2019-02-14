/* Created: 18/09/2014
 * Last Updated: 18/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.UIApi.Controls;
using System.Collections.Generic;

namespace Eon.UIApi.Screens
{
    /// <summary>
    /// Used to define a Screen.
    /// </summary>
    public abstract class Screen : GameObject
    {
        List<MenuItem> controls;

        int selectedIndex = 0;

        ScreenStates currentState = ScreenStates.TransitioningOn;

        /// <summary>
        /// The index of the currently selected control.
        /// </summary>
        public int SelectedIndex
        {
            get { return selectedIndex; }
            protected set { selectedIndex = value; }
        }

        /// <summary>
        /// The currenly hovered over control.
        /// </summary>
        protected MenuItem CurrentControl
        {
            get { return controls[selectedIndex]; }
        }

        /// <summary>
        /// Used to define what state the MenuScreen is currently in.
        /// </summary>
        protected ScreenStates CurrentState
        {
            get { return currentState; }
        }

        public Screen(string id) : base(id) { }

        internal void Attach(MenuItem control)
        {
            if (controls == null)
                controls = new List<MenuItem>();

            control.Idx = controls.Count;
            controls.Add(control);
        }

        protected override void Update()
        {
            switch (currentState)
            {
                case ScreenStates.TransitioningOn:
                    TransitionOnAction();
                    break;

                case ScreenStates.Active:
                    HandleInput();
                    break;

                case ScreenStates.TransitioningOff:
                    TransitionOffAction();
                    break;
            }

            base.Update();
        }

        protected virtual void HandleInput() { }

        /// <summary>
        /// Used to cycle through controls in an downward direction.
        /// </summary>
        protected virtual void ChangeDown()
        {
            if (controls.Count > 0)
            {
                int temp = selectedIndex;
                temp++;

                if (temp < controls.Count)
                    selectedIndex = temp;
                else
                    selectedIndex = 0;
            }
        }

        /// <summary>
        /// Used to cycle through controls in an upward direction.
        /// </summary>
        protected virtual void ChangeUp()
        {
            if (controls.Count > 0)
            {
                int temp = selectedIndex;
                temp--;

                if (temp < 0)
                    selectedIndex = controls.Count - 1;
                else
                    selectedIndex = temp;
            }
        }

        /// <summary>
        /// Disable this MenuScreen.
        /// </summary>
        internal void _TransitionOff()
        {
            currentState = ScreenStates.TransitioningOff;
        }

        /// <summary>
        /// Used to define what happends when this MenuScreen becomes active.
        /// </summary>
        protected virtual void TransitionOnAction()
        {
            currentState = ScreenStates.Active;
        }

        /// <summary>
        /// Used to define what happends when the MenuScreen is becomming in-active.
        /// </summary>
        protected virtual void TransitionOffAction()
        {
            Destroy();
        }

        /// <summary>
        /// Resets the Screen.
        /// </summary>
        protected virtual void ResetScreen()
        {
            selectedIndex = 0;
        }

        public override void Destroy()
        {
            if (controls != null)
            {
                foreach (MenuItem item in controls)
                    item.Destroy();

                controls.Clear();
                controls = null;
            }

            base.Destroy();
        }

        internal void ChangeSelectedIndex(int index)
        {
            if (index < controls.Count)
                selectedIndex = index;
        }
    }
}
