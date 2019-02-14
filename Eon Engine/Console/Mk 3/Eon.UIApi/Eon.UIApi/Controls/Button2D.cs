/* Created: 04/09/2013
 * Last Updated: 14/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine;
using Eon.Rendering2D;
using Eon.Rendering2D.Cameras;
using Eon.UIApi.Screens;
using Microsoft.Xna.Framework;

namespace Eon.UIApi.Controls
{
    /// <summary>
    /// Defines a basic 2D control.
    /// </summary>
    public class Button2D : MenuItem
    {
        Rectangle bounds;
        Sprite texture;

        string textureFilepath;

        bool ignoreCamera = false;

        /// <summary>
        /// Wheather of not the any kind of camera related 
        /// mouse calculations should be ignored.
        /// </summary>
        public bool IgnoreCamera
        {
            get { return ignoreCamera; }
            set { ignoreCamera = value; }
        }

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
            protected set { bounds = value; }
        }

        /// <summary>
        /// The image of the button.
        /// </summary>
        public Sprite Texture
        {
            get { return texture; }
            protected set { texture = value; }
        }

        /// <summary>
        /// Creates a new D2Button.
        /// </summary>
        /// <param name="id">The id for the D2Button.</param>
        /// <param name="bounds">The bounds of the button.</param>
        public Button2D(string id, MenuScreen menu, Rectangle bounds, string textureFilepath)
            : base(id, menu)
        {
            this.bounds = bounds;
            this.textureFilepath = textureFilepath;
        }

        protected override void Initialize()
        {
            texture = new Sprite(ID + "Spr", 0,
                textureFilepath, Color.White, true, bounds);

            AttachComponent(texture);

            base.Initialize();
        }

        /// <summary>
        /// A check to see if the mouse is currently inside of this MenuItem.
        /// </summary>
        /// <returns>The result of the check.</returns>
        protected override bool IsInside(Vector2 position)
        {
            if (CameraManager2D.CurrentCamera != null && !ignoreCamera)
                position = Vector2.Transform(position, Matrix.Invert(CameraManager2D.CurrentCamera.ViewMatrix));

            return bounds.Contains((int)position.X, (int)position.Y);
        }
    }
}
