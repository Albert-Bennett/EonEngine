/* Created: 18/09/2014
 * Last Updated: 24/11/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.UIApi.Helpers;
using Microsoft.Xna.Framework;

namespace Eon.UIApi.Screens
{
    /// <summary>
    /// Defines a type of menu which is used 
    /// in game.
    /// </summary>
    public abstract class PopUpMenu : Screen
    {
        bool active = false;
        bool ignoreCamera = false;
        string name;

        Rectangle bounds;

        internal OnPopUpCloseEvent OnClosed;

        /// <summary>
        /// The name of the PopUpMenu.
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// The bounding area of the PopUpMenu.
        /// </summary>
        protected Rectangle Bounds
        {
            get { return bounds; }
            set { bounds = value; }
        }

        /// <summary>
        /// Is the PopUpMenu active.
        /// </summary>
        protected bool Active
        {
            get { return active; }
            set { active = value; }
        }

        /// <summary>
        /// Used to define wheather or not the 
        /// PopUpMenu should ignore all kinds of Camera2D. 
        /// </summary>
        protected bool IgnoreCamera
        {
            get { return ignoreCamera; }
            set { ignoreCamera = value; }
        }

        /// <summary>
        /// Creates a new PopUpMenu.
        /// </summary>
        /// <param name="id">The ID of the PopUpMenu.</param>
        /// <param name="name">The name of the PopUpMenu.</param>
        /// <param name="bounds">The bounding area of the PopUpMenu.</param>
        public PopUpMenu(string id, string name, Rectangle bounds)
            : base(id)
        {
            this.name = name;
            this.bounds = bounds;

            MenuManager.AddPopUp(this);
        }

        protected override void Update()
        {
            CheckActive();

            if (active)
                base.Update();
        }

        void CheckActive()
        {
            bool temp = InputChecker.IsInside(bounds, ignoreCamera);

            if (!temp && active)
            {
                active = temp;

                OnInActiveAction();
            }
            else if (temp && !active)
            {
                active = temp;

                OnActiveAction();
            }
        }

        /// <summary>
        /// Used to define what happens when the PopUpMenu becomes active.
        /// </summary>
        protected virtual void OnActiveAction() { }

        /// <summary>
        /// Used to define what happens when the PopUpMenu becomes in-active.
        /// </summary>
        protected virtual void OnInActiveAction() { }

        /// <summary>
        /// Closes the PopUpMenu.
        /// </summary>
        public void Close()
        {
            if (OnClosed != null)
                OnClosed(ID);

            Destroy();
        }
    }
}
