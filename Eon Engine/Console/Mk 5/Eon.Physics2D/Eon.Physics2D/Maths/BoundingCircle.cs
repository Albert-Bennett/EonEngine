/* Created: 02/10/2013
 * Last Updated: 26/09/2015
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
    /// Defines a circle that can be used for 2D collision detection.
    /// </summary>
    public sealed class BoundingCircle : IBounding2D
    {
        Vector2 center;
        float radius;

        /// <summary>
        /// The position of the BoundingCircle.
        /// </summary>
        public Vector2 Position
        {
            get { return -new Vector2(radius); }
            set { center = value + new Vector2(radius); }
        }

        /// <summary>
        /// The center of the BoundingCircle.
        /// </summary>
        public Vector2 Center
        {
            get { return center; }
            set { center = value; }
        }

        /// <summary>
        /// The size of the BoundingCircle.
        /// </summary>
        public Vector2 Size
        {
            get { return new Vector2(radius * 2); }
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
        /// <returns>The generated rectangle.</returns>
        public Rectangle GenerateRectangle()
        {
            Rectangle rect = new Rectangle(0, 0,
                (int)Radius * 2, (int)Radius * 2);

            rect.X = (int)(Center.X - Radius);
            rect.Y = (int)(Center.Y + Radius);

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
        public MTV Contains(BoundingCircle circle)
        {
            float distance = (center - circle.center).Length();
            float r = radius + circle.radius;

            if (distance < radius + circle.radius)
                return new MTV(Vector2.Normalize(circle.center - center), distance - r);

            return MTV.Initial;
        }

        /// <summary>
        /// A check to see if a BoundingRectangle
        /// has collided with this BoundingCircle.
        /// </summary>
        /// <param name="rect">The BoundingRectangle to check aginst.</param>
        /// <returns>The result of the check.</returns>
        public MTV Contains(BoundingRectangle rect)
        {
            Vector2 normal = center - rect.Center;

            Vector2 closest = normal;

            float xExtent = (rect.Position.X + rect.Width - rect.Position.X) / 2;
            float yExtent = (rect.Position.Y + rect.Height - rect.Position.Y) / 2;

            closest.X = MathHelper.Clamp(-xExtent, xExtent, closest.X);
            closest.Y = MathHelper.Clamp(-yExtent, yExtent, closest.Y);

            bool inside = false;

            if (normal == closest)
            {
                inside = true;

                if (Math.Abs(normal.X) > Math.Abs(normal.Y))
                {
                    if (closest.X > 0)
                        closest.X = xExtent;
                    else
                        closest.X = -xExtent;
                }
                else
                {
                    if (closest.Y > 0)
                        closest.Y = yExtent;
                    else
                        closest.Y = -yExtent;
                }
            }

            MTV result = MTV.Initial;

            Vector2 norm = normal - closest;
            float d = normal.LengthSquared();
            float r = radius;

            if (d > r * r && !inside)
                return MTV.Initial;

            d = (float)Math.Sqrt(d);

            if (inside)
                result.Direction = -normal;
            else
                result.Direction = normal;

            result.Distance = r - d;

            result.Direction.Normalize();
            return result;
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
