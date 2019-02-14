/* Created: 14/04/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Physics2D.Maths.Shapes;
using Microsoft.Xna.Framework;
using System;

namespace Eon.Physics2D.Maths
{
    public class ConvexShapeCollisionHelper
    {
        public static bool CheckCollision(ConvexShape shape1, ConvexShape shape2, out MTV mtv)
        {
            float overlap = 100000.00f;

            Vector2 smallest = Vector2.Zero;

            Vector2[] axis1 = shape1.GetAxis();
            Vector2[] axis2 = shape2.GetAxis();

            if (shape1 is Circle && shape2 is Circle)
            {
                axis1 = new Vector2[1];
                axis1[0] = ((Circle)shape1).Center - ((Circle)shape2).Center;

                axis2 = axis1;
            }

            for (int i = 0; i < axis1.Length; i++)
            {
                Vector2 axis = axis1[i];
                axis.Normalize();

                Projection p1 = shape1.Project(axis);
                Projection p2 = shape2.Project(axis);

                if (!p1.Overlaps(p2))
                {
                    mtv = new MTV(smallest, 0);
                    return false;
                }
                else
                {
                    float over = p1.GetOverlap(p2);

                    if (p1.Contains(p2) || p2.Contains(p1))
                    {
                        float mins = Math.Abs(p1.Min - p2.Min);
                        float maxs = Math.Abs(p1.Max - p2.Max);

                        if (mins < maxs)
                            over += mins;
                        else
                            over += maxs;
                    }

                    if (over < overlap)
                    {
                        overlap = over;
                        smallest = axis;
                    }
                }
            }

            for (int i = 0; i < axis2.Length; i++)
            {
                Vector2 axis = axis2[i];

                Projection p1 = shape1.Project(axis);
                Projection p2 = shape2.Project(axis);

                if (!p1.Overlaps(p2))
                {
                    mtv = new MTV(smallest, 0);
                    return false;
                }
                else
                {
                    float over = p1.GetOverlap(p2);

                    if (p1.Contains(p2) || p2.Contains(p1))
                    {
                        float mins = Math.Abs(p1.Min - p2.Min);
                        float maxs = Math.Abs(p1.Max - p2.Max);

                        if (mins < maxs)
                            over += mins;
                        else
                            over += maxs;
                    }

                    if (over < overlap)
                    {
                        overlap = over;
                        smallest = axis;
                    }
                }
            }

            mtv = new MTV(smallest, overlap);

            return true;
        }
    }
}
