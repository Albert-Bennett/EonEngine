/* Created: 03/09/2014
 * Last Updated: 03/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.Helpers;

namespace Eon.Maths
{
    /// <summary>
    /// Used to define a collection that can hold two 
    /// values and check if a value for an object lies within it.
    /// </summary>
    public class FloatRange
    {
        float min;
        float max;

        /// <summary>
        /// Gets/ Sets the Minimum value of the Range. 
        /// </summary>
        public float Minimum
        {
            get { return min; }
            set { min = value; }
        }

        /// <summary>
        /// Sets the maximum value of the Range.
        /// </summary>
        public float Maximum
        {
            get { return max; }
            set { max = value; }
        }

        /// <summary>
        /// Creates a new FloatRange.
        /// </summary>
        /// <param name="minimum">The minimum value in the FloatRange.</param>
        /// <param name="maximum">The maximum value of the FloatRange.</param>
        public FloatRange(float minimum, float maximum)
        {
            min = minimum;
            max = maximum;
        }

        /// <summary>
        /// Creates a new FloatRange between 0.0f - 1.0f.
        /// </summary>
        public FloatRange()
        {
            max = 1;
            min = 0;
        }

        /// <summary>
        /// A check to see if a number is within the FloatRange.
        /// </summary>
        /// <param name="value">The number to be checked.</param>
        /// <returns>The result of the check.</returns>
        public bool IsWithinRange(float value)
        {
            if (min == max)
                return value == min;

            return value >= min & value <= max;
        }

        /// <summary>
        /// Gets a random number between the limits of the FloatRange.
        /// </summary>
        /// <returns>The random number.</returns>
        public float GetRandomWithin()
        {
            return RandomHelper.GetRandom(min, max);
        }
    }
}
