/* Created: 02/04/2014
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
    public class IntRange
    {
        int min;
        int max;

        /// <summary>
        /// Gets/ Sets the Minimum value of the Range. 
        /// </summary>
        public int Minimum
        {
            get { return min; }
            set { min = value; }
        }

        /// <summary>
        /// Sets the maximum value of the Range.
        /// </summary>
        public int Maximum
        {
            get { return max; }
            set { max = value; }
        }

        /// <summary>
        /// Creates a new IntRange.
        /// </summary>
        /// <param name="minimum">The minimum value in the IntRange.</param>
        /// <param name="maximum">The maximum value of the IntRange.</param>
        public IntRange(int minimum, int maximum)
        {
            min = minimum;
            max = maximum;
        }

        /// <summary>
        /// Creates a new IntRange between 0-1.
        /// </summary>
        public IntRange()
        {
            max = 1;
            min = 0;
        }

        /// <summary>
        /// A check to see if a number is within the IntRange.
        /// </summary>
        /// <param name="value">The number to be checked.</param>
        /// <returns>The result of the check.</returns>
        public bool IsWithinRange(int value)
        {
            if (min == max)
                return value == min;

            return value >= min & value <= max;
        }

        /// <summary>
        /// Gets a random number between the limits of the IntRange.
        /// </summary>
        /// <returns>The random number.</returns>
        public int GetRandomWithin()
        {
            return RandomHelper.GetRandom(min, max);
        }
    }
}
