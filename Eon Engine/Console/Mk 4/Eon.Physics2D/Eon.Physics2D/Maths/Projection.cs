/* Created: 14/04/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;
using Eon.Maths.Helpers;

namespace Eon.Physics2D.Maths
{
    /// <summary>
    /// Used to define the projection of a vertex onto an axis.
    /// </summary>
    public struct Projection
    {
        float min;
        float max;

        /// <summary>
        /// Minimum value.
        /// </summary>
        public float Min
        {
            get { return min; }
        }

        /// <summary>
        /// Maximum value.
        /// </summary>
        public float Max
        {
            get { return max; }
        }

        /// <summary>
        /// Creates a new Projection.
        /// </summary>
        /// <param name="min">Minumum value.</param>
        /// <param name="max">Maximum value.</param>
        public Projection(float min, float max)
        {
            this.min = min;
            this.max = max;
        }

        /// <summary>
        /// A check to see if this Projection contains another.
        /// </summary>
        /// <param name="projection">The projection to be checked aginst.</param>
        /// <returns>The result of the check.</returns>
        public bool Contains(Projection projection)
        {
            return min >= projection.min && max > projection.max;
        }

        /// <summary>
        /// Used to find the overlap between two projections. 
        /// </summary>
        /// <param name="projection">The projection to check aginst.</param>
        /// <returns>The amount of overlap.</returns>
        public float GetOverlap(Projection projection)
        {
            if (Overlaps(projection))
                return EonMathsHelper.Min(max, projection.max) -
                    EonMathsHelper.Max(min, projection.min);
            else
                return 0;
        }

        /// <summary>
        /// Used to check to see if an overlap occours.
        /// </summary>
        /// <param name="projection">The project to check against.</param>
        /// <returns>The result of the check.</returns>
        public bool Overlaps(Projection projection)
        {
            return projection.min < max || projection.max < min;
        }
    }
}
