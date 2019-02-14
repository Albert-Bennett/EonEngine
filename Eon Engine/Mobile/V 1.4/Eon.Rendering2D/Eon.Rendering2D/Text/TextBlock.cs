/* Created 09/09/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;
using Eon.Rendering2D.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Eon.Rendering2D.Text
{
    /// <summary>
    /// Defines a block of text. 
    /// </summary>
    public sealed class TextBlock : TextItem
    {
        List<string> lines = new List<string>();

        float spacing;
        float boundsX;

        /// <summary>
        /// Creates a new TextBlock.
        /// </summary>
        /// <param name="id">The unique ID to give the TextBlock.</param>
        /// <param name="drawLayer">The layer to draw the TextBlock on.</param>
        /// <param name="text">The text to use in the TextBlock.</param>
        /// <param name="fontFilepath">The filepath for the font to be used.</param>
        /// <param name="position">The position of the TextBlock.</param>
        /// <param name="colour">The colour to draw the TextBlock in.</param>
        /// <param name="scale">The scale of the TextBlock.</param>
        /// <param name="postRender">Wheather or not the TextBlock should be post rendered.</param>
        /// <param name="spacing">The amount of spacing between lines.</param>
        /// <param name="boundsX">The maximum size along the x-axis.</param>
        public TextBlock(string id, int drawLayer, string text, string fontFilepath,
            Vector2 position, Color colour, float scale,
            bool postRender, float spacing, float boundsX)
            : base(id, drawLayer, "", fontFilepath, position, colour, 0,
            scale, Vector2.Zero, postRender)
        {
            this.spacing = spacing;
            this.boundsX = boundsX;

            ChangeText(text);
        }

        /// <summary>
        /// Changes the text inside of the Textblock.
        /// </summary>
        /// <param name="newText">The new text inside of the TextBlock.</param>
        public override void ChangeText(string newText)
        {
            lines.Clear();

            string[] words = StringHelper.SplitRemove(newText, ' ');
            int idx = 0;

            float spaceSize = font.MeasureString(" ").X * scale;

            while (idx < words.Length)
            {
                float maxX = boundsX;
                string line = "";

                while (maxX > 0 && words.Length > idx)
                {
                    float lenght = (font.MeasureString(words[idx]).X * scale) + spaceSize;

                    maxX -= lenght;

                    if (maxX > 0)
                    {
                        line += words[idx] + " ";
                        idx++;
                    }
                }

                lines.Add(line);
            }
        }

        protected override void _Draw()
        {
            Vector2 initPos = pos;

            if (Owner != null)
            {
                initPos.X += Owner.World.Translation.X;
                initPos.Y += Owner.World.Translation.Y;
            }

            for (int i = 0; i < lines.Count; i++)
            {
                Common.Batch.DrawString(font, lines[i], initPos,
                    colour, 0, Vector2.Zero, scale, SpriteEffects.None, 1);

                initPos.Y += spacing;
            }
        }

        /// <summary>
        /// Gets the maximum size of the text.
        /// </summary>
        /// <returns>The maximum size of the text.</returns>
        public override Vector2 GetTextSize()
        {
            Vector2 vec = Vector2.Zero;

            for (int i = 0; i < lines.Count; i++)
            {
                Vector2 temp = font.MeasureString(lines[i]) * scale;

                if (EonMathHelper.IsGreaterThanOrEqualTo(temp, vec))
                    vec = temp;
            }

            return vec;
        }

        public override void ScreenResolutionChanged()
        {
            spacing = Common.ReCalibrateScale(spacing);
            boundsX = Common.ReCalibrateScale(boundsX);

            base.ScreenResolutionChanged();
        }
    }
}
