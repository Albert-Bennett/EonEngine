/* Created 04/09/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine;
using Eon.Rendering2D;
using Eon.UIApi.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.UIApi.Controls
{
    /// <summary>
    /// Defines a basic 2D control.
    /// </summary>
    public class Button2D : MenuItem
    {
        protected Rectangle bounds;
        protected Sprite texture;

        string textureFilepath;

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
        public Button2D(string id, Rectangle bounds, string textureFilepath)
            : base(id)
        {
            this.bounds = bounds;
            this.textureFilepath = textureFilepath;
        }

        protected override void Initialize()
        {
            texture = new Sprite(ID + "Spr", 0, textureFilepath, Color.White, true,
                new Vector2(bounds.X, bounds.Y), new Vector2(bounds.Width, bounds.Height));

            Owner.AttachComponent(texture);

            base.Initialize();
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
