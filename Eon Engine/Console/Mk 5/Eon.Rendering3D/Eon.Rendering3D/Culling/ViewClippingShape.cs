/* Created: 09/11/2013
 * Last Updated: 13/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;
using Eon.Rendering3D.Culling.Helpers;
using Microsoft.Xna.Framework;
using System;

namespace Eon.Rendering3D.Culling
{
    /// <summary>
    /// Defines a shape that is used to 
    /// define the boundaries of the view port.
    /// </summary>
    public struct ViewClippingShape : IClippingShape
    {
        #region Fields

        Vector2[] points;
        int count;

        Vector2[] axis;
        int[] axisMin;
        int[] axisMax;

        #endregion
        #region Properties

        /// <summary>
        /// The points that make up the ViewClipShape. 
        /// </summary>
        public Vector2[] Points
        {
            get { return points; }
        }

        /// <summary>
        /// The lowest set of values for each axis.
        /// </summary>
        public int[] MinimumAxis
        {
            get { return axisMin; }
        }

        /// <summary>
        /// The highest set of values for each axis.
        /// </summary>
        public int[] MaximumAxis
        {
            get { return axisMax; }
        }

        /// <summary>
        /// Gets a point on the ViewClipShape at a particular index.
        /// </summary>
        /// <param name="index">The index of the point to get.</param>
        /// <returns>A point on the ViewClipShape.</returns>
        public Vector2 this[int index]
        {
            get
            {
                if (index < points.Length)
                    return points[index];

                throw new ArgumentOutOfRangeException("Index: " +
                    index + " greater than amount of points: "
                    + points.Length);
            }
        }

        #endregion
        #region Ctors

        ViewClippingShape(Vector2[] points)
        {
            count = points.Length;

            axis = new Vector2[count];
            axisMax = new int[count];
            axisMin = new int[count];

            this.points = points;
        }

        /// <summary>
        /// Creates a new ViewClipShape from a set of points.
        /// </summary>
        /// <param name="points">The point to make up the ViewClipShape.</param>
        /// <returns>The created ViewClipShape.</returns>
        public static ViewClippingShape FromPoints(Vector2[] points)
        {
            ViewClippingShape shape = new ViewClippingShape(points);
            shape.BuildAxis();
            return shape;
        }

        /// <summary>
        /// Creates a new ViewClipShape from a set of points.
        /// </summary>
        /// <param name="point1">Point 1.</param>
        /// <param name="point2">Point 2.</param>
        /// <param name="point3">Point 3.</param>
        /// <returns>The created ViewClipShape.</returns>
        public static ViewClippingShape FromPoints(Vector2 point1,
            Vector2 point2, Vector2 point3)
        {
            Vector2[] points = new Vector2[]
            {
                point1,
                point2,
                point3
            };

            ViewClippingShape shape = new ViewClippingShape(points);
            shape.BuildAxis();
            return shape;
        }

        /// <summary>
        /// Creates a new ViewClipShape from a set of points.
        /// </summary>
        /// <param name="point1">Point 1.</param>
        /// <param name="point2">Point 2.</param>
        /// <param name="point3">Point 3.</param>
        /// <param name="point4">Point 4.</param>
        /// <returns>The created ViewClipShape.</returns>
        public static ViewClippingShape FromPoints(Vector2 point1,
            Vector2 point2, Vector2 point3, Vector2 point4)
        {
            Vector2[] points = new Vector2[]
            {
                point1,
                point2,
                point3,
                point4
            };

            ViewClippingShape shape = new ViewClippingShape(points);
            shape.BuildAxis();
            return shape;
        }

        /// <summary>
        /// Creates a new ViewClipShape from a set of points.
        /// </summary>
        /// <param name="point1">Point 1.</param>
        /// <param name="point2">Point 2.</param>
        /// <param name="point3">Point 3.</param>
        /// <param name="point4">Point 4.</param>
        /// <param name="point5">Point 5.</param>
        /// <returns>The created ViewClipShape.</returns>
        public static ViewClippingShape FromPoints(Vector2 point1,
            Vector2 point2, Vector2 point3, Vector2 point4, Vector2 point5)
        {
            Vector2[] points = new Vector2[]
            {
                point1,
                point2,
                point3,
                point4,
                point5
            };

            ViewClippingShape shape = new ViewClippingShape(points);
            shape.BuildAxis();
            return shape;
        }

        /// <summary>
        /// Creates a new ViewClipShape from a set of points.
        /// </summary>
        /// <param name="point1">Point 1.</param>
        /// <param name="point2">Point 2.</param>
        /// <param name="point3">Point 3.</param>
        /// <param name="point4">Point 4.</param>
        /// <param name="point5">Point 5.</param>
        /// <param name="point6">Point 6.</param>
        /// <returns>The created ViewClipShape.</returns>
        public static ViewClippingShape FromPoints(Vector2 point1, Vector2 point2,
            Vector2 point3, Vector2 point4, Vector2 point5, Vector2 point6)
        {
            Vector2[] points = new Vector2[]
            {
                point1,
                point2,
                point3,
                point4,
                point5,
                point6
            };

            ViewClippingShape shape = new ViewClippingShape(points);
            shape.BuildAxis();
            return shape;
        }

        void BuildAxis()
        {
            axis = new Vector2[count];
            axisMax = new int[count];
            axisMin = new int[count];

            for (int i = 0; i < count; i++)
            {
                int second = i + 1;

                if (second >= count)
                    second = 0;

                axis[i] = points[i] - points[second];
            }

            SetScalars();
        }

        void SetScalars()
        {
            for (int i = 0; i < count; i++)
            {
                Vector2 axis = GetAxis(i);

                IntersectionHelper.Project(axis, this,
                    out axisMin[i], out axisMax[i]);
            }
        }

        #endregion
        #region Helpers

        /// <summary>
        /// Inserts a point at a particular index.
        /// </summary>
        /// <param name="index">The index to insert the point at.</param>
        /// <param name="point">The point to be inserted.</param>
        public void InsertAt(int index, Vector2 point)
        {
            if (index >= 0 || index <= 4)
            {
                points = ArrayHelper.InsertAt<Vector2>(index, point, points);

                count++;
                BuildAxis();
            }
        }

        /// <summary>
        /// Gets an axis at an index.
        /// </summary>
        /// <param name="index">The index of axis to get.</param>
        /// <returns>The found axis.</returns>
        public Vector2 GetAxis(int index)
        {
            if (index < 0 || index > (count - 1))
                throw new ArgumentOutOfRangeException(
                    "Index must be between 0 to " + (count - 1));

            return axis[index];
        }

        /// <summary>
        /// Gets the minimum axis scalar at an index.
        /// </summary>
        /// <param name="index">The index of the minimum axis scalar.</param>
        /// <returns>The minimum axis scalar.</returns>
        public int GetMinimumScalar(int index)
        {
            if (index < 0 || index > (count - 1))
                throw new ArgumentOutOfRangeException(
                    "Index must be between 0 to " + (count - 1));

            return axisMin[index];
        }

        /// <summary>
        /// Gets the maximum axis scalar at an index.
        /// </summary>
        /// <param name="index">The index of the maximum axis scalar.</param>
        /// <returns>The maximum axis scalar.</returns>
        public int GetMaximumScalar(int index)
        {
            if (index < 0 || index > (count - 1))
                throw new ArgumentOutOfRangeException(
                    "Index must be between 0 to " + (count - 1));

            return axisMax[index];
        }

        /// <summary>
        /// Replaces a point on the ViewClipShape.
        /// </summary>
        /// <param name="index">The index of the point to be replaced.</param>
        /// <param name="newPoint">The new point.</param>
        public void ReplacePoint(int index, Vector2 newPoint)
        {
            if (index < 0 || index > (count - 1))
                throw new ArgumentOutOfRangeException(
                    "Index must be between 0 to " + (count - 1));

            points[index] = newPoint;
            BuildAxis();
        }

        /// <summary>
        /// Adapted from http://paulbourke.net/geometry/insidepoly/
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool ContainsPoint(Vector2 point)
        {
            int counter = 0;
            int i;
            Vector2 p2;

            Vector2 p1 = this[0];

            for (i = 1; i <= points.Length; i++)
            {
                p2 = this[i % points.Length];

                if (point.Y > Math.Min(p1.Y, p2.Y))
                    if (point.Y <= Math.Max(p1.Y, p2.Y))
                        if (point.X <= Math.Max(p1.X, p2.X))
                            if (p1.Y != p2.Y)
                            {
                                float xinters = (point.Y - p1.Y) * (p2.X - p1.X) / (p2.Y - p1.Y) + p1.X;

                                if (p1.X == p2.X || point.X <= xinters)
                                    counter++;
                            }

                p1 = p2;
            }

            return counter % 2 != 0;
        }

        #endregion
    }
}
