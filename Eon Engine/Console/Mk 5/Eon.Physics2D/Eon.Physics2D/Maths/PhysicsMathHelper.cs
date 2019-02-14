/* Created: 18/09/2015
 * Last Updated: 18/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.Helpers;
using Microsoft.Xna.Framework;
using System;

namespace Eon.Physics2D.Maths
{
    /// <summary>
    /// Defines a helper class for maths in physics.
    /// </summary>
    public static class PhysicsMathHelper
    {
        /// <summary>
        /// Finds the angle of a given Vector2.
        /// </summary>
        /// <param name="position">The position to get the angle of.</param>
        /// <returns>The angle of the given Vector2.</returns>
        public static float GetAngle(Vector2 position)
        {
            return (float)Math.Atan2(position.Y, position.X);
        }

        /// <summary>
        /// Calculates a Vector2 that defines a point in a wave.
        /// </summary>
        /// <param name="angle">The angle of the object.</param>
        /// <param name="magnitude">The magnitude of the wave.</param>
        /// <returns>A point on a wave.</returns>
        public static Vector2 Polar(float angle, float magnitude)
        {
            return magnitude * new Vector2((float)Math.Cos(angle),
                (float)Math.Sin(angle));
        }
    }
}
