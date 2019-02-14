/* Created 03/09/2015
 * Last Updated: 03/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.States;
using Eon.UIApi.Controls._2D;
using Microsoft.Xna.Framework;

namespace Eon.UIApi.Controls.Containers
{
    /// <summary>
    /// Defines a DropMenu.
    /// </summary>
    public sealed class DropMenu : Container
    {
        Vector2 position;

        /// <summary>
        /// Creates a new DropMenu.
        /// </summary>
        /// <param name="id">The ID of the DropMenu.</param>
        /// <param name="presidence">The Presidence of the DropMenu.</param>
        public DropMenu(string id, GameStates presidence,
            Vector2 position)
            : base(id, presidence)
        {
            this.position = position;
        }

        /// <summary>
        /// Adds a new MenuItem to the DropMenu.
        /// If the MenuItem is I2DMenuItem.
        /// </summary>
        /// <param name="control">The MenuItem to be added.</param>
        public override void AddControl(MenuItem control)
        {
            if (control is I2DMenuItem)
                base.AddControl(control);
        }

        /// <summary>
        /// Sets the position of the DropMenu.
        /// </summary>
        /// <param name="position">The new position.</param>
        public void SetPosition(Vector2 position)
        {
            this.position = position;

            for (int i = 0; i < Controls.Count; i++)
                ((I2DMenuItem)Controls[i]).SetPosition(position);
        }

        /// <summary>
        /// Moves the DropMenu.
        /// </summary>
        /// <param name="movement">The amount ot move the DropMenu.</param>
        public void Move(Vector2 movement)
        {
            this.position += movement;

            for (int i = 0; i < Controls.Count; i++)
                ((I2DMenuItem)Controls[i]).Move(position);
        }

        void Hide()
        {
            for (int i = 0; i < Controls.Count; i++)
                Controls[i].Disable();
        }

        void Show()
        {
            for (int i = 0; i < Controls.Count; i++)
                Controls[i].Enable();
        }
    }
}
