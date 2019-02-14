/* Created 14/04/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;
using Microsoft.Xna.Framework;

namespace Eon.Physics2D.Math.Shapes
{
    /// <summary>
    /// Used to define a convex shape for collision detection/responce.
    /// </summary>
    public abstract class ConvexShape
    {
        Vector2 center;
        Vector2 diff = Vector2.Zero;

        public abstract Vector2[] Vertices { get; }

        /// <summary>
        /// The center of the ConvexShape.
        /// </summary>
        public virtual Vector2 Center
        {
            get { return center; }
            set
            {
                diff = center - value;
                center = value;
            }
        }

        protected virtual void CreateVertices()
        {
            if (diff == Vector2.Zero)
            {
                center = new Vector2();

                for (int i = 0; i < Vertices.Length; i++)
                {
                    center.X += Vertices[i].X;
                    center.Y += Vertices[i].Y;
                }

                center.X /= Vertices.Length;
                center.Y /= Vertices.Length;
            }

            if (diff != Vector2.Zero)
                if (Vertices != null && Vertices.Length > 0)
                    for (int i = 0; i < Vertices.Length; i++)
                        Vertices[i] += diff;

            diff = Vector2.Zero;
        }

        /// <summary>
        /// Gets the axis of the ConvexShape.
        /// </summary>
        /// <returns>The axis of the ConvexShape.</returns>
        public virtual Vector2[] GetAxis()
        {
            Vector2[] axis = new Vector2[0];
            float[] slopes = new float[0];

            for (int i = 0; i < Vertices.Length; i++)
            {
                Vector2 p1 = Vertices[i];

                int temp = i;

                Vector2 p2 = Vertices[temp+1 == Vertices.Length ? 0 : temp + 1];

                float slope = EonMathHelper.Slope(p1, p2);

                bool found = false;

                if (slopes.Length == 0)
                    slopes = ArrayHelper.AddItem<float>(slope, slopes);
                else
                {
                    int j = 0;

                    while (!found || j < slopes.Length)
                    {
                        if (slopes[i] == slope)
                            found = true;

                        j++;
                    }

                    if (!found)
                        slopes = ArrayHelper.AddItem<float>(slope, slopes);
                }

                if (!found)
                {
                    Vector2 edge = p1 - p2;
                    Vector2 normal = EonMathHelper.Perpendicular(edge);
                    normal.Normalize();

                    axis[i] = normal;
                }
            }

            return axis;
        }

        /// <summary>
        /// Projects an axis on to the ConvexShape.
        /// </summary>
        /// <param name="axis">The axis to be projected.</param>
        /// <returns>The projected axis.</returns>
        public Projection Project(Vector2 axis)
        {
            float min = Vector2.Dot(axis, Vertices[0]);
            float max = min;

            for (int i = 1; i < Vertices.Length; i++)
            {
                float dp = Vector2.Dot(axis, Vertices[i]);

                if (dp < min)
                    min = dp;
                else if (dp > max)
                    max = dp;
            }

            return new Projection(min, max);
        }
    }
}
