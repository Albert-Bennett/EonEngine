/* Created 09/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Rendering3D.Culling.Helpers
{
    /// <summary>
    /// Defines a helper class for intersections.
    /// </summary>
    internal class IntersectionHelper
    {
        public static void Project(Vector2 axis, IContainPoints shape, out int min, out int max)
        {
            min = GenerateScalar(shape.Points[0], ref axis);
            max = min;

            for (int i = 1; i < shape.Count; i++)
            {
                int curr = GenerateScalar(shape.Points[i], ref axis);

                if (curr <= min)
                    min = curr;
                else if (curr > max)
                    max = curr;
            }
        }

        /// <summary>
        /// Generates a scalar value for axis projection
        /// (based on George Clingerman's sample).
        /// </summary>
        /// <param name="point">Point to be projected.</param>
        /// <param name="axis">Axis of projection.</param>
        /// <returns>Generated scalar alopng the given axis.</returns>
        internal static int GenerateScalar(Vector2 point, ref Vector2 axis)
        {
            float numerator = (point.X * axis.X) + (point.Y * axis.Y);
            float denominator = (axis.X * axis.X) + (axis.Y * axis.Y);
            float res = numerator / denominator;

            return (int)((axis.X * (res * axis.X)) + (axis.Y * (res * axis.Y)));
        }

        public static bool Intersects(ref ProjectionRectangle rect, ref ViewClipShape shape)
        {
            int min, max;

            Project(rect.Axis[0], shape, out min, out max);

            if (rect.MinimumAxis[0] > max || rect.MaximumAxis[0] < min)
                return false;

            Project(rect.Axis[1], shape, out min, out max);

            if (rect.MinimumAxis[1] > max || rect.MaximumAxis[1] < min)
                return false;

            for (int i = 0; i < shape.Count; i++)
            {
                Project(shape.GetAxis(i), rect, out min, out max);

                if (shape.GetMinimumScalar(i) > max || 
                    shape.GetMaximumScalar(i) < min)
                    return false;
            }

            return true;
        }
    }
}
