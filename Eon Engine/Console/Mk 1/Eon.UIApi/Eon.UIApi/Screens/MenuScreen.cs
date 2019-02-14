/* Created 04/09/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Engine;
using Eon.EngineComponents;
using Eon.UIAPI.Controls;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eon.UIAPI.Screens
{
    /// <summary>
    /// A delegate used to define what happends 
    /// when the selected index of controls has changed. 
    /// </summary>
    /// <param name="index">The newly selected control index.</param>
    public delegate void SelectedIndexChangedEvent(int index);

    /// <summary>
    /// Defines a basic 2D / 3D screen.
    /// </summary>
    public abstract class MenuScreen : GameObject
    {
        List<MenuItem> controls;
        List<MenuItem> disabledControls;

        GameStates screenPrecidence = GameStates.MainMenu;

        protected int selectedIndex = 0;
        protected bool ignoreMouse = false;

        bool created = false;

        /// <summary>
        /// The index of the currently selected control.
        /// </summary>
        internal int Selectedindex
        {
            get { return selectedIndex; }
        }

        /// <summary>
        /// Wheather or not to ingnore the mouse when selecting controls.
        /// </summary>
        public bool IgnoreMouse
        {
            get
            {
                bool equal = InputManager.MouseDelta.Equals(Vector2.Zero);

                if (!equal && ignoreMouse)
                    return true;
                else
                    return false;
            }
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

            _TransitionOff();
            created = true;
        }

        protected override void Update()
        {
            ignoreMouse = false;
            HandleInput();

            base.Update();

            if (disabledControls != null && disabledControls.Count > 0)
            {
                for (int i = 0; i < disabledControls.Count; i++)
                {
                    disabledControls[i].Enable();

                    if (controls == null)
                        controls = new List<MenuItem>();

                    controls.Add(disabledControls[i]);
                }

                disabledControls.Clear();
                disabledControls = null;
            }
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
            Disable();

            if (created)
            {
                Dispose();

                created = false;
            }

            ResetScreen();
        }

        /// <summary>
        /// Activate this MenuScreen.
        /// </summary>
        internal void _TransitionOn()
        {
            if (!created)
                ReInitialize();

            created = true;

            TransitionOn();

            Enable();
        }

        /// <summary>
        /// Switches between screens. 
        /// </summary>
        /// <param name="screenName">The name of a screen to switch to. 
        /// Given a name of a non existant screen results in switching to no screen.</param>
        protected void SwitchScreen(string screenName)
        {
            MenuManager.SwitchScreen(screenName);
        }

        /// <summary>
        /// Used to define what happends when this MenuScreen becomes active.
        /// </summary>
        protected virtual void TransitionOn() { }

        /// <summary>
        /// Resets this MenuScreen.
        /// </summary>
        protected virtual void ResetScreen()
        {
            selectedIndex = 0;
        }

        /// <summary>
        /// Destroys this MenuScreen.
        /// </summary>
        public override void Destroy()
        {
            MenuManager.Remove(this);

            base.Destroy();
        }

        protected override void AttachComponentAction(ObjectComponent component)
        {
            if (component is MenuItem)
            {
                if (disabledControls == null)
                    disabledControls = new List<MenuItem>();

                ((MenuItem)component)._SetIndex(disabledControls.Count);
                ((MenuItem)component).Disable();

                disabledControls.Add((MenuItem)component);
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

            if (disabledControls != null)
            {
                disabledControls.Clear();
                disabledControls = null;
            }

            base.Dispose();
        }
    }
}
