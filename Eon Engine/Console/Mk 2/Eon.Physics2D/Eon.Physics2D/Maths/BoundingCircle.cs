/* Created 02/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;
using Eon.Maths.Helpers;
using Microsoft.Xna.Framework;

namespace Eon.Physics2D.Maths
{
    /// <summary>
    /// Defines a circle that can be used for 2D collision detection.
    /// </summary>
    public sealed class BoundingCircle
    {
        Vector2 center;
        float radius;

        /// <summary>
        /// The center of the BoundingCircle.
        /// </summary>
        public Vector2 Center
        {
            get { return center; }
            set { center = value; }
        }

        /// <summary>
        /// The radius of the BoundingCircle.
        /// </summary>
        public float Radius
        {
            get { return radius; }
            set { radius = value; }
        }

        /// <summary>
        /// Returns the area of the BoundingCircle. 
        /// </summary>
        public float Area
        {
            get { return EonMathsHelper.Pi * (radius * radius); }
        }

        /// <summary>
        /// Creates a new BoundingCircle.
        /// </summary>
        /// <param name="center">The center of the BoundingCircle.</param>
        /// <param name="radius">he radius of the BoundingCircle.</param>
        public BoundingCircle(Vector2 center, float radius)
        {
            this.center = center;
            this.radius = radius;
        }

        /// <summary>
        /// Generates a rectangle from the BoundingCircle.
        /// </summary>
        /// <param name="circle">The BoundingCircle to generate the rectangle from.</param>
        /// <returns>The generated rectangle.</returns>
        public static Rectangle GenerateRectangle(BoundingCircle circle)
        {
            Rectangle rect = new Rectangle(0, 0,
                (int)circle.Radius * 2, (int)circle.Radius * 2);

            rect.X = (int)(circle.Center.X - circle.Radius);
            rect.Y = (int)(circle.Center.Y + circle.Radius);

            return rect;
        }

        /// <summary>
        /// A check to see if a Vector2 lies 
        /// within the bounds of the BoundingCircle.
        /// </summary>
        /// <param name="position">The position to check.</param>
        /// <returns>The result of the check.</returns>
        public bool Contains(Vector2 position)
        {
            return Vector2.Distance(center, position) < radius;
        }

        /// <summary>
        /// A check to see if another BoundingCircle 
        /// is contained in this BoundingCircle.
        /// </summary>
        /// <param name="circle">The BoundingCircle to check aginst.</param>
        /// <returns>The result of the check.</returns>
        public bool Contains(BoundingCircle circle)
        {
            return Vector2.Distance(center, circle.center) < radius - circle.radius;
        }

        /// <summary>
        /// Genetaes a BoundingCircle from a rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle to generate the </param>
        /// <returns>The generated BoundingCircle.</returns>
        public static BoundingCircle Generate(Rectangle rectangle)
        {
            Vector2 center = new Vector2(rectangle.Center.X, rectangle.Center.Y);
            float radius = (rectangle.Height + rectangle.Width) / 2;

            return new BoundingCircle(center, radius);
        }

        public override string ToString()
        {
            return "BoundingCircle: Radius = " + radius +
                ", Center = " + center.X + ", " + center.Y;
        }
    }
}
