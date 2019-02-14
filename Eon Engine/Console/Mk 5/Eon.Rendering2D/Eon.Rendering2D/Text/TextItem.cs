/* Created: 09/08/2013
 * Last Updated: 24/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Engine.Languages;
using Eon.Rendering2D.Drawing;
using Eon.System.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Eon.Rendering2D.Text
{
    /// <summary>
    /// Used to define a piece of text.
    /// </summary>
    public class TextItem : ObjectComponent, IDrawItem, ITextureQualityChanged
    {
        string text = "";
        string actualText = "";

        protected bool postRender;

        Vector2 pos;
        Color colour;

        SpriteFont font;

        float rot;
        Vector2 origin;
        float scale = 1;
        int drawLayer;

        bool renderDisabled = false;

        /// <summary>
        /// The layer that the TextItem will be drawn on.
        /// </summary>
        public int DrawLayer
        {
            get { return drawLayer; }
        }

        /// <summary>
        /// Render the object even if Disabled.
        /// </summary>
        public bool RenderDisabled
        {
            get { return renderDisabled; }
            set { renderDisabled = value; }
        }

        /// <summary>
        /// The font used to render the TextItem.
        /// </summary>
        public SpriteFont Font
        {
            get { return font; }
            set { font = value; }
        }

        /// <summary>
        /// The text that is displayed by this TextItem.
        /// </summary>
        public string Text
        {
            get { return text; }
            protected set { text = value; }
        }

        /// <summary>
        /// The colour of the TextItem.
        /// </summary>
        public Color Colour
        {
            get { return colour; }
            set { colour = value; }
        }

        /// <summary>
        /// The position of the TextItem.
        /// </summary>
        public Vector2 Position
        {
            get { return pos; }
            set { pos = value; }
        }

        /// <summary>
        /// The scale of the TextItem.
        /// </summary>
        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        /// <summary>
        /// The rotation of the TextItem.
        /// </summary>
        public float Rotation
        {
            get { return rot; }
            set { rot = value; }
        }

        /// <summary>
        /// The point where the TextItem rotates about.
        /// </summary>
        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        /// <summary>
        /// The true text value.
        /// </summary>
        public string ActualText
        {
            get { return actualText; }
        }

        /// <summary>
        /// Creates a new TextItem.
        /// </summary>
        /// <param name="id">The unique ID to give the TextItem.</param>
        /// <param name="drawLayer">The layer to draw the TextItem on.</param>
        /// <param name="text">The text to use in the TextItem.</param>
        /// <param name="fontFilepath">The filepath for the font to be used.</param>
        /// <param name="position">The position of the TextItem.</param>
        /// <param name="colour">The colour to draw the TextItem in.</param>
        /// <param name="postRender">Wheather or not the TextItem should be post rendered.</param>
        public TextItem(string id, int drawLayer, string text, string fontFilepath,
            Vector2 position, Color colour, bool postRender)
            : this(id, drawLayer, text, fontFilepath, position, colour, 0, 1, Vector2.Zero, postRender) { }

        /// <summary>
        /// Creates a new TextItem.
        /// </summary>
        /// <param name="id">The unique ID to give the TextItem.</param>
        /// <param name="drawLayer">The layer to draw the TextItem on.</param>
        /// <param name="text">The text to use in the TextItem.</param>
        /// <param name="fontFilepath">The filepath for the font to be used.</param>
        /// <param name="position">The position of the TextItem.</param>
        /// <param name="colour">The colour to draw the TextItem in.</param>
        /// <param name="rotation">The rotation of the TextItem.</param>
        /// <param name="scale">The scale of the TextItem.</param>
        /// <param name="origin">The point to rotate the TextItem around.</param>
        /// <param name="postRender">Wheather or not the TextItem should be post rendered.</param>
        public TextItem(string id, int drawLayer, string text, string fontFilepath,
            Vector2 position, Color colour, float rotation, float scale,
            Vector2 origin, bool postRender)
            : base(id)
        {
            try
            {
                font = Common.ContentBuilder.Load<SpriteFont>(fontFilepath);
            }
            catch (Exception)
            {
                throw new ArgumentNullException("The font " + fontFilepath + " wasn't found.");
            }

            pos = position;
            rot = rotation;
            this.scale = scale;

            this.drawLayer = drawLayer;

            this.text = text;
            actualText = text;

            this.origin = origin;
            this.colour = colour;

            this.postRender = postRender;

            if (!postRender)
                DrawingManager.Add(this);
            else
                PostRenderManager.Add(this);
        }

        /// <summary>
        /// Translates the text in the TextItem to another language.
        /// </summary>
        public void Translate()
        {
            if (DictionaryManager.Languages.Count > 0)
                ChangeText(DictionaryManager.FindTranslation(text));
        }

        /// <summary>
        /// Translates the text in the TextItem to another language.
        /// </summary>
        /// <param name="language">The language to translate text to.</param>
        public void Translate(string language)
        {
            if (DictionaryManager.Languages.Count > 0)
                ChangeText(DictionaryManager.FindTranslation(language, text));
        }

        /// <summary>
        /// Changes the text of the TextItem.
        /// </summary>
        /// <param name="newText">The new text.</param>
        public virtual void ChangeText(string newText)
        {
            text = newText;
            actualText = newText;
        }

        /// <summary>
        /// Moves the TextItem.
        /// </summary>
        /// <param name="movementAmount">The amount to move the TextItem by.</param>
        public void Move(Vector2 movementAmount)
        {
            pos += movementAmount;
        }

        /// <summary>
        /// Rotates the TextItem. 
        /// </summary>
        /// <param name="rotationAmount">The amount to rotate the TextItem by.</param>
        public void Rotate(float rotationAmount)
        {
            rot += rotationAmount;
        }

        /// <summary>
        /// Finds the center of the TextItem.
        /// </summary>
        /// <returns>The center of the TextItem.</returns>
        public virtual Vector2 FindTextCenter()
        {
            Vector2 size = font.MeasureString(text);
            size *= scale;

            return size / 2;
        }

        public void Draw(DrawingStage stage)
        {
            _Draw(stage);
        }

        protected virtual void _Draw(DrawingStage stage)
        {
            Vector2 p = pos;

            if (Owner != null)
            {
                p.X += Owner.World.Position.X;
                p.Y += Owner.World.Position.Y;
            }

            Common.Batch.DrawString(font, text, p, colour, rot,
                origin, scale, SpriteEffects.None, 1);
        }

        /// <summary>
        /// The actual siz eof the un-modified text.
        /// </summary>
        /// <returns>The size of the actual text.</returns>
        public Vector2 GetActualTextSize()
        {
            return font.MeasureString(actualText);
        }

        /// <summary>
        /// Gets the size of the TextItem.
        /// </summary>
        /// <returns>The size of the TextItem.</returns>
        public virtual Vector2 GetTextSize()
        {
            return font.MeasureString(text) * scale;
        }

        /// <summary>
        /// Gets the size of a string. 
        /// </summary>
        /// <param name="text">The text to get the size of.</param>
        /// <returns>The size of the inputted string.</returns>
        public Vector2 GetTextSize(string text)
        {
            return font.MeasureString(text);
        }

        /// <summary>
        /// Measures the actual size of the text and not the current.
        /// </summary>
        /// <returns>The size of the actual text.</returns>
        public virtual Vector2 MeasureActualSize()
        {
            return Font.MeasureString(actualText) * Scale;
        }

        /// <summary>
        /// The spacing between lines in the font.
        /// </summary>
        /// <returns>Font line spacing.</returns>
        public int LineSpacing()
        {
            return (int)((float)font.LineSpacing * scale);
        }

        /// <summary>
        /// Centers the TextItem.
        /// </summary>
        /// <param name="position">The way in which the
        /// TextItem is to be centered.</param>
        public void Center(ScreenPositions position)
        {
            Vector2 size = GetTextSize();

            _Center(position, size);
        }

        /// <summary>
        /// Centers the TextItem using it's actual text.
        /// </summary>
        /// <param name="position">The way in which the
        /// TextItem is to be centered.</param>
        public void CenterActual(ScreenPositions position)
        {
            Vector2 size = GetActualTextSize();

            _Center(position, size);
        }

        void _Center(ScreenPositions position, Vector2 size)
        {
            switch (position)
            {
                case ScreenPositions.LocalCenter:
                    {
                        pos.X -= size.X / 2;
                        pos.Y -= size.Y / 2;
                    }
                    break;

                case ScreenPositions.CenterX:
                    {
                        pos.X -= size.X / 2;
                    }
                    break;

                case ScreenPositions.CenterY:
                    {
                        pos.Y -= size.Y / 2;
                    }
                    break;

                case ScreenPositions.ScreenCenter:
                    {
                        pos.X = (Common.TextureQuality.X / 2) - (size.X / 2);
                        pos.Y = (Common.TextureQuality.Y / 2) - (size.Y / 2);
                    }
                    break;

                case ScreenPositions.ScreenCenterX:
                    {
                        pos.X = (Common.TextureQuality.X / 2) - (size.X / 2);
                    }
                    break;

                case ScreenPositions.ScreenCenterY:
                    {
                        pos.Y = (Common.TextureQuality.Y / 2) - (size.Y / 2);
                    }
                    break;
            }
        }

        public void TextureQualityChanged()
        {
            scale = Common.GetReScaled(scale);
            pos = Common.GetReScaled(pos);
        }

        protected override void _Destroy()
        {
            if (postRender)
                PostRenderManager.Remove(this);
            else
                DrawingManager.Remove(this);

            base._Destroy();
        }
    }
}
