/* Created 14/04/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Physics2D.Maths
{
    /// <summary>
    /// Used to define a minimum traversal vector.
    /// </summary>
    public struct MTV
    {
        float distance;

        Vector2 direction;

        /// <summary>
        /// The amopunt of distance to move.
        /// </summary>
        public float Distance
        {
            get { return distance; }
        }

        /// <summary>
        /// The direction of movement.
        /// </summary>
        public Vector2 Direction
        {
            get { return direction; }
        }

        /// <summary>
        /// Creates a new MTV.
        /// </summary>
        /// <param name="direction">The direction of movement.</param>
        /// <param name="distance">The amount of distance to be moved.</param>
        public MTV(Vector2 direction, float distance)
        {
            this.direction = direction;
            this.distance = distance;
        }
    }
}
