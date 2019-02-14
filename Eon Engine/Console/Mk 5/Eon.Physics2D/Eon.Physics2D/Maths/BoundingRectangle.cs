/* Created: 24/09/2015
 * Last Updated: 25/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;
using System;

namespace Eon.Physics2D.Maths
{
    /// <summary>
    /// Defines a BoundingRectangle.
    /// </summary>
    public sealed class BoundingRectangle : IBounding2D
    {
        Vector2 position;
        Vector2 center;

        float width;
        float height;

        /// <summary>
        /// The center of the BoundingRectangle.
        /// </summary>
        public Vector2 Center
        {
            get { return center; }
            set
            {
                center = value;

                position.X = value.X - width / 2;
                position.Y = value.Y - height / 2;
            }
        }

        /// <summary>
        /// The position of the BoundingRectangle.
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;

                center.X = value.X + width / 2;
                center.Y = value.Y + height / 2;
            }
        }

        /// <summary>
        /// The size of the BoundingRectangle.
        /// </summary>
        public Vector2 Size
        {
            get { return new Vector2(width, height); }
        }

        /// <summary>
        /// The width of the BoundingRectangle.
        /// </summary>
        public float Width
        {
            get { return width; }
            set
            {
                width = value;
                center.X = position.X + value / 2;
            }
        }

        /// <summary>
        /// The height of the BoundingRectangle.
        /// </summary>
        public float Height
        {
            get { return height; }
            set
            {
                height = value;
                center.Y = position.Y + value / 2;
            }
        }

        /// <summary>
        /// The maximum point on the BoundingRectangle.
        /// </summary>
        public Vector2 Max
        {
            get
            {
                return new Vector2(width,
                    height) + position;
            }
        }

        /// <summary>
        /// Creates a new BoundingRectangle.
        /// </summary>
        /// <param name="position">The position of the BoundingRectangle.</param>
        /// <param name="width">The width of the BoundingRectangle.</param>
        /// <param name="height">The height of the BoundingRectangle.</param>
        public BoundingRectangle(Vector2 position, float width, float height)
        {
            this.position = position;
            this.width = width;
            this.height = height;

            center = position + (new Vector2(width, height) / 2);
        }

        /// <summary>
        /// Generates a rectangle from the BoundingRectangle.
        /// </summary>
        /// <returns>The generated rectangle.</returns>
        public Rectangle GenerateRectangle()
        {
            return new Rectangle()
            {
                X = (int)position.X,
                Y = (int)position.Y,
                Width = (int)width,
                Height = (int)height
            };
        }

        /// <summary>
        /// A check to see if a point is inside of
        /// a BoundingRectangle.
        /// </summary>
        /// <param name="position">The position to be checked.</param>
        /// <returns>The result of the check.</returns>
        public bool Contains(Vector2 position)
        {
            return position.X <= Max.X && position.X >= Position.X &&
                position.Y <= Max.Y && position.Y >= Position.Y;
        }

        /// <summary>
        /// A check to see if a BoundingCircle collides with the
        /// BoundingRectangle.
        /// </summary>
        /// <param name="circle">The circle to be BoundingCircle with.</param>
        /// <returns>The MTV.</returns>
        public MTV Contains(BoundingCircle circle)
        {
            Vector2 normal = circle.Center - center;

            Vector2 closest = normal;

            float xExtent = (Max.X - position.X) / 2;
            float yExtent = (Max.Y - position.Y) / 2;

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
            float r = circle.Radius;

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
        /// A check to see if another BoundingRectangle
        /// is colliding with this one.
        /// </summary>
        /// <param name="rectangle">The other BoundingRectangle.</param>
        /// <returns>The MTV between them.</returns>
        public MTV Contains(BoundingRectangle rectangle)
        {
            Vector2 normal = rectangle.center - center;

            float extentA = (Max.X - position.X) / 2;
            float extentB = (rectangle.Max.X - rectangle.Position.X) / 2;

            float xOverlap = extentA + extentB - Math.Abs(normal.X);

            if (xOverlap > 0)
            {
                MTV result = MTV.Initial;

                extentA = (Max.Y - position.Y) / 2;
                extentB = (rectangle.Max.Y - rectangle.Position.Y) / 2;

                float yOverlap = extentA + extentB - Math.Abs(normal.Y);

                if (yOverlap > 0)
                {
                    if (xOverlap > yOverlap)
                    {
                        result.Distance = xOverlap;

                        if (normal.X < 0)
                            result.Direction = new Vector2(-1, 0);
                        else
                            result.Direction = new Vector2(1, 0);
                    }
                    else
                    {
                        result.Distance = yOverlap;

                        if (normal.Y < 0)
                            result.Direction = new Vector2(0, -1);
                        else
                            result.Direction = new Vector2(0, 1);
                    }
                }

                return result;
            }

            return MTV.Initial;
        }
    }
}
