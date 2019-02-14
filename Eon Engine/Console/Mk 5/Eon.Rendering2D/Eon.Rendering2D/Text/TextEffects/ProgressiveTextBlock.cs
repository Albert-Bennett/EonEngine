/* Created: 05/05/2015
 * Last Updated: 03/06/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;
using Eon.Maths.Helpers;
using Eon.Rendering2D.Drawing;
using Eon.System.Interfaces.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Eon.Rendering2D.Text.TextEffects
{
    /// <summary>
    /// Defines a TextBlock that generates text in intervals. 
    /// </summary>
    public sealed class ProgressiveTextBlock : TextItem
    {
        List<string> lines = new List<string>();
        string[] actualLines;

        TimeSpan currentTime = TimeSpan.Zero;
        TimeSpan timeOut = TimeSpan.Zero;

        float spacing;
        float boundsX;

        int lineIndex = 0;
        int letterIndex = 0;
        bool finished = false;

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
        /// <param name="boundsX">The maximum size along the x-axis.</param>
        /// <param name="milliseconds">Length of time it takes for letters to be generated.</param>
        public ProgressiveTextBlock(string id, int drawLayer, string text, string fontFilepath,
            Vector2 position, Color colour, float scale, bool postRender, float boundsX, int milliseconds)
            : base(id, drawLayer, text, fontFilepath, position, colour, 0,
            scale, Vector2.Zero, postRender)
        {
            this.spacing = LineSpacing();

            this.boundsX = boundsX;

            this.timeOut = TimeSpan.FromMilliseconds(milliseconds);

            ChangeText(text);
        }

        public override void ChangeText(string newText)
        {
            lines.Clear();

            string[] words = StringHelper.SplitRemove(newText, ' ');
            int idx = 0;

            float spaceSize = Font.MeasureString(" ").X * Scale;

            while (idx < words.Length)
            {
                float maxX = boundsX;
                string line = "";

                while (maxX > 0 && words.Length > idx)
                {
                    float lenght = (Font.MeasureString(words[idx]).X * Scale) + spaceSize;

                    maxX -= lenght;

                    if (maxX > 0)
                    {
                        line += words[idx] + " ";
                        idx++;
                    }
                }

                lines.Add(line);
            }

            Reset();
        }

        protected override void Update()
        {
            if (!finished)
            {
                currentTime += Common.ElapsedTimeDelta;

                if (currentTime >= timeOut)
                {
                    currentTime -= timeOut;

                    if (actualLines[lineIndex] == null)
                        actualLines[lineIndex] = "" + lines[lineIndex][0];
                    else
                        actualLines[lineIndex] += lines[lineIndex][letterIndex];

                    letterIndex++;

                    if (letterIndex == lines[lineIndex].Length)
                    {
                        lineIndex++;
                        letterIndex = 0;

                        if (lineIndex == lines.Count)
                            finished = true;
                    }
                }
            }
        }

        protected override void _Draw(DrawingStage stage)
        {
            Vector2 initPos = Position;

            if (Owner != null)
            {
                initPos.X += Owner.World.Position.X;
                initPos.Y += Owner.World.Position.Y;
            }

            for (int i = 0; i < actualLines.Length; i++)
            {
                if (actualLines[i] != null)
                    Common.Batch.DrawString(Font, actualLines[i], initPos,
                        Colour, 0, Vector2.Zero, Scale, SpriteEffects.None, 1);

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

            for (int i = 0; i < actualLines.Length; i++)
            {
                Vector2 temp = Font.MeasureString(actualLines[i]) * Scale;

                if (EonMathsHelper.IsGreaterThanOrEqualTo(temp, vec))
                    vec = temp;
            }

            return vec;
        }

        /// <summary>
        /// Resets the ProgressiveTextBlock.
        /// </summary>
        public void Reset()
        {
            actualLines = new string[lines.Count];
            currentTime = TimeSpan.Zero;

            lineIndex = 0;
            letterIndex = 0;
            finished = false;
        }

        public override void Disable()
        {
            if (!RenderDisabled)
                Reset();

            base.Disable();
        }
    }
}
