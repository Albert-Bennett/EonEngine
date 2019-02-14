/* Created 22/04/2015
 * Last Updated: 03/10/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.Helpers;
using Eon.Rendering2D;
using Eon.Rendering2D.Cameras;
using Eon.Rendering2D.Text;
using Eon.UIApi.Screens;
using Microsoft.Xna.Framework;

namespace Eon.UIApi.Controls._2D
{
    /// <summary>
    /// Defines a slider control that can be used to mediate a value.
    /// </summary>
    public sealed class Slider : MenuItem, I2DMenuItem
    {
        int moveableLength = 0;

        Vector2 startPos;
        Rectangle sliderBounds;
        Rectangle backgroundBounds;

        Sprite background;
        Sprite sliderCursor;
        TextItem text;
        Color textColour = Color.White;

        string backgroundFilepath;
        int drawLayer;
        bool postRender;

        string sliderFilepath;
        string displayText;
        float textOffsetY;
        string fontFilepath;

        float currentValue = 0.0f;

        /// <summary>
        /// The current value of the Slider.
        /// </summary>
        public float CurrentValue
        {
            get
            {
                float dist = (sliderCursor.Offset.X +
                    (sliderBounds.Width / 2)) - startPos.X - backgroundBounds.X;

                return MathHelper.Clamp(dist / (moveableLength -
                    sliderBounds.Width), 0.0f, 1.0f);
            }
            set
            {
                currentValue = value;

                if (sliderCursor != null)
                    CalculatePos(value);
            }
        }

        /// <summary>
        /// The colour of the text.
        /// </summary>
        public Color TextColour
        {
            get { return textColour; }
            set
            {
                if (text != null)
                    text.Colour = value;

                textColour = value;
            }
        }

        /// <summary>
        /// The bounding area of the Slider.
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(backgroundBounds.X, sliderBounds.Y,
                    backgroundBounds.Width, sliderBounds.Height);
            }
        }

        /// <summary>
        /// Creates a new Slider control.
        /// </summary>
        /// <param name="id">The ID of the Slider control.</param>
        /// <param name="menu">The Screen that this is attached to.</param>
        /// <param name="sliderSize">The size of the slider.</param>
        /// <param name="backgroundBounds">The bounds of the background.</param>
        /// <param name="startPos">The starting position of the slider 
        /// (relative to the background bounds).</param>
        /// <param name="textOffsetY">Y offset of the text.</param>
        /// <param name="fontFilepath">Font filepath.</param>
        /// <param name="displayText">Text to be displayed with the Slider.</param>
        /// <param name="sliderFilepath">Slider texture filepath.</param>
        /// <param name="backgroundFilepath">Background texture filepath.</param>
        /// <param name="moveableLength">The distance that the slider can move.</param>
        /// <param name="drawLayer">The layer to draw the Slider on.</param>
        /// <param name="postRender">Render the Slider after everything else?</param>
        public Slider(string id, Screen menu, Vector2 sliderSize,
            Rectangle backgroundBounds, Vector2 startPos, float textOffsetY,
            string fontFilepath, string displayText, string sliderFilepath,
            string backgroundFilepath, int moveableLength, int drawLayer, bool postRender)
            : base(id, menu)
        {
            this.backgroundBounds = backgroundBounds;

            this.startPos = startPos;

            sliderBounds = new Rectangle()
            {
                Width = (int)sliderSize.X,
                Height = (int)sliderSize.Y,
                X = (int)(startPos.X + backgroundBounds.X),
                Y = (int)((startPos.Y + backgroundBounds.Y) - (sliderSize.Y / 2))
            };

            this.moveableLength = moveableLength;

            this.backgroundFilepath = backgroundFilepath;
            this.drawLayer = drawLayer;
            this.postRender = postRender;

            this.sliderFilepath = sliderFilepath;
            this.displayText = displayText;
            this.textOffsetY = textOffsetY;
            this.fontFilepath = fontFilepath;
        }

        protected override void Initialize()
        {
            background = new Sprite(ID + "BG", drawLayer,
                backgroundFilepath, Color.White, postRender, backgroundBounds);

            AttachComponent(background);

            sliderCursor = new Sprite(ID + "Slider", drawLayer,
                sliderFilepath, Color.White, postRender, sliderBounds);

            AttachComponent(sliderCursor);

            CalculatePos(currentValue);

            if (displayText != "")
            {
                Vector2 pos = new Vector2()
                {
                    X = backgroundBounds.Center.X,
                    Y = backgroundBounds.Y + textOffsetY
                };

                text = new TextItem(ID + "DisplayText", drawLayer, displayText,
                    fontFilepath, pos, textColour, postRender);

                text.Translate();
                text.Center(ScreenPositions.LocalCenter);

                AttachComponent(text);
            }

            base.Initialize();
        }

        void CalculatePos(float value)
        {
            Vector2 offset = sliderCursor.Offset;

            offset.X = (backgroundBounds.X + startPos.X) +
                (moveableLength * EonMathsHelper.Clamp(value, 0.0f, 1.0f)) -
                sliderBounds.Width;

            sliderCursor.Offset = offset;
        }

        /// <summary>
        /// A check to see if the mouse is currently inside of this Slider.
        /// </summary>
        /// <returns>The result of the check.</returns>
        protected override bool IsInside(Vector2 position)
        {
            if (CameraManager2D.CurrentCamera != null && !IgnoreCamera)
                position = Vector2.Transform(position, Matrix.Invert(CameraManager2D.CurrentCamera.ViewMatrix));

            return sliderCursor.Bounds.Contains(position.X, (int)position.Y);
        }

        /// <summary>
        /// Moves the Slider.
        /// </summary>
        /// <param name="deltaX">Delta X.</param>
        public void Move(float deltaX)
        {
            if (HoveredOver)
            {
                Vector2 offset = sliderCursor.Offset;
                offset.X += deltaX;

                if (offset.X < startPos.X + backgroundBounds.X)
                    offset.X = startPos.X + backgroundBounds.X;

                else if (offset.X > backgroundBounds.X + startPos.X + moveableLength - (sliderBounds.Width))
                    offset.X = backgroundBounds.X + startPos.X + moveableLength - (sliderBounds.Width);

                sliderCursor.Offset = offset;
            }
        }

        /// <summary>
        /// Moves the Slider.
        /// </summary>
        /// <param name="movement">The amount to move the MenuItem by.</param>
        public void Move(Vector2 movement)
        {
            startPos += movement;

            sliderBounds.X += (int)movement.X;
            sliderBounds.Y += (int)movement.Y;

            backgroundBounds.X += (int)movement.X;
            backgroundBounds.Y += (int)movement.Y;

            World.Position += new Vector3(movement, 0);
        }

        /// <summary>
        /// Sets the position of the Slider.
        /// </summary>
        /// <param name="movement">The new position of the MenuItem.</param>
        public void SetPosition(Vector2 position)
        {
            startPos = position;

            Vector2 dif = backgroundBounds.Location.ToVector2() -
                sliderBounds.Location.ToVector2();

            backgroundBounds.X = (int)position.X;
            backgroundBounds.Y = (int)position.Y;

            sliderBounds.X = (int)(position.X + dif.X);
            sliderBounds.Y = (int)(position.Y + dif.Y);

            World.Position += new Vector3(position, 0);
        }
    }
}
