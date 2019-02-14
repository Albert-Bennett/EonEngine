/* Created: 18/09/2014
 * Last Updated: 20/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Interfaces;
using Eon.System.States;
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
        List<IActive> activatableControls;

        int selectedIndex = -1;
        int previousIndex = 0;

        bool showCursor = true;
        bool ignoreCamera = true;

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
        /// Should the current Camera2D be ignored when calculating 
        /// if a position is inside a MenuItem belonging to this.
        /// </summary>
        public bool IgnoreCamera
        {
            get { return ignoreCamera; }
            set { ignoreCamera = value; }
        }

        /// <summary>
        /// The index of the currently previously selected control.
        /// </summary>
        public int PrevSelectedIndex
        {
            get { return previousIndex; }
        }

        /// <summary>
        /// The currenly hovered over control.
        /// </summary>
        protected MenuItem CurrentControl
        {
            get
            {
                if (selectedIndex > -1)
                    return controls[selectedIndex];

                return null;
            }
        }

        /// <summary>
        /// The previously selected control.
        /// </summary>
        protected MenuItem PreviousControl
        {
            get
            {
                if (previousIndex > -1)
                    return controls[previousIndex];

                return null;
            }
        }

        /// <summary>
        /// Used to define what state the MenuScreen is currently in.
        /// </summary>
        protected ScreenStates CurrentState
        {
            get { return currentState; }
        }

        public Screen(string id, GameStates precidence) : base(id) 
        {
            this.Presidence = precidence;
        }

        internal void Attach(MenuItem control)
        {
            if (controls == null)
                controls = new List<MenuItem>();

            if (MenuManager.IgnoreMouse && selectedIndex == -1)
                selectedIndex = controls.Count;

            control.ControlIndex = controls.Count;
            controls.Add(control);

            if (control is IActive)
            {
                if (activatableControls == null)
                    activatableControls = new List<IActive>();

                activatableControls.Add(control as IActive);
                (control as IActive).OnActivate += new OnActivatedEvent(OnControlActive);
            }
        }

        void OnControlActive(IActive activeObject)
        {
            if (activatableControls.Count > 1)
                for (int i = 0; i < activatableControls.Count; i++)
                    if (activatableControls[i] != activeObject &&
                        activatableControls[i].Activated)
                        activatableControls[i].ToogleActive();
        }

        protected override void Update()
        {
            if (MenuManager.Cursor != null)
                if (MenuManager.Cursor.Enabled != showCursor)
                    MenuManager.Cursor.ToogleEnable();

            if(controls != null && MenuManager.IgnoreMouse)
                if (!CurrentControl.IsSelectable)
                {
                    bool found = false;
                    int idx = CurrentControl.ControlIndex + 1;

                    while (!found && idx < controls.Count)
                    {
                        if (controls[idx].IsSelectable)
                        {
                            selectedIndex = controls[idx].ControlIndex;
                            found = true;
                        }

                        idx++;
                    }
                }

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
        /// Finds the control of the given index.
        /// </summary>
        /// <param name="controlID">The ID of the control.</param>
        protected MenuItem GetControl(string controlID)
        {
            MenuItem control = null;

            bool found = false;
            int idx = 0;

            while (!found && idx < controls.Count)
            {
                if (controls[idx].ID == controlID)
                {
                    control = controls[idx];
                    found = true;
                }

                idx++;
            }

            return control;
        }

        /// <summary>
        /// Used to cycle through controls in an downward direction.
        /// </summary>
        protected virtual void ChangeDown()
        {
            if (controls.Count > 0)
            {
                int temp = selectedIndex;
                temp++;

                bool changed = false;
                int count = controls.Count;

                while (count > 0 && !changed)
                {
                    if (temp == controls.Count)
                        temp = 0;

                    if (controls[temp].IsSelectable)
                    {
                        previousIndex = selectedIndex;
                        selectedIndex = temp;
                        changed = true;
                    }

                    temp++;
                    count--;
                }
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

                bool changed = false;
                int count = controls.Count;

                while (count > 0 && !changed)
                {
                    if (temp < 0)
                        temp = controls.Count - 1;

                    if (controls[temp].IsSelectable)
                    {
                        previousIndex = selectedIndex;
                        selectedIndex = temp;
                        changed = true;
                    }

                    count--;
                    temp--;
                }
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

            if (activatableControls != null)
                activatableControls.Clear();

            activatableControls = null;

            base.Destroy();
        }

        internal void ChangeSelectedIndex(int index)
        {
            if (controls != null)
                if (index < controls.Count && selectedIndex != index)
                    if (index > -1)
                    {
                        if (controls[index].IsSelectable)
                        {
                            if (selectedIndex > -1)
                                previousIndex = selectedIndex;

                            selectedIndex = index;
                        }
                    }
                    else
                        selectedIndex = index;
        }
    }
}
