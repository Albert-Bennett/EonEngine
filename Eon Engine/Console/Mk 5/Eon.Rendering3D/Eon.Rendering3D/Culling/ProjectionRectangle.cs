/* Created: 09/11/2013
 * Last Updated: 05/10/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering3D.Culling.Helpers;
using Microsoft.Xna.Framework;
using System;

namespace Eon.Rendering3D.Culling
{
    /// <summary>
    /// Defines a rectangle shape that is used for culling.
    /// </summary>
    public struct ProjectionRectangle : IClippingShape
    {
        Vector2[] points;

        Vector2[] axis;
        int[] axisMin;
        int[] axisMax;

        /// <summary>
        /// The points that make up the ProjectionRectangle.
        /// </summary>
        public Vector2[] Points
        {
            get { return points; }
        }

        /// <summary>
        /// Gets a point at an index.
        /// </summary>
        /// <param name="index">The index of the point to get.</param>
        /// <returns>The point at the given index.</returns>
        public Vector2 this[int index]
        {
            get
            {
                if (index > Points.Length || index < 0)
                    throw new ArgumentOutOfRangeException(
                        "Given index has to be between 0 and 3");

                return points[index];

            }
        }

        /// <summary>
        /// The axis of the ProjectionRectangle.
        /// </summary>
        public Vector2[] Axis
        {
            get { return axis; }
        }

        /// <summary>
        /// The minimum values for the ProjectionRectangle's axis.
        /// </summary>
        public int[] MinimumAxis
        {
            get { return axisMin; }
        }

        /// <summary>
        /// The maximum values for the ProjectionRectangle's axis.
        /// </summary>
        public int[] MaximumAxis
        {
            get { return axisMax; }
        }

        ProjectionRectangle(Vector2 min, Vector2 max)
        {
            axis = new Vector2[2];
            axisMin = new int[2];
            axisMax = new int[2];

            points = new Vector2[]
            {
                min,
                new Vector2(max.X, min.Y),
                new Vector2(min.X, max.Y),
                max
            };
        }

        /// <summary>
        /// Creates a ProjectionRectangle from mimimal and maximal points.
        /// </summary>
        /// <param name="minimalPoint">Minimal point.</param>
        /// <param name="maximalPoint">Maximal point.</param>
        /// <returns>The created ProjectionRectangle.</returns>
        public static ProjectionRectangle FromPoints(
            Vector2 minimalPoint, Vector2 maximalPoint)
        {
            ProjectionRectangle rect = new ProjectionRectangle(minimalPoint, maximalPoint);
            rect.Construct();

            return rect;
        }

        void Construct()
        {
            axis[0] = points[0] - points[3];
            axis[1] = points[0] - points[1];

            IntersectionHelper.Project(axis[0], this,
                out axisMin[0], out axisMax[0]);

            IntersectionHelper.Project(axis[1], this,
                out axisMin[1], out axisMax[1]);
        }

        /// <summary>
        /// Gets the axis at an index.
        /// </summary>
        /// <param name="index">The index of the axis to get.</param>
        /// <returns>The retrieved axis.</returns>
        public Vector2 GetAxis(int index)
        {
            if (index < 0 || index > 1)
                throw new ArgumentOutOfRangeException(
                    "Index must be within 0 -> 1.");

            return axis[index];
        }

        /// <summary>
        /// A check to see if the ProjectionRectangle 
        /// intersects with the given ViewClipShape.
        /// </summary>
        /// <param name="shape">The ViewclipShape to check aginst.</param>
        /// <returns>The result of the check.</returns>
        public bool Intersects(ViewClippingShape shape)
        {
            return IntersectionHelper.Intersects(ref this, ref shape);
        }
    }
}
