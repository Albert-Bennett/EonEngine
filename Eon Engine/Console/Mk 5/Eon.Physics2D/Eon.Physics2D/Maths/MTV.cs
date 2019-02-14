/* Created: 14/04/2014
 * Last Updated: 26/09/2015
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
            set { distance = value; }
        }

        /// <summary>
        /// The direction of movement.
        /// </summary>
        public Vector2 Direction
        {
            get { return direction; }
            set
            {
                direction = value;
                direction.Normalize();
            }
        }

        /// <summary>
        /// Creates a new MTV.
        /// </summary>
        /// <param name="direction">The direction of movement.</param>
        /// <param name="distance">The amount of distance to be moved.</param>
        public MTV(Vector2 direction, float distance)
        {
            this.direction = direction;
            direction.Normalize();

            this.distance = distance;
        }

        /// <summary>
        /// Initial MTV.
        /// </summary>
        public static MTV Initial
        {
            get
            {
                return new MTV()
                    {
                        direction = Vector2.Zero,
                        distance = 0
                    };
            }
        }

        public static bool operator ==(MTV value1, MTV value2)
        {
            return value1.Distance == value2.Distance &&
                value1.Direction == value2.Direction;
        }

        public static bool operator !=(MTV value1, MTV value2)
        {
            return value1.Distance != value2.Distance ||
                value1.Direction != value2.Direction;
        }

        public override bool Equals(object obj)
        {
            if (obj is MTV)
            {
                MTV other = (MTV)obj;

                return other == this;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return distance.GetHashCode() ^
                direction.X.GetHashCode() ^
                direction.Y.GetHashCode();
        }
    }
}
