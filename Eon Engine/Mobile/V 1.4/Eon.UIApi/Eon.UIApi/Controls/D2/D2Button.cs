/* Created 04/09/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine;
using Eon.Helpers;
using Eon.UIApi.Screens;
using Microsoft.Xna.Framework;

namespace Eon.UIApi.Controls
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
        /// The bounding are for the D2Button.
        /// </summary>
        public Rectangle Bounds
        {
            get { return bounds; }
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
        /// Moves this D2Button.
        /// </summary>
        /// <param name="movementAmount">The amount to move this D2Button by.</param>
        public void Move(Vector2 movementAmount)
        {
            bounds.X += (int)movementAmount.X;
            bounds.Y += (int)movementAmount.Y;
        }

        /// <summary>
        /// A check to see if the D2Button has been touched.
        /// </summary>
        /// <param name="position">The position of the touch.</param>
        /// <returns>The result of the check.</returns>
        public override bool IsTouched(Vector2 position)
        {
            return RectangleHelper.IsInsideOf(bounds, position);
        }
    }
}
