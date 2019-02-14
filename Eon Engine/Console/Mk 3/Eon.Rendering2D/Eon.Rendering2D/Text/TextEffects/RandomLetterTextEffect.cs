/* Created: 09/08/2013
 * Last Updated: 15/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.Helpers;
using Eon.System.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Linq;

namespace Eon.Rendering2D.Text.TextEffects
{
    /// <summary>
    /// Used to define a Text Effect that builds a string randomly using a given string.
    /// </summary>
    public sealed class RandomLetterTextEffect : TextItem, IUpdate
    {
        /// <summary>
        /// Used to signal the end of the RandomLetterTextEffect.
        /// </summary>
        public FinishedTextEffectEvent OnFinishedTextEffect = null;

        /// <summary>
        /// Used to signal when the RandomLetterTextEffect has generated a letter.
        /// </summary>
        public HasGeneratedEvent OnGenerated = null;

        char[] textArray;
        char[] edditing;

        int count = 0;
        char temporyKey = '_';
        bool hasFinished = false;

        TimeSpan changeRate;
        TimeSpan currentTime = TimeSpan.Zero;

        public int Priority
        {
            get { return 0; }
        }

        /// <summary>
        /// Creates a new RandomLetterTextEffect.
        /// </summary>
        /// <param name="id">The unique ID to give the RandomLetterTextEffect.</param>
        /// <param name="drawLayer">The layer to draw the RandomLetterTextEffect on.</param>
        /// <param name="text">The text to use in the RandomLetterTextEffect.</param>
        /// <param name="fontFilepath">The filepath for the font to be used.</param>
        /// <param name="position">The position of the RandomLetterTextEffect.</param>
        /// <param name="colour">The colour to draw the RandomLetterTextEffect in.</param>
        /// <param name="postRender">Wheather or not the RandomLetterTextEffect should be post rendered.</param>
        /// <param name="changeRate">The rate at which letters are formed.</param>
        public RandomLetterTextEffect(string id, int drawLayer, string text, string fontFilepath,
            Vector2 position, Color colour, bool postRender, TimeSpan changeRate)
            : this(id, drawLayer, text, '_', fontFilepath, position, colour, 0, 1, Vector2.Zero, postRender, changeRate) { }

        /// <summary>
        /// Creates a new RandomLetterTextEffect.
        /// </summary>
        /// <param name="id">The unique ID to give the RandomLetterTextEffect.</param>
        /// <param name="drawLayer">The layer to draw the RandomLetterTextEffect on.</param>
        /// <param name="text">The text to use in the RandomLetterTextEffect.</param>
        /// <param name="temporyKey">The character to use to indicate 
        /// the end of the generated string.</param>
        /// <param name="fontFilepath">The filepath for the font to be used.</param>
        /// <param name="position">The position of the RandomLetterTextEffect.</param>
        /// <param name="colour">The colour to draw the RandomLetterTextEffect in.</param>
        /// <param name="rotation">The rotation of the RandomLetterTextEffect.</param>
        /// <param name="scale">The scale of the RandomLetterTextEffect.</param>
        /// <param name="origin">The point to rotate the RandomLetterTextEffect around.</param>
        /// <param name="postRender">Wheather or not the RandomLetterTextEffect should be post rendered.</param>
        /// <param name="changeRate">The rate at which letters are formed.</param>
        public RandomLetterTextEffect(string id, int drawLayer, string text, char temporyKey, string fontFilepath,
            Vector2 position, Color colour, float rotation, float scale,
            Vector2 origin, bool postRender, TimeSpan changeRate)
            : base(id, drawLayer, text, fontFilepath, position, colour, rotation, scale, origin, postRender)
        {
            this.changeRate = changeRate;
            this.temporyKey = temporyKey;

            textArray = text.ToArray();
            edditing = new char[textArray.Length];
        }

        protected override void Initialize()
        {
            Reset();
            GetText();

            base.Initialize();
        }

        void GetText()
        {
            text = "";

            for (int i = 0; i < edditing.Length; i++)
                text += edditing[i];
        }

        public void _Update()
        {
            if (!hasFinished)
            {
                currentTime += Common.ElapsedTimeDelta;

                if (currentTime >= changeRate)
                    Generate();
            }
            else
                if (OnFinishedTextEffect != null)
                    OnFinishedTextEffect(ID);
        }

        void Generate()
        {
            int idx = RandomHelper.GetRandom(0, textArray.Length);

            if (edditing[idx] == temporyKey)
            {
                edditing[idx] = textArray[idx];
                count++;

                GetText();

                if (OnGenerated != null)
                    OnGenerated(ID);
            }
            else if (count == textArray.Length)
                hasFinished = true;
            else
                Generate();

            currentTime = TimeSpan.Zero;
        }

        /// <summary>
        /// Change the string used to genereate the RandomLetterTextEffect.
        /// </summary>
        /// <param name="newText">The new string to use.</param>
        public override void ChangeText(string newText)
        {
            base.ChangeText(newText);
            textArray = text.ToArray();

            Reset();
        }

        /// <summary>
        /// Resets the RandomLetterTextEffect.
        /// </summary>
        public void Reset()
        {
            Enable();

            for (int i = 0; i < textArray.Length; i++)
                edditing[i] = temporyKey;

            hasFinished = false;
            count = 0;
            text = "";
        }
    }
}
