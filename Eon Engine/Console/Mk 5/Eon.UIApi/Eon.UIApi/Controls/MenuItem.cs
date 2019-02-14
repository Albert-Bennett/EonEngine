/* Created: 04/09/2013
 * Last Updated: 01/10/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine.Input;
using Eon.Rendering2D.Cameras;
using Eon.UIApi.Screens;
using Microsoft.Xna.Framework;

namespace Eon.UIApi.Controls
{
    /// <summary>
    /// Defines an absrtract class for which all menu controls will inherit from. 
    /// </summary>
    public abstract class MenuItem : GameObject
    {
        int idx;

        Screen owner;

        bool hovered = false;
        bool prevHoveredOver = false;
        bool selectable = true;
        bool ignoreCamera = false;

        public OnClickedEvent OnClicked;
        public OnSelectedEvent OnSelected;

        /// <summary>
        /// The MenuScreen that this MenuItem is attached to.
        /// </summary>
        public Screen Owner
        {
            get { return owner; }
        }

        /// <summary>
        /// The index of the control in the Screen.
        /// </summary>
        public int ControlIndex
        {
            get { return idx; }
            internal set { idx = value; }
        }

        /// <summary>
        /// Wheater of not the any kind of camera related 
        /// mouse calculations should be ignored.
        /// </summary>
        public bool IgnoreCamera
        {
            get { return ignoreCamera; }
            set { ignoreCamera = value; }
        }

        /// <summary>
        /// Was the MenuItem previously hovered over.
        /// </summary>
        public bool PrevHoveredOver
        {
            get { return prevHoveredOver; }
        }

        /// <summary>
        /// Is this MenuItem hovered over.
        /// </summary>
        public bool HoveredOver
        {
            get
            {
                if (selectable)
                    if (!MenuManager.IgnoreMouse)
                        return IsInside();
                    else
                        return owner.SelectedIndex == idx;

                return false;
            }
        }

        /// <summary>
        /// Is the MenuItem selectable.
        /// </summary>
        public bool IsSelectable
        {
            get { return selectable; }
        }

        /// <summary>
        /// Creates a new MenuItem.
        /// </summary>
        /// <param name="id">The ID of the MenuItem.</param>
        /// <param name="menu">The Screen that this MenuItem is attached to.</param>
        public MenuItem(string id, Screen menu)
            : base(id)
        {
            owner = menu;
            owner.Attach(this);

            Presidence = menu.Presidence;
        }

        public virtual void CheckClick()
        {
            if (hovered)
                if (OnClicked != null)
                    OnClicked(ID);
        }

        /// <summary>
        /// Makes the MenuItem unable to be selected.
        /// </summary>
        public virtual void UnSelectable()
        {
            if (selectable)
                selectable = false;
        }

        /// <summary>
        /// Makes the MenuItem selectable.
        /// </summary>
        public virtual void Selectable()
        {
            if (!selectable)
                selectable = true;
        }

        protected override void Update()
        {
            _OnHoveredOver(HoveredOver);

            base.Update();
        }

        void _OnHoveredOver(bool hovered)
        {
            if (IsSelectable)
                OnHoveredOver(hovered);
        }

        protected virtual void OnHoveredOver(bool hovered)
        {
            if (hovered == true)
                owner.ChangeSelectedIndex(idx);
            else if (prevHoveredOver && !hovered && !MenuManager.IgnoreMouse)
                owner.ChangeSelectedIndex(-1);

            prevHoveredOver = this.hovered;
            this.hovered = hovered;

            if (OnSelected != null)
                if (prevHoveredOver && !hovered)
                    OnSelected(true, ControlIndex);
                else if (!prevHoveredOver && hovered)
                    OnSelected(false, ControlIndex);
        }

        bool IsInside()
        {
            bool inside = false;

            if (InputManager.IsMouseConnected)
                if (!ignoreCamera && CameraManager2D.CurrentCamera != null)
                    inside = IsInside(Vector2.Transform(InputManager.MousePos,
                        Matrix.Invert(CameraManager2D.CurrentCamera.ViewMatrix)));
                else
                    inside = IsInside(InputManager.MousePos);
            else if (InputManager.TouchPad != null)
                if (!ignoreCamera && CameraManager2D.CurrentCamera != null)
                    inside = IsInside(Vector2.Transform(InputManager.TouchPad.TouchLoc,
                        Matrix.Invert(CameraManager2D.CurrentCamera.ViewMatrix)));
                else
                    inside = IsInside(InputManager.TouchPad.TouchLoc);
            else if (MenuManager.Cursor != null)
                if (!ignoreCamera && CameraManager2D.CurrentCamera != null)
                    inside = IsInside(Vector2.Transform(MenuManager.Cursor.Position,
                        Matrix.Invert(CameraManager2D.CurrentCamera.ViewMatrix)));
                else
                    inside = IsInside(MenuManager.Cursor.Position);

            return inside;
        }

        protected abstract bool IsInside(Vector2 position);
    }
}
