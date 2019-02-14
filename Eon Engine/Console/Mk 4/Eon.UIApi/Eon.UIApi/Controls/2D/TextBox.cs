/* Created 15/08/2015
 * Last Updated: 03/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine.Input;
using Eon.Maths.Helpers;
using Eon.Rendering2D;
using Eon.Rendering2D.Text;
using Eon.System.Interfaces;
using Eon.UIApi.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace Eon.UIApi.Controls._2D
{
    /// <summary>
    /// Defines a Textbox control.
    /// </summary>
    public class TextBox : MenuItem, IActive, I2DMenuItem
    {
        TextItem text;
        Sprite bg;

        string fontFilepath = "Eon/Fonts/Arial12";
        string displayText = "";

        Rectangle bounds;
        Vector2 textPosOffset = new Vector2(5, 5);

        Color fontColour;

        bool active = false;

        public OnActivatedEvent OnActivate { get; set; }
        public OnTextChangedEvent OnTextChanged;

        /// <summary>
        /// The font of the TextItem.
        /// </summary>
        public string FontFilepath
        {
            get { return fontFilepath; }
            set
            {
                if (text == null)
                    fontFilepath = value;
                else
                    text.Font = Common.ContentBuilder.Load<SpriteFont>(value);
            }
        }

        /// <summary>
        /// The colour of the text displayed.
        /// </summary>
        public Color FontColour
        {
            get { return fontColour; }
            set
            {
                if (text == null)
                    text.Colour = value;
                else
                    fontColour = value;
            }
        }

        /// <summary>
        /// The offset of the TextBox in 
        /// relation to the bounds of the TextBox.
        /// </summary>
        public Vector2 TextPositionOffset
        {
            get { return textPosOffset; }
            set
            {
                textPosOffset = value;

                if (text != null)
                    text.Position = bounds.Location.ToVector2() + value;
            }
        }

        /// <summary>
        /// Is the Control currently active.
        /// </summary>
        public bool Activated
        {
            get { return active; }
        }

        /// <summary>
        /// The text contained in the TextBox.
        /// </summary>
        public string Text
        {
            get { return displayText; }
            set
            {
                displayText = value;

                if (text != null)
                    text.ChangeText(value);
            }
        }

        /// <summary>
        /// The bounding area of the ImageButton.
        /// </summary>
        public Rectangle Bounds
        {
            get { return bounds; }
        }

        /// <summary>
        /// Creates a new TextBox.
        /// </summary>
        /// <param name="id">The ID of the TextBox.</param>
        /// <param name="menu">The Screen that the TextBox will be attached to.</param>
        /// <param name="position">The position of the TextBox.</param>
        /// <param name="size">The size of the TextBox.</param>
        public TextBox(string id, Screen menu,
            Vector2 position, Vector2 size)
            : base(id, menu)
        {
            bounds = new Rectangle(
                (int)position.X,
                (int)position.Y,
                (int)size.X, (int)size.Y);
        }

        protected override void Initialize()
        {
            bg = new Sprite(ID + "BG", 3, "Eon/Textures/Pixel", Color.LightGray, true, bounds);
            AttachComponent(bg);

            text = new TextItem(ID + "txt", 3, displayText, fontFilepath,
                bounds.Location.ToVector2() + textPosOffset, Color.Black, true);

            AttachComponent(text);

            base.Initialize();
        }

        protected override void Update()
        {
            if (active)
            {
                string[] input = InputManager.GetKeyValue();
                string t = "";

                string prevText = text.Text;

                if (input != null)
                    for (int i = 0; i < input.Length; i++)
                        if (input[i] == "Back")
                        {
                            if (t.Count() > 0)
                                t.Remove(t.Count() - 1);
                            else if (text.Text.Count() > 0)
                                text.ChangeText(text.Text.Remove(text.Text.Count() - 1));
                        }
                        else
                            t += input[i];

                text.ChangeText(text.Text + t);
                displayText = text.Text;

                if (OnTextChanged != null && text.Text != prevText)
                    OnTextChanged(ID, text.Text);
            }

            base.Update();
        }

        protected override bool IsInside(Vector2 position)
        {
            return EonMathsHelper.IsInsideOf(bounds, position);
        }

        public override void CheckClick()
        {
            if (OnActivate != null)
                OnActivate(this);

            active = true;

            base.CheckClick();
        }

        public void ToogleActive()
        {
            active = !active;
        }

        /// <summary>
        /// Moves the TextBox.
        /// </summary>
        /// <param name="movement">The amount to move the MenuItem by.</param>
        public void Move(Vector2 movement)
        {
            bounds.X += (int)movement.X;
            bounds.Y += (int)movement.Y;

            World.Position += new Vector3(movement, 0);
        }

        /// <summary>
        /// Sets the position of the TextBox.
        /// </summary>
        /// <param name="movement">The new position of the MenuItem.</param>
        public void SetPosition(Vector2 position)
        {
            bounds.X = (int)position.X;
            bounds.Y = (int)position.Y;

            World.Position = new Vector3(position, 0);
        }
    }
}
