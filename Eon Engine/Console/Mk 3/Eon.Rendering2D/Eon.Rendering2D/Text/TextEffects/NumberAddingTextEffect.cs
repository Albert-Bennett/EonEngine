/* Created: 04/10/2013
 * Last Updated: 15/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Interfaces;
using Microsoft.Xna.Framework;
using System;

namespace Eon.Rendering2D.Text.TextEffects
{
    /// <summary>
    /// A Text Effect that is used to add multiple numbers together.
    /// </summary>
    public sealed class NumberAddingTextEffect : TextItem, IUpdate
    {
        /// <summary>
        /// Used to signal the end of the NumberAddingTextEffect.
        /// </summary>
        public FinishedTextEffectEvent OnFinishedTextEffect = null;

        /// <summary>
        /// Used to signal when the NumberAddingTextEffect has generated a number.
        /// </summary>
        public HasGeneratedEvent OnGenerated = null;

        TimeSpan changeRate;
        TimeSpan currentTime = TimeSpan.Zero;

        int maxNumber;
        int minNumber;
        int rate;
        int currentNumber;

        bool adding = true;

        public int Priority
        {
            get { return 0; }
        }

        /// <summary>
        /// Creates a new NumberAddingTextEffect.
        /// </summary>
        /// <param name="id">The unique ID to give the NumberAddingTextEffect.</param>
        /// <param name="drawLayer">The layer to draw the NumberAddingTextEffect on.</param>
        /// <param name="maxNumber">The maximum number to be used.</param>
        /// <param name="fontFilepath">The filepath for the font to be used.</param>
        /// <param name="position">The position of the NumberAddingTextEffect.</param>
        /// <param name="colour">The colour to draw the NumberAddingTextEffect in.</param>
        /// <param name="postRender">Wheather or not the NumberAddingTextEffect should be post rendered.</param>
        /// <param name="changeRate">The rate at which letters are formed.</param>
        public NumberAddingTextEffect(string id, int drawLayer, int maxNumber, string fontFilepath,
            Vector2 position, Color colour, bool postRender, TimeSpan changeRate)
            : this(id, drawLayer, 0, maxNumber, 1, fontFilepath, position, colour, 0, 1, Vector2.Zero, postRender, changeRate) { }

        /// <summary>
        /// Creates a new NumberAddingTextEffect.
        /// </summary>
        /// <param name="id">The unique ID to give the NumberAddingTextEffect.</param>
        /// <param name="drawLayer">The layer to draw the NumberAddingTextEffect on.</param>
        /// <param name="startNumber">The number to start the Text Effect at.</param>
        /// <param name="maxNumber">The maximum number to be used.</param>
        /// <param name="rate">The amount to be added per generation.</param>
        /// <param name="fontFilepath">The filepath for the font to be used.</param>
        /// <param name="position">The position of the NumberAddingTextEffect.</param>
        /// <param name="colour">The colour to draw the NumberAddingTextEffect in.</param>
        /// <param name="rotation">The rotation of the NumberAddingTextEffect.</param>
        /// <param name="scale">The scale of the NumberAddingTextEffect.</param>
        /// <param name="origin">The point to rotate the NumberAddingTextEffect around.</param>
        /// <param name="postRender">Wheather or not the NumberAddingTextEffect should be post rendered.</param>
        /// <param name="changeRate">The rate at which letters are formed.</param>
        public NumberAddingTextEffect(string id, int drawLayer, int startNumber, int maxNumber, int rate,
            string fontFilepath, Vector2 position, Color colour, float rotation, float scale,
            Vector2 origin, bool postRender, TimeSpan changeRate)
            : base(id, drawLayer, startNumber.ToString(), fontFilepath, position, colour, rotation, scale, origin, postRender)
        {
            this.changeRate = changeRate;

            this.rate = rate;
            currentNumber = minNumber = startNumber;
            this.maxNumber = maxNumber;

            if (minNumber > maxNumber)
                adding = false;
        }

        public void _Update()
        {
            if (currentNumber < maxNumber && adding)
                Generate();
            else if (currentNumber > maxNumber && !adding)
                Generate();
            else
                if (OnFinishedTextEffect != null)
                    OnFinishedTextEffect(ID);
        }

        void Generate()
        {
            currentTime += Common.ElapsedTimeDelta;

            if (currentTime >= changeRate)
            {
                if (adding)
                {
                    currentNumber += rate;

                    if (currentNumber > maxNumber)
                        currentNumber = maxNumber;
                }
                else
                {
                    currentNumber -= rate;

                    if (currentNumber <= maxNumber)
                        currentNumber = maxNumber;
                }

                currentTime = TimeSpan.Zero;
                text = currentNumber.ToString();

                if (OnGenerated != null)
                    OnGenerated(ID);
            }
        }

        /// <summary>
        /// Used to change a number in the Text Effect by using a string.
        /// </summary>
        /// <param name="number">the number as a string to be used.</param>
        public override void ChangeText(string number)
        {
            int temp = 0;

            if (int.TryParse(number, out temp))
                if (temp >= minNumber)
                    maxNumber = temp;
                else
                    minNumber = temp;
        }

        /// <summary>
        /// Changes the minimum number used in the NumberAddingTextEffect.
        /// </summary>
        /// <param name="minNumber">The new minimum nuber to be used.</param>
        public void ChangeMinNumber(int minNumber)
        {
            if (minNumber > this.minNumber)
                maxNumber = minNumber;
            else
                this.minNumber = minNumber;
        }

        /// <summary>
        /// Resets the NumberAddingTextEffect.
        /// </summary>
        public void Reset()
        {
            currentTime = TimeSpan.Zero;
            currentNumber = minNumber;
        }
    }
}
