/* Created: 26/01/2015
 * Last Updated: 14/08/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine.Input;
using Eon.Rendering2D.Text;
using Eon.UIApi.Screens;
using Microsoft.Xna.Framework;

namespace EEDK.Gui.Controls
{
    /// <summary>
    /// Used to define a button.
    /// </summary>
    public sealed class ControlButton : Eon.UIApi.Controls._2D.Button2D
    {
        TextItem textItem;

        Color selected = Color.White;
        Color notSelected = Color.Transparent;

        /// <summary>
        /// The text to be displayed on the ControlButton.
        /// </summary>
        public string Text
        {
            get { return textItem.Text; }
            set { textItem.ChangeText(value); }
        }

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

        /// <summary>
        /// The colour of the text.
        /// </summary>
        public Color TextColour
        {
            get { return textItem.Colour; }
            set { textItem.Colour = value; }
        }

        /// <summary>
        /// Creates a new ControlButton.
        /// </summary>
        /// <param name="id">The id of the ControlButton./param>
        /// <param name="owner">The owner of the ControlButton.</param>
        /// <param name="position">The position of the ControlButton.</param>
        /// <param name="padding">The amount of padding around the button.</param>
        public ControlButton(string id, MenuScreen owner,
            Vector2 position, Vector2 padding, bool flushRight)
            : base(id, owner, new Rectangle(), "Eon\\Textures\\Pixel")
        {
            textItem = new TextItem(id + "txt", 1, id, "GUI\\Verdana12pt",
                position + padding, Color.Black, true);

            Vector2 size = textItem.GetTextSize();
            size += padding * 2;

            if (flushRight)
            {
                position.X -= size.X;
                position.Y += padding.Y;

                textItem.Position = position;

                Bounds = new Rectangle((int)(position.X - padding.X),
                    (int)(position.Y - padding.Y),
                    (int)size.X, (int)size.Y);
            }
            else
                Bounds = new Rectangle(
                    (int)position.X,
                    (int)position.Y,
                    (int)size.X, (int)size.Y);
        }

        protected override void Initialize()
        {
            AttachComponent(textItem);

            base.Initialize();
        }

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
