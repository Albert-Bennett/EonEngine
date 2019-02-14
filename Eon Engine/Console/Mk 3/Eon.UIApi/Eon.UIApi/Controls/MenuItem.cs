/* Created: 04/09/2013
 * Last Updated: 18/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine;
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

        public OnClickedEvent OnClicked;

        /// <summary>
        /// The MenuScreen that this MenuItem is attached to.
        /// </summary>
        public Screen Owner
        {
            get { return owner; }
        }

        internal int Idx
        {
            get { return idx; }
            set { idx = value; }
        }

        /// <summary>
        /// Was the MenuItem previously hovered over.
        /// </summary>
        protected bool PrevHoveredOver
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
                if (Enabled)
                    if (IsInside())
                        return true;
                    else
                        return owner.SelectedIndex == idx;
                else
                    return false;
            }
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
        }

        public virtual void CheckClick()
        {
            if (hovered)
                if (OnClicked != null)
                    OnClicked(ID);
        }

        protected override void Update()
        {
            OnHoveredOver(HoveredOver);

            base.Update();
        }

        protected virtual void OnHoveredOver(bool hovered)
        {
            if (hovered == true)
                owner.ChangeSelectedIndex(idx);

            prevHoveredOver = this.hovered;
            this.hovered = hovered;
        }

        bool IsInside()
        {
            bool inside = false;

            if (InputManager.IsMouseConnected)
                inside = IsInside(InputManager.MousePos);

            if (!inside)
                if (InputManager.TouchPad != null)
                    inside = IsInside(InputManager.TouchPad.TouchLoc);

            return inside;
        }

        protected abstract bool IsInside(Vector2 position);
    }
}
