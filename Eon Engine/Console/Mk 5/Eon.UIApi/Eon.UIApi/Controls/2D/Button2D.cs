/* Created: 04/09/2013
 * Last Updated: 01/10/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine;
using Eon.Rendering2D;
using Eon.Rendering2D.Cameras;
using Eon.Rendering2D.Text;
using Eon.UIApi.Screens;
using Microsoft.Xna.Framework;

namespace Eon.UIApi.Controls._2D
{
    /// <summary>
    /// Defines a basic 2D control.
    /// </summary>
    public class Button2D : MenuItem, I2DMenuItem
    {
        Rectangle bounds;
        Sprite texture;

        Color hoveredOverColour = Color.White;
        Color colour = Color.White;
        Color fontColour = Color.Black;
        Color unSelectableColour = Color.White; 

        TextItem text;

        int drawLayer = 0;
        bool postRender = false;
        bool changeText = false;

        string textureFilepath;
        string displayText = "";
        string hoveredSnd = "";
        string fontFilepath = "Eon/Fonts/Arial12";

        /// <summary>
        /// Change the colour of the text (if any) when hovered over.
        /// </summary>
        public bool ChangeTextOnHovered
        {
            get { return changeText; }
            set { changeText = value; }
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
        /// The colour to change the Button2D to when hovered over.
        /// </summary>
        public Color HoveredOverColour
        {
            get { return hoveredOverColour; }
            set { hoveredOverColour = value; }
        }

        /// <summary>
        /// The colour of the background of the Button2D.
        /// </summary>
        public Color Colour
        {
            get { return colour; }
            set
            {
                colour = value;

                if (texture != null)
                    texture.Colour = value;
            }
        }

        /// <summary>
        /// The colour of the Button2D when un selectable.
        /// </summary>
        public Color UnSelectableColour
        {
            get { return unSelectableColour; }
            set
            {
                unSelectableColour = value;

                if (texture != null && !IsSelectable)
                    texture.Colour = value;
            }
        }

        /// <summary>
        /// The colour of the text of the Button2D.
        /// </summary>
        public Color FontColour
        {
            get { return fontColour; }
            set
            {
                fontColour = value;

                if (text != null)
                    text.Colour = value;
            }
        }

        /// <summary>
        /// The sound to play when hovered over.
        /// </summary>
        public string HoveredOverSound
        {
            get { return hoveredSnd; }
            set { hoveredSnd = value; }
        }

        /// <summary>
        /// The text to be displayed on the Button2D.
        /// </summary>
        public string Text
        {
            get
            {
                if (text != null)
                    return text.Text;

                return "";
            }
            set
            {
                if (text != null)
                    text.ChangeText(value);
                else
                {
                    text = new TextItem(ID + "DisplayText", drawLayer, value,
                        fontFilepath, bounds.Center.ToVector2(), Color.White, postRender);

                    text.Center(ScreenPositions.LocalCenter);

                    AttachComponent(text);
                }

                text.Translate();
            }
        }

        /// <summary>
        /// Creates a new D2Button.
        /// </summary>
        /// <param name="id">The id for the D2Button.</param>
        /// <param name="bounds">The bounds of the button.</param>
        public Button2D(string id, Screen menu, Rectangle bounds, string textureFilepath)
            : base(id, menu)
        {
            this.bounds = bounds;
            this.textureFilepath = textureFilepath;
        }

        /// <summary>
        /// Creates a new D2Button.
        /// </summary>
        /// <param name="id">The id for the D2Button.</param>
        /// <param name="bounds">The bounds of the button.</param>
        /// <param name="drawLayer">The layer to draw the Button2D on.</param>
        /// <param name="fontFilepath">The filepath for the font to be used.</param>
        /// <param name="menu">The menu screen that this is attached to.</param>
        /// <param name="postRender">Render the Button2D after everything else?</param>
        /// <param name="text">The text to be displayed on the Button2D.</param>
        /// <param name="textureFilepath">The filepath for the background texture.</param>
        public Button2D(string id, Screen menu, Rectangle bounds, string textureFilepath,
            string fontFilepath, string text, int drawLayer, bool postRender)
            : base(id, menu)
        {
            this.bounds = bounds;
            this.textureFilepath = textureFilepath;

            this.drawLayer = drawLayer;
            this.postRender = postRender;
            this.displayText = text;
            this.fontFilepath = fontFilepath;
        }

        protected override void Initialize()
        {
            Color c = colour;

            if (!IsSelectable)
                c = unSelectableColour;
            else if (HoveredOver)
                c = hoveredOverColour;

            texture = new Sprite(ID + "Spr", drawLayer,
                textureFilepath, c, postRender, bounds);

            AttachComponent(texture);

            if (displayText != "")
            {
                text = new TextItem(ID + "DisplayText", drawLayer, displayText,
                    fontFilepath, bounds.Center.ToVector2(), fontColour, postRender);

                AttachComponent(text);

                text.Translate();

                text.Center(ScreenPositions.LocalCenter);
            }

            base.Initialize();
        }

        /// <summary>
        /// A check to see if the mouse is currently inside of this MenuItem.
        /// </summary>
        /// <returns>The result of the check.</returns>
        protected override bool IsInside(Vector2 position)
        {
            if (CameraManager2D.CurrentCamera != null && !IgnoreCamera)
                position = Vector2.Transform(position, Matrix.Invert(CameraManager2D.CurrentCamera.ViewMatrix));

            return bounds.Contains((int)position.X, (int)position.Y);
        }

        protected override void OnHoveredOver(bool hovered)
        {
            if (hovered)
            {
                if (!PrevHoveredOver)
                {
                    if (hoveredSnd != "")
                        AudioManager.Play(hoveredSnd);

                    if (changeText)
                        text.Colour = hoveredOverColour;

                    texture.Colour = hoveredOverColour;
                }
            }
            else if (PrevHoveredOver)
            {
                if (changeText)
                    text.Colour = fontColour;

                texture.Colour = colour;
            }

            base.OnHoveredOver(hovered);
        }

        /// <summary>
        /// Moves the Button2D.
        /// </summary>
        /// <param name="movement">The amount to move the MenuItem by.</param>
        public void Move(Vector2 movement)
        {
            bounds.X += (int)movement.X;
            bounds.Y += (int)movement.Y;

            World.Position += new Vector3(movement, 0);
        }

        /// <summary>
        /// Sets the position of the Button2D.
        /// </summary>
        /// <param name="movement">The new position of the MenuItem.</param>
        public void SetPosition(Vector2 position)
        {
            bounds.X = (int)position.X;
            bounds.Y = (int)position.Y;

            World.Position = new Vector3(position, 0);
        }

        public override void UnSelectable()
        {
            if (texture != null)
                texture.Colour = unSelectableColour;

            base.UnSelectable();
        }

        public override void Selectable()
        {
            if (texture != null)
                texture.Colour = colour;

            base.Selectable();
        }
    }
}
