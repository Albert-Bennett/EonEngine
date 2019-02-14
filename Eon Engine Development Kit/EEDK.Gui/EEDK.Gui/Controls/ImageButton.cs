/* Created: 26/01/2015
 * Last Updated: 14/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine.Input;
using Eon.UIApi.Screens;
using Microsoft.Xna.Framework;

namespace EEDK.Gui.Controls
{
    /// <summary>
    /// Used to define a button that contains an image only.
    /// </summary>
    public sealed class ImageButton : Eon.UIApi.Controls._2D.Button2D
    {
        Color selected = Color.LightBlue;
        Color notSelected = Color.White;

        /// <summary>
        /// The colour of the background of
        /// the control when selected.
        /// </summary>
        public Color Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        /// <summary>
        /// The colour of the background of
        /// the control when not selected.
        /// </summary>
        public Color NotSelected
        {
            get { return notSelected; }
            set { notSelected = value; }
        }

        public ImageButton(string id, MenuScreen owner, Rectangle bounds, string filepath)
            : base(id, owner, bounds, filepath) { }

        protected override void Update()
        {
            if (IsInside(InputManager.MousePos) &&
                InputManager.IsMouseButtonStroked(MouseButtons.Left))
                CheckClick();

            base.Update();
        }

        protected override void OnHoveredOver(bool hovered)
        {
            if (hovered)
                Texture.Colour = selected;
            else
                Texture.Colour = notSelected;

            base.OnHoveredOver(hovered);
        }
    }
}
