/* Created: 14/04/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;
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

        public override Vector2[] Vertices
        {
            get { throw new NotImplementedException(); }
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
            //Create vertices using point number.

        }
    }
}
