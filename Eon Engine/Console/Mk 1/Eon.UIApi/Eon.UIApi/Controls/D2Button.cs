/* Created 04/09/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Engine;
using Eon.UIAPI.Screens;
using Microsoft.Xna.Framework;

namespace Eon.UIAPI.Controls
{
    /// <summary>
    /// Defines a basic 2D control.
    /// </summary>
    public class D2Button : MenuItem
    {
        protected Rectangle bounds;

        /// <summary>
        /// The position of the D2Button.
        /// </summary>
        public Vector2 Position
        {
            get { return new Vector2(bounds.X, bounds.Y); }
        }

        /// <summary>
        /// Creates a new D2Button.
        /// </summary>
        /// <param name="id">The id for the D2Button.</param>
        /// <param name="bounds">The bounds of the button.</param>
        public D2Button(string id, Rectangle bounds)
            : base(id)
        {
            this.bounds = bounds;
        }

        /// <summary>
        /// A check to see if the mouse is currently inside of this MenuItem.
        /// </summary>
        /// <returns>The result of the check.</returns>
        protected override bool IsMouseInside()
        {
            if (((MenuScreen)Owner).IgnoreMouse)
                return false;
            else
                return bounds.Contains((int)InputManager.MousePos.X, (int)InputManager.MousePos.Y);
        }

        /// <summary>
        /// Moves this D2Button.
        /// </summary>
        /// <param name="movementAmount">The amount to move this D2Button by.</param>
        public void Move(Vector2 movementAmount)
        {
            bounds.X += (int)movementAmount.X;
            bounds.Y += (int)movementAmount.Y;
        }
    }
}
