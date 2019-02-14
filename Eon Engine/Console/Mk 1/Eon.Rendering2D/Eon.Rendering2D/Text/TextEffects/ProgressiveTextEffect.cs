/* Created 09/08/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Helpers;
using Eon.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eon.Rendering2D.Text.TextEffects
{
    /// <summary>
    /// Used to define a Text Effect that creates letters in a ordered manner.
    /// </summary>
    public sealed class ProgressiveTextEffect : TextItem, IUpdate
    {
        TimeSpan changeRate;
        TimeSpan currentTime = TimeSpan.Zero;

        char[] textArray;
        List<char> currentText = new List<char>();
        int idx = 0;

        /// <summary>
        /// Used to signal the end of the ProgressiveTextEffect.
        /// </summary>
        public FinishedTextEffectEvent OnFinishedTextEffect = null;

        /// <summary>
        /// Used to signal when the ProgressiveTextEffect has generated a letter.
        /// </summary>
        public HasGeneratedEvent OnGenerated = null;

        /// <summary>
        /// Creates a new ProgressiveTextEffect.
        /// </summary>
        /// <param name="id">The unique ID to give the ProgressiveTextEffect.</param>
        /// <param name="drawLayer">The layer to draw the ProgressiveTextEffect on.</param>
        /// <param name="text">The text to use in the ProgressiveTextEffect.</param>
        /// <param name="fontFilepath">The filepath for the font to be used.</param>
        /// <param name="position">The position of the ProgressiveTextEffect.</param>
        /// <param name="colour">The colour to draw the ProgressiveTextEffect in.</param>
        /// <param name="rotation">The rotation of the ProgressiveTextEffect.</param>
        /// <param name="scale">The scale of the ProgressiveTextEffect.</param>
        /// <param name="origin">The point to rotate the ProgressiveTextEffect around.</param>
        /// <param name="postRender">Wheather or not the ProgressiveTextEffect should be post rendered.</param>
        /// <param name="changeRate">The rate at which letters are formed.</param>
        public ProgressiveTextEffect(string id, int drawLayer, string text, string fontFilepath,
            Vector2 position, Color colour, float rotation, float scale,
            Vector2 origin, bool postRender, TimeSpan changeRate)
            : base(id, drawLayer, text, fontFilepath, position, colour, rotation, scale, origin, postRender)
        {
            this.changeRate = changeRate;

            textArray = text.ToArray();
        }

        /// <summary>
        /// Creates a new ProgressiveTextEffect.
        /// </summary>
        /// <param name="id">The unique ID to give the ProgressiveTextEffect.</param>
        /// <param name="drawLayer">The layer to draw the ProgressiveTextEffect on.</param>
        /// <param name="text">The text to use in the ProgressiveTextEffect.</param>
        /// <param name="fontFilepath">The filepath for the font to be used.</param>
        /// <param name="position">The position of the ProgressiveTextEffect.</param>
        /// <param name="colour">The colour to draw the ProgressiveTextEffect in.</param>
        /// <param name="postRender">Wheather or not the ProgressiveTextEffect should be post rendered.</param>
        /// <param name="changeRate">The rate at which letters are formed.</param>
        public ProgressiveTextEffect(string id, int drawLayer, string text, string fontFilepath,
            Vector2 position, Color colour, bool postRender, TimeSpan changeRate)
            : this(id, drawLayer, text, fontFilepath, position, colour, 0, 1, Vector2.Zero, postRender, changeRate) { }

        protected override void Initialize()
        {
            text = "";

            base.Initialize();
        }

        public void _Update()
        {
            if (idx < textArray.Length)
            {
                currentTime += Common.ElapsedTimeDelta;

                if (currentTime >= changeRate)
                {
                    currentText.Add(textArray[idx]);
                    idx++;

                    currentTime = TimeSpan.Zero;

                    StringHelper.StringFromList(ref currentText, out text);

                    if (OnGenerated != null)
                        OnGenerated(ID);
                }
            }
            else
                if (OnFinishedTextEffect != null)
                    OnFinishedTextEffect(ID);
                else
                    Disable();
        }

        /// <summary>
        /// Used to change the string being used in the ProgressiveTextEffect.
        /// </summary>
        /// <param name="newText">The new string to be used in the ProgressiveTextEffect.</param>
        public override void ChangeText(string newText)
        {
            textArray = newText.ToArray();
            text = newText;

            Reset();
        }

        /// <summary>
        /// Resets the ProgressiveTextEffect.
        /// </summary>
        public void Reset()
        {
            idx = 0;
            currentTime = TimeSpan.Zero;
            currentText.Clear();
            text = "";
        }
    }
}
