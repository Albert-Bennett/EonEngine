/* Created 04/09/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.EngineComponents;
using Eon.UIApi.Controls;
using System.Collections.Generic;

namespace Eon.UIApi.Screens
{
    /// <summary>
    /// Defines a basic 2D / 3D screen.
    /// </summary>
    public abstract class MenuScreen : GameObject
    {
        List<MenuItem> controls;

        GameStates screenPrecidence = GameStates.MainMenu;
        ScreenStates currentState = ScreenStates.TransitioningOn;

        protected int selectedIndex = 0;
        protected bool ignoreMouse = false;

        /// <summary>
        /// The index of the currently selected control.
        /// </summary>
        internal int Selectedindex
        {
            get { return selectedIndex; }
        }

        /// <summary>
        /// Used to define what state the MenuScreen is currently in.
        /// </summary>
        protected ScreenStates CurrentState
        {
            get { return currentState; }
        }

        /// <summary>
        /// The controls attached to this MenuScreen.
        /// </summary>
        protected List<MenuItem> Controls
        {
            get { return controls; }
        }

        internal GameStates ScreenPrecidence
        {
            get { return screenPrecidence; }
        }

        public SelectedIndexChangedEvent OnSelectionChanged = null;

        /// <summary>
        /// Creates a new MenuScreen.
        /// </summary>
        /// <param name="id">The id for this MenuScreen.</param>
        /// <param name="screenPrecidence">Used to manage what precidence
        /// this MenuScreen works under.</param>
        public MenuScreen(string id, GameStates screenPrecidence)
            : base(id)
        {
            this.screenPrecidence = screenPrecidence;
        }

        protected override void Update()
        {
            switch (currentState)
            {
                case ScreenStates.On:
                    HandleInput();
                    break;

                case ScreenStates.TransitioningOn:
                    TransitionOnAction();
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
                ignoreMouse = true;

                int temp = selectedIndex;
                temp++;

                if (temp < controls.Count)
                {
                    selectedIndex = temp;

                    if (OnSelectionChanged != null)
                        OnSelectionChanged(temp);
                }
                else
                {
                    selectedIndex = 0;

                    if (OnSelectionChanged != null)
                        OnSelectionChanged(0);
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
                ignoreMouse = true;

                int temp = selectedIndex;
                temp--;

                if (temp < 0)
                {
                    selectedIndex = controls.Count - 1;

                    if (OnSelectionChanged != null)
                        OnSelectionChanged(controls.Count - 1);
                }
                else
                {
                    selectedIndex = temp;

                    if (OnSelectionChanged != null)
                        OnSelectionChanged(temp);
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
        /// Activate this MenuScreen.
        /// </summary>
        internal void _TransitionOn()
        {
            currentState = ScreenStates.TransitioningOn;
        }

        /// <summary>
        /// Used to define what happends when this MenuScreen becomes active.
        /// </summary>
        protected virtual void TransitionOnAction()
        {
            currentState = ScreenStates.On;
        }

        /// <summary>
        /// Used to define what happends when the MenuScreen is becommin in-active.
        /// </summary>
        protected virtual void TransitionOffAction() { }

        /// <summary>
        /// Switches between screens. 
        /// </summary>
        /// <param name="screenName">The name of a screen to switch to. 
        /// Given a name of a non existant screen results in switching to no screen.</param>
        protected void SwitchScreen(string screenName)
        {
            MenuManager.SwitchScreen(screenName);
        }

        protected override void AttachComponentAction(ObjectComponent component)
        {
            if (component is MenuItem)
            {
                if (controls == null)
                    controls = new List<MenuItem>();

                ((MenuItem)component)._SetIndex(controls.Count);

                controls.Add((MenuItem)component);
            }

            base.AttachComponentAction(component);
        }

        public override void RemoveComponent(ObjectComponent component)
        {
            if (controls != null)
                if (component != null && component is MenuItem)
                    controls.Remove(component as MenuItem);

            base.RemoveComponent(component);
        }

        public override void Dispose()
        {
            if (controls != null)
            {
                controls.Clear();
                controls = null;
            }

            base.Dispose();
        }
    }
}
