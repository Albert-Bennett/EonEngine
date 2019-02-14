/* Created: 14/04/2014
 * Last Updated: 04/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.Helpers;
using Microsoft.Xna.Framework;
using System;

namespace Eon.Physics2D.Maths.Shapes
{
    /// <summary>
    /// Used to define a circle ConvexShape.
    /// </summary>
    public sealed class Circle : ConvexShape
    {
        const int POINT_COUNT = 16;
        float radius;

        Vector2[] vertices = new Vector2[POINT_COUNT];

        /// <summary>
        /// The vertices that make up the circle.
        /// </summary>
        public override Vector2[] Vertices
        {
            get { return vertices; }
        }

        /// <summary>
        /// Gerts the area of the Circle.
        /// </summary>
        public float Area
        {
            get
            {
                return EonMathsHelper.Pi * (radius * radius);
            }
        }

        /// <summary>
        /// The radius of the Circle.
        /// </summary>
        public float Radius
        {
            get { return radius; }
            set { radius = value; }
        }

        /// <summary>
        /// Creates a new Circle.
        /// </summary>
        /// <param name="center">The center of the Circle.</param>
        /// <param name="radius">The radius of the Circle.</param>
        public Circle(Vector2 center, float radius)
        {
            Center = center;
            this.radius = radius;

            CreateVertices();
        }

        protected override void CreateVertices()
        {
            for (int i = 0; i < POINT_COUNT; i++)
                vertices[i] = CalculatePoint(i);
        }

        Vector2 CalculatePoint(int index)
        {
            float theta = index * MathHelper.TwoPi;

            Vector2 v = Center + radius * new Vector2(
            (float)Math.Cos(theta), (float)Math.Sin(theta));

            return v;
        }
    }
}
