/* Created 09/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Rendering3D.Culling.Helpers;
using Microsoft.Xna.Framework;
using System;

namespace Eon.Rendering3D.Culling
{
    /// <summary>
    /// Used to define a ClipPlane.
    /// </summary>
    public struct ClippingPlane
    {
        Vector3[] points;

        /// <summary>
        /// The points that make up the ClipPlane.
        /// </summary>
        public Vector3[] Points
        {
            get { return points; }
        }

        /// <summary>
        /// Gets a point at a given index.
        /// </summary>
        /// <param name="index">The index of the point to get.</param>
        /// <returns>The point at the index.</returns>
        public Vector3 this[int index]
        {
            get
            {
                if (index < points.Length)
                    return points[index];

                throw new ArgumentOutOfRangeException("Index must be less than the amount of points.");
            }
        }

        /// <summary>
        /// The number of points in the ClipPlane.
        /// </summary>
        public int Count
        {
            get { return 4; }
        }

        /// <summary>
        /// Creates a new ClipPlane.
        /// </summary>
        /// <param name="point1">Point 1.</param>
        /// <param name="point2">Point 2.</param>
        /// <param name="point3">Point 3.</param>
        /// <param name="point4">Point 4.</param>
        public ClippingPlane(Vector3 point1, Vector3 point2,
            Vector3 point3, Vector3 point4)
        {
            points = new Vector3[]
            {
                point1,
                point2,
                point3,
                point4
            };
        }

        /// <summary>
        /// A check for intersection between a y co-ordinate.
        /// </summary>
        /// <param name="y">The y co-ordinate.</param>
        /// <returns>The result of the check.</returns>
        public IntersectionTypes Intersects(float y)
        {
            if (points[0].Y > y && points[1].Y > y && points[2].Y > y && points[3].Y > y)
                return IntersectionTypes.Above;
            else if (points[0].Y < y && points[1].Y < y && points[2].Y < y && points[3].Y < y)
                return IntersectionTypes.Below;

            return IntersectionTypes.Intersecting;
        }
    }
}
