/* Created 02/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;
using Microsoft.Xna.Framework;

namespace Eon.Physics2D.Math.Helpers
{
    /// <summary>
    /// Defines a helper class for objects colliding with rectangles.
    /// </summary>
    public static class CollisionShapeHelper
    {
        /// <summary>
        /// Used to determine if two rectangles collide.
        /// </summary>
        /// <param name="rectangle1">Rectangle 1.</param>
        /// <param name="rectangle2">Rectangle 2.</param>
        /// <param name="pointOfContact">The point of contact between the two shapes.</param>
        /// <returns>The result of the check.</returns>
        public static CollisionType Collides(Rectangle rectangle1, Rectangle rectangle2, out Vector2 pointOfContact)
        {
            pointOfContact = EonMathHelper.Midpoint(new Vector2(rectangle1.Center.X, rectangle1.Center.Y),
                new Vector2(rectangle2.Center.X, rectangle2.Center.Y));

            if (rectangle1.Right < rectangle2.X || rectangle1.X > rectangle2.Right ||
                rectangle1.Bottom < rectangle2.Y || rectangle1.Y > rectangle2.Bottom)
                return CollisionType.None;

            else if (rectangle1.X <= rectangle2.X && rectangle1.Right >= rectangle2.Right &&
                rectangle1.Y < rectangle2.Y && rectangle1.Bottom >= rectangle2.Bottom)
                return CollisionType.Full;

            return CollisionType.Partial;
        }

        /// <summary>
        /// A check to see if a rectangle contains a vector2.
        /// </summary>
        /// <param name="rectangle">The rectangle to check.</param>
        /// <param name="position">The position to check for.</param>
        /// <returns>The result of the check.</returns>
        public static bool Contains(Rectangle rectangle, Vector2 position)
        {
            return position.X >= rectangle.X && position.X <= rectangle.Right
             && position.Y >= rectangle.Y && position.Y < rectangle.Bottom;
        }

        /// <summary>
        /// Used to check if two BoundingCircles collides. 
        /// </summary>
        /// <param name="otherCircle">The BoundingCircle to check aginst.</param>
        /// <param name="circle">The BoundingCircle to collide with.</param>
        /// <param name="pointOfContact">The point of contact between the two shapes.</param>
        /// <returns>The result of the check.</returns>
        public static CollisionType Collides(BoundingCircle circle, BoundingCircle otherCircle, out Vector2 pointOfContact)
        {
            float radialDistance = circle.Radius + otherCircle.Radius;

            float centerDistance;
            centerDistance = Vector2.Distance(circle.Center, otherCircle.Center);

            pointOfContact = EonMathHelper.Midpoint(circle.Center, otherCircle.Center);

            if (centerDistance > radialDistance)
                return CollisionType.None;
            else if (radialDistance - centerDistance >= 0)
                return CollisionType.Full;

            return CollisionType.Partial;
        }

        /// <summary>
        /// Used to check if a rectangle collides with the BoundingCircle. 
        /// </summary>
        /// <param name="rectangle">The rectangle to check aginst.</param>
        /// <param name="circle">The BoundingCircle to collide with.</param>
        /// <param name="pointOfContact">The point of contact between the two shapes.</param>
        /// <returns>The result of the check.</returns>
        public static CollisionType Collides(Rectangle rectangle, BoundingCircle circle, out Vector2 pointOfContact)
        {
            Vector2 rectCenter = new Vector2(rectangle.Center.X, rectangle.Center.Y);
            float dist = Vector2.Distance(circle.Center, rectCenter);

            pointOfContact = EonMathHelper.Midpoint(circle.Center, new Vector2(rectangle.Center.X, rectangle.Center.Y));

            if (dist - (circle.Radius + rectangle.Width) <= 0 && dist - (circle.Radius + rectangle.Height) <= 0)
                return CollisionType.Full;
            else if (dist > circle.Radius + rectangle.Height && dist > circle.Radius + rectangle.Width)
                return CollisionType.None;

            return CollisionType.Partial;
        }
    }
}
