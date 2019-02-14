﻿/* Created 09/08/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering2D.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Eon.Rendering2D.Text
{
    /// <summary>
    /// Used to define a piece of text.
    /// </summary>
    public class TextItem : ObjectComponent, IDrawItem
    {
        protected string text;
        protected bool postRender;

        protected Vector2 pos;
        protected Color colour;
        protected SpriteFont font;

        float rot;
        Vector2 origin;
        protected float scale = 1;
        int drawLayer;

        /// <summary>
        /// The layer that the TextItem will be drawn on.
        /// </summary>
        public int DrawLayer
        {
            get { return drawLayer; }
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
                font = Common.ContentManager.Load<SpriteFont>(fontFilepath);
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
            this.origin = origin;
            this.colour = colour;

            this.postRender = postRender;

            if (!postRender)
                DrawingManager.Add(this);
            else
                PostRenderManager.Add(this);
        }

        /// <summary>
        /// Sets the rotation of the TextItem.
        /// </summary>
        /// <param name="rotation">The new rotation.</param>
        public void SetRotation(float rotation)
        {
            rot = rotation;
        }

        /// <summary>
        /// Sets the scale of the TextItem.
        /// </summary>
        /// <param name="scale">The new scale of the TextItem.</param>
        public void SetScale(float scale)
        {
            this.scale = scale;
        }

        /// <summary>
        /// Changes the text of the TextItem.
        /// </summary>
        /// <param name="newText">The new text.</param>
        public virtual void ChangeText(string newText)
        {
            text = newText;
        }

        /// <summary>
        /// Sets the position of the TextItem.
        /// </summary>
        /// <param name="position">The new position of the TextItem.</param>
        public void SetPosition(Vector2 position)
        {
            pos = position;
        }

        /// <summary>
        /// Moves the TextItem.
        /// </summary>
        /// <param name="movementAmount">The amount to move the TextItem by.</param>
        public void Translate(Vector2 movementAmount)
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
        /// Sets teh rotation point of the TextItem. 
        /// </summary>
        /// <param name="origin">The new origin to use.</param>
        public void SetOrigin(Vector2 origin)
        {
            this.origin = origin;
        }

        /// <summary>
        /// Sets the colour of the TextItem. 
        /// </summary>
        /// <param name="colour">The new draw colour.</param>
        public void SetColour(Color colour)
        {
            this.colour = colour;
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
                p.X += Owner.World.Translation.X;
                p.Y += Owner.World.Translation.Y;
            }

            Common.Batch.DrawString(font, text, p, colour, rot,
                origin, scale, SpriteEffects.None, 1);
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
        /// Centers the TextItem.
        /// </summary>
        /// <param name="position">The way in which the
        /// TextItem is to be centered.</param>
        public void Center(ScreenPositions position)
        {
            Vector2 size = GetTextSize();

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
                        pos.X = (Common.DefaultScreenResolution.X / 2) - (size.X / 2);
                        pos.Y = (Common.DefaultScreenResolution.Y / 2) - (size.Y / 2);
                    }
                    break;

                case ScreenPositions.ScreenCenterX:
                    {
                        pos.X = (Common.DefaultScreenResolution.X / 2) - (size.X / 2);
                    }
                    break;

                case ScreenPositions.ScreenCenterY:
                    {
                        pos.Y = (Common.DefaultScreenResolution.Y / 2) - (size.Y / 2);
                    }
                    break;
            }
        }

        public override void Destroy(bool remove)
        {
            if (postRender)
                PostRenderManager.Remove(this);
            else
                DrawingManager.Remove(this);

            base.Destroy(remove);
        }
    }
}
