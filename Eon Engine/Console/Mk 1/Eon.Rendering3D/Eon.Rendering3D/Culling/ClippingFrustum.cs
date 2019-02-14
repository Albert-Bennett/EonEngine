/* Created 09/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Helpers;
using Eon.Rendering3D.Cameras;
using Eon.Rendering3D.Culling.Helpers;
using Microsoft.Xna.Framework;
using System;

namespace Eon.Rendering3D.Culling
{
    /// <summary>
    /// Defines a frustrum that is used 
    /// to clip the view port.
    /// </summary>
    public struct ClippingFrustum
    {
        #region Fields

        ClippingPlane far;
        ClippingPlane near;

        /// <summary>
        /// The near plane.
        /// </summary>
        public ClippingPlane NearPlane
        {
            get { return near; }
        }

        /// <summary>
        /// The far plane.
        /// </summary>
        public ClippingPlane FarPlane
        {
            get { return far; }
        }

        #endregion
        #region Ctor

        ClippingFrustum(Vector3[] corners)
        {
            near = new ClippingPlane(corners[0], corners[1], corners[2], corners[3]);
            far = new ClippingPlane(corners[4], corners[5], corners[6], corners[7]);
        }

        /// <summary>
        /// Creates a new ClippingFrustrum from a set of corners.  
        /// </summary>
        /// <param name="corners">The corners that make up the ClippingFrustrum.</param>
        /// <returns>The generated ClippingFrustrum.</returns>
        public static ClippingFrustum FromPoints(Vector3[] corners)
        {
            if (corners.Length != 8)
                throw new ArgumentOutOfRangeException("Eight points are needed.");

            return new ClippingFrustum(corners);
        }

        #endregion
        #region Helpers

        /// <summary>
        /// Projects the clippingFrustrum to a target Y co-ordinate.
        /// </summary>
        /// <param name="y">The Y co-ordinate to be projected onto.</param>
        /// <returns>The projection.</returns>
        public ViewClipShape ProjectToY(float y)
        {
            IntersectionTypes nearType = near.Intersects(y);
            IntersectionTypes farType = far.Intersects(y);

            ViewClipShape shape;

            if (nearType == farType && nearType != IntersectionTypes.Intersecting &&
                farType != IntersectionTypes.Intersecting)
            {
                Vector2 viewPoint = CameraManager.CurrentCamera.ViewPoint;
                shape = ViewClipShape.FromPoints(viewPoint, viewPoint, viewPoint);

                return shape;
            }

            if (nearType != farType && nearType != IntersectionTypes.Intersecting &&
                farType != IntersectionTypes.Intersecting)
                shape = FromNoIntersection(y, near, far);
            else if (farType == IntersectionTypes.Intersecting)
                shape = FromIntersection(y, far, near);
            else
                shape = FromIntersection(y, near, far);

            Vector2 vp = CameraManager.CurrentCamera.ViewPoint;

            if (!shape.ContainsPoint(vp))
                AddCameraPos(ref shape, vp);

            return shape;
        }

        ViewClipShape FromIntersection(float y,
            ClippingPlane plane1, ClippingPlane plane2)
        {
            Vector3[] intersections = new Vector3[6];

            int count = 0;

            if (plane1[0].Y > y)
            {
                if (plane1[1].Y > y)
                {
                    if (plane1[2].Y > y)
                    {
                        intersections[0] = EonMathHelper.GetLineFromY(plane1[0], plane1[3], y);
                        intersections[1] = EonMathHelper.GetLineFromY(plane1[2], plane1[3], y);

                        #region Plane2 Projection

                        if (plane2[3].Y > y)
                        {
                            intersections[2] = EonMathHelper.GetLineFromY(plane2[3], plane1[3], y);
                            count = 3;
                        }
                        else
                        {
                            if (plane2[1].Y < y)
                            {
                                intersections[2] = EonMathHelper.GetLineFromY(plane2[2], plane1[2], y);
                                intersections[3] = EonMathHelper.GetLineFromY(plane2[1], plane1[0], y);
                                intersections[4] = EonMathHelper.GetLineFromY(plane2[0], plane1[0], y);

                                count = 5;
                            }
                            else
                            {
                                if (plane2[2].Y > y)
                                {
                                    intersections[2] = EonMathHelper.GetLineFromY(plane2[2], plane2[3], y);
                                    count = 3;
                                }
                                else
                                {
                                    intersections[2] = EonMathHelper.GetLineFromY(plane2[2], plane1[2], y);
                                    intersections[3] = EonMathHelper.GetLineFromY(plane2[1], plane2[2], y);

                                    count = 4;
                                }

                                if (plane2[0].Y > y)
                                {
                                    if (count == 4)
                                    {
                                        intersections[4] = EonMathHelper.GetLineFromY(plane2[0], plane2[3], y);
                                        count = 5;
                                    }
                                    else
                                    {
                                        intersections[3] = EonMathHelper.GetLineFromY(plane2[0], plane2[3], y);
                                        count = 4;
                                    }
                                }
                                else
                                {
                                    if (count == 4)
                                    {
                                        intersections[4] = EonMathHelper.GetLineFromY(plane2[1], plane2[0], y);
                                        intersections[5] = EonMathHelper.GetLineFromY(plane2[0], plane1[0], y);
                                    }
                                    else
                                    {
                                        intersections[3] = EonMathHelper.GetLineFromY(plane2[1], plane2[0], y);
                                        intersections[4] = EonMathHelper.GetLineFromY(plane2[0], plane1[0], y);
                                    }

                                    count += 2;
                                }
                            }
                        }

                        #endregion
                    }
                    else
                    {
                        if (plane1[3].Y > y)
                        {
                            intersections[0] = EonMathHelper.GetLineFromY(plane1[3], plane1[2], y);
                            intersections[1] = EonMathHelper.GetLineFromY(plane1[1], plane1[2], y);

                            #region Plane2 Projection

                            if (plane2[2].Y > y)
                            {
                                intersections[2] = EonMathHelper.GetLineFromY(plane2[2], plane1[2], y);
                                count = 3;
                            }
                            else
                            {
                                if (plane2[0].Y < y)
                                {
                                    intersections[2] = EonMathHelper.GetLineFromY(plane2[1], plane1[1], y);
                                    intersections[3] = EonMathHelper.GetLineFromY(plane2[0], plane1[3], y);
                                    intersections[4] = EonMathHelper.GetLineFromY(plane2[3], plane1[3], y);

                                    count = 5;
                                }
                                else
                                {
                                    if (plane2[1].Y > y)
                                    {
                                        intersections[2] = EonMathHelper.GetLineFromY(plane2[1], plane2[2], y);
                                        count = 3;
                                    }
                                    else
                                    {
                                        intersections[2] = EonMathHelper.GetLineFromY(plane2[1], plane1[1], y);
                                        intersections[3] = EonMathHelper.GetLineFromY(plane2[0], plane2[1], y);

                                        count = 4;
                                    }

                                    if (plane2[3].Y > y)
                                    {
                                        if (count == 4)
                                            intersections[4] = EonMathHelper.GetLineFromY(plane2[3], plane2[2], y);
                                        else
                                            intersections[3] = EonMathHelper.GetLineFromY(plane2[3], plane2[2], y);

                                        count++;
                                    }
                                    else
                                    {
                                        if (count == 4)
                                        {
                                            intersections[4] = EonMathHelper.GetLineFromY(plane2[0], plane2[3], y);
                                            intersections[5] = EonMathHelper.GetLineFromY(plane2[3], plane1[3], y);

                                            count = 6;
                                        }
                                        else
                                        {
                                            intersections[3] = EonMathHelper.GetLineFromY(plane2[0], plane2[3], y);
                                            intersections[4] = EonMathHelper.GetLineFromY(plane2[3], plane1[3], y);

                                            count = 5;
                                        }
                                    }
                                }
                            }

                            #endregion
                        }
                        else
                        {
                            intersections[0] = EonMathHelper.GetLineFromY(plane1[0], plane1[3], y);
                            intersections[1] = EonMathHelper.GetLineFromY(plane1[1], plane1[2], y);

                            #region Plane2 Projection

                            if (plane2[1].Y > y)
                            {
                                if (plane2[2].Y > y)
                                {
                                    if (plane2[3].Y > y)
                                    {
                                        intersections[2] = EonMathHelper.GetLineFromY(plane2[2], plane1[2], y);
                                        intersections[3] = EonMathHelper.GetLineFromY(plane2[3], plane1[3], y);

                                        count = 4;
                                    }
                                    else
                                    {
                                        intersections[2] = EonMathHelper.GetLineFromY(plane2[2], plane1[2], y);
                                        intersections[3] = EonMathHelper.GetLineFromY(plane2[2], plane2[3], y);
                                        intersections[4] = EonMathHelper.GetLineFromY(plane2[0], plane2[3], y);

                                        count = 5;
                                    }
                                }
                                else
                                {
                                    if (plane2[3].Y > y)
                                    {
                                        intersections[2] = EonMathHelper.GetLineFromY(plane2[1], plane2[2], y);
                                        intersections[3] = EonMathHelper.GetLineFromY(plane2[3], plane2[2], y);
                                        intersections[4] = EonMathHelper.GetLineFromY(plane2[3], plane1[3], y);

                                        count = 5;
                                    }
                                    else
                                    {
                                        if (plane2[0].Y > y)
                                        {
                                            intersections[2] = EonMathHelper.GetLineFromY(plane2[1], plane2[2], y);
                                            intersections[3] = EonMathHelper.GetLineFromY(plane2[0], plane2[3], y);

                                            count = 4;
                                        }
                                        else
                                        {
                                            intersections[2] = EonMathHelper.GetLineFromY(plane2[1], plane2[2], y);
                                            intersections[3] = EonMathHelper.GetLineFromY(plane2[1], plane2[0], y);
                                            intersections[4] = EonMathHelper.GetLineFromY(plane2[0], plane1[0], y);

                                            count = 5;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                intersections[2] = EonMathHelper.GetLineFromY(plane2[1], plane1[1], y);

                                if (plane2[0].Y > y)
                                {
                                    intersections[3] = EonMathHelper.GetLineFromY(plane2[0], plane2[1], y);
                                    intersections[4] = EonMathHelper.GetLineFromY(plane2[0], plane2[3], y);

                                    count = 5;
                                }
                                else
                                {
                                    intersections[3] = EonMathHelper.GetLineFromY(plane2[0], plane1[0], y);

                                    count = 4;
                                }
                            }

                            #endregion
                        }
                    }
                }
                else
                {
                    if (plane1[3].Y < y)
                    {
                        intersections[0] = EonMathHelper.GetLineFromY(plane1[0], plane1[3], y);
                        intersections[1] = EonMathHelper.GetLineFromY(plane1[0], plane1[1], y);

                        #region Plane2 Projection

                        if (plane2[0].Y < y)
                        {
                            intersections[2] = EonMathHelper.GetLineFromY(plane2[0], plane1[0], y);
                            count = 3;
                        }
                        else
                        {
                            if (plane2[2].Y > y)
                            {
                                intersections[2] = EonMathHelper.GetLineFromY(plane2[1], plane1[1], y);
                                intersections[3] = EonMathHelper.GetLineFromY(plane2[2], plane1[2], y);
                                intersections[4] = EonMathHelper.GetLineFromY(plane2[3], plane1[3], y);

                                count = 5;
                            }
                            else
                            {
                                if (plane2[1].Y > y)
                                {
                                    intersections[2] = EonMathHelper.GetLineFromY(plane2[1], plane1[1], y);
                                    intersections[3] = EonMathHelper.GetLineFromY(plane2[1], plane2[2], y);

                                    count = 4;
                                }
                                else
                                {
                                    intersections[2] = EonMathHelper.GetLineFromY(plane2[0], plane2[1], y);

                                    count = 3;
                                }

                                if (plane2[3].Y > y)
                                {
                                    if (count == 4)
                                    {
                                        intersections[4] = EonMathHelper.GetLineFromY(plane2[3], plane2[2], y);
                                        intersections[5] = EonMathHelper.GetLineFromY(plane2[3], plane1[3], y);

                                        count = 6;
                                    }
                                    else
                                    {
                                        intersections[3] = EonMathHelper.GetLineFromY(plane2[3], plane2[2], y);
                                        intersections[4] = EonMathHelper.GetLineFromY(plane2[3], plane1[3], y);

                                        count = 5;
                                    }
                                }
                                else
                                {
                                    if (count == 4)
                                    {
                                        intersections[4] = EonMathHelper.GetLineFromY(plane2[0], plane2[3], y);
                                        count = 5;
                                    }
                                    else
                                    {
                                        intersections[3] = EonMathHelper.GetLineFromY(plane2[0], plane2[3], y);
                                        count = 4;
                                    }
                                }
                            }
                        }

                        #endregion
                    }
                    else
                    {
                        if (plane1[2].Y < y)
                        {
                            intersections[0] = EonMathHelper.GetLineFromY(plane1[3], plane1[2], y);
                            intersections[1] = EonMathHelper.GetLineFromY(plane1[0], plane1[1], y);

                            #region Plane2 Projection

                            if (plane2[0].Y > y)
                            {
                                if (plane2[1].Y > y)
                                {
                                    if (plane2[2].Y > y)
                                    {
                                        intersections[2] = EonMathHelper.GetLineFromY(plane2[1], plane1[1], y);
                                        intersections[3] = EonMathHelper.GetLineFromY(plane2[2], plane1[2], y);

                                        count = 4;
                                    }
                                    else
                                    {
                                        intersections[2] = EonMathHelper.GetLineFromY(plane2[1], plane1[1], y);
                                        intersections[3] = EonMathHelper.GetLineFromY(plane2[1], plane2[2], y);
                                        intersections[4] = EonMathHelper.GetLineFromY(plane2[3], plane2[2], y);

                                        count = 5;
                                    }
                                }
                                else
                                {
                                    if (plane2[2].Y > y)
                                    {
                                        intersections[2] = EonMathHelper.GetLineFromY(plane2[0], plane2[1], y);
                                        intersections[3] = EonMathHelper.GetLineFromY(plane2[2], plane2[1], y);
                                        intersections[4] = EonMathHelper.GetLineFromY(plane2[2], plane1[2], y);

                                        count = 5;
                                    }
                                    else
                                    {
                                        if (plane2[3].Y > y)
                                        {
                                            intersections[2] = EonMathHelper.GetLineFromY(plane2[0], plane2[1], y);
                                            intersections[3] = EonMathHelper.GetLineFromY(plane2[3], plane2[2], y);

                                            count = 4;
                                        }
                                        else
                                        {
                                            intersections[2] = EonMathHelper.GetLineFromY(plane2[0], plane2[1], y);
                                            intersections[3] = EonMathHelper.GetLineFromY(plane2[0], plane2[3], y);
                                            intersections[4] = EonMathHelper.GetLineFromY(plane2[3], plane1[3], y);

                                            count = 5;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (plane2[4].Y > y)
                                {
                                    intersections[2] = EonMathHelper.GetLineFromY(plane2[0], plane1[0], y);
                                    intersections[3] = EonMathHelper.GetLineFromY(plane2[3], plane2[0], y);
                                    intersections[4] = EonMathHelper.GetLineFromY(plane2[3], plane2[2], y);

                                    count = 5;
                                }
                                else
                                {
                                    intersections[2] = EonMathHelper.GetLineFromY(plane2[0], plane1[0], y);
                                    intersections[3] = EonMathHelper.GetLineFromY(plane2[3], plane1[3], y);
                                    count = 4;
                                }
                            }

                            #endregion
                        }
                        else
                        {
                            intersections[0] = EonMathHelper.GetLineFromY(plane1[2], plane1[1], y);
                            intersections[1] = EonMathHelper.GetLineFromY(plane1[0], plane1[1], y);

                            #region Plane2 Projection

                            if (plane2[1].Y > y)
                            {
                                intersections[2] = EonMathHelper.GetLineFromY(plane2[1], plane1[1], y);
                                count = 3;
                            }
                            else
                            {
                                if (plane2[3].Y < y)
                                {
                                    intersections[2] = EonMathHelper.GetLineFromY(plane2[0], plane1[0], y);
                                    intersections[3] = EonMathHelper.GetLineFromY(plane2[3], plane1[2], y);
                                    intersections[4] = EonMathHelper.GetLineFromY(plane2[2], plane1[2], y);

                                    count = 5;
                                }
                                else
                                {
                                    if (plane2[0].Y > y)
                                    {
                                        intersections[2] = EonMathHelper.GetLineFromY(plane2[0], plane2[1], y);

                                        count = 3;
                                    }
                                    else
                                    {
                                        intersections[2] = EonMathHelper.GetLineFromY(plane2[0], plane1[0], y);
                                        intersections[3] = EonMathHelper.GetLineFromY(plane2[3], plane2[0], y);

                                        count = 4;
                                    }

                                    if (plane2[2].Y > y)
                                    {
                                        if (count == 4)
                                            intersections[4] = EonMathHelper.GetLineFromY(plane2[2], plane2[1], y);
                                        else
                                            intersections[3] = EonMathHelper.GetLineFromY(plane2[2], plane2[1], y);

                                        count++;
                                    }
                                    else
                                    {
                                        if (count == 4)
                                        {
                                            intersections[4] = EonMathHelper.GetLineFromY(plane2[3], plane2[2], y);
                                            intersections[5] = EonMathHelper.GetLineFromY(plane2[2], plane1[2], y);
                                        }
                                        else
                                        {
                                            intersections[3] = EonMathHelper.GetLineFromY(plane2[3], plane2[2], y);
                                            intersections[4] = EonMathHelper.GetLineFromY(plane2[2], plane1[2], y);
                                        }

                                        count += 2;
                                    }
                                }
                            }

                            #endregion
                        }
                    }
                }
            }
            else
            {
                if (plane1[1].Y > y)
                {
                    if (plane1[3].Y > y)
                    {
                        intersections[0] = EonMathHelper.GetLineFromY(plane1[1], plane1[0], y);
                        intersections[1] = EonMathHelper.GetLineFromY(plane1[3], plane1[0], y);

                        #region Plane2 Projection

                        if (plane2[0].Y > y)
                        {
                            intersections[2] = EonMathHelper.GetLineFromY(plane2[0], plane1[0], y);
                            count = 3;
                        }
                        else
                        {
                            if (plane2[2].Y < y)
                            {
                                intersections[2] = EonMathHelper.GetLineFromY(plane2[3], plane1[3], y);
                                intersections[3] = EonMathHelper.GetLineFromY(plane2[2], plane1[1], y);
                                intersections[4] = EonMathHelper.GetLineFromY(plane2[1], plane1[1], y);

                                count = 5;
                            }
                            else
                            {
                                if (plane2[3].Y > y)
                                {
                                    intersections[2] = EonMathHelper.GetLineFromY(plane2[3], plane2[0], y);
                                    count = 3;
                                }
                                else
                                {
                                    intersections[2] = EonMathHelper.GetLineFromY(plane2[3], plane1[3], y);
                                    intersections[3] = EonMathHelper.GetLineFromY(plane2[2], plane2[3], y);

                                    count = 4;
                                }

                                if (plane2[1].Y > y)
                                {
                                    if (count == 4)
                                        intersections[4] = EonMathHelper.GetLineFromY(plane2[1], plane2[0], y);
                                    else
                                        intersections[3] = EonMathHelper.GetLineFromY(plane2[1], plane2[0], y);

                                    count++;
                                }
                                else
                                {
                                    if (count == 4)
                                    {
                                        intersections[4] = EonMathHelper.GetLineFromY(plane2[2], plane2[1], y);
                                        intersections[5] = EonMathHelper.GetLineFromY(plane2[1], plane1[1], y);
                                    }
                                    else
                                    {
                                        intersections[3] = EonMathHelper.GetLineFromY(plane2[2], plane2[1], y);
                                        intersections[4] = EonMathHelper.GetLineFromY(plane2[1], plane1[1], y);
                                    }

                                    count += 2;
                                }
                            }
                        }

                        #endregion
                    }
                    else
                    {
                        if (plane1[2].Y > y)
                        {
                            intersections[0] = EonMathHelper.GetLineFromY(plane1[1], plane1[0], y);
                            intersections[1] = EonMathHelper.GetLineFromY(plane1[2], plane1[3], y);

                            #region Plane2 Projection

                            if (plane2[2].Y > y)
                            {
                                if (plane2[3].Y > y)
                                {
                                    if (plane2[0].Y > y)
                                    {
                                        intersections[2] = EonMathHelper.GetLineFromY(plane2[3], plane1[3], y);
                                        intersections[3] = EonMathHelper.GetLineFromY(plane2[0], plane1[0], y);

                                        count = 4;
                                    }
                                    else
                                    {
                                        intersections[2] = EonMathHelper.GetLineFromY(plane2[3], plane1[3], y);
                                        intersections[3] = EonMathHelper.GetLineFromY(plane2[3], plane2[0], y);
                                        intersections[4] = EonMathHelper.GetLineFromY(plane2[1], plane2[0], y);

                                        count = 5;
                                    }
                                }
                                else
                                {
                                    if (plane2[0].Y > y)
                                    {
                                        intersections[2] = EonMathHelper.GetLineFromY(plane2[2], plane2[3], y);
                                        intersections[3] = EonMathHelper.GetLineFromY(plane2[0], plane2[3], y);
                                        intersections[4] = EonMathHelper.GetLineFromY(plane2[0], plane1[0], y);

                                        count = 5;
                                    }
                                    else
                                    {
                                        if (plane2[1].Y > y)
                                        {
                                            intersections[2] = EonMathHelper.GetLineFromY(plane2[2], plane2[3], y);
                                            intersections[3] = EonMathHelper.GetLineFromY(plane2[1], plane2[0], y);

                                            count = 4;
                                        }
                                        else
                                        {
                                            intersections[2] = EonMathHelper.GetLineFromY(plane2[2], plane2[3], y);
                                            intersections[3] = EonMathHelper.GetLineFromY(plane2[2], plane2[1], y);
                                            intersections[4] = EonMathHelper.GetLineFromY(plane2[1], plane1[1], y);

                                            count = 5;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (plane2[1].Y > y)
                                {
                                    intersections[2] = EonMathHelper.GetLineFromY(plane2[2], plane1[2], y);
                                    intersections[3] = EonMathHelper.GetLineFromY(plane2[1], plane2[2], y);
                                    intersections[4] = EonMathHelper.GetLineFromY(plane2[1], plane2[0], y);

                                    count = 5;
                                }
                                else
                                {
                                    intersections[2] = EonMathHelper.GetLineFromY(plane2[2], plane1[2], y);
                                    intersections[3] = EonMathHelper.GetLineFromY(plane2[1], plane1[1], y);

                                    count = 4;
                                }
                            }

                            #endregion
                        }
                        else
                        {
                            intersections[0] = EonMathHelper.GetLineFromY(plane1[1], plane1[2], y);
                            intersections[1] = EonMathHelper.GetLineFromY(plane1[1], plane1[2], y);

                            #region Plane2 Projection

                            if (plane2[1].Y < y)
                            {
                                intersections[2] = EonMathHelper.GetLineFromY(plane2[1], plane1[1], y);
                                count = 3;
                            }
                            else
                            {
                                if (plane2[3].Y > y)
                                {
                                    intersections[2] = EonMathHelper.GetLineFromY(plane2[2], plane1[2], y);
                                    intersections[3] = EonMathHelper.GetLineFromY(plane2[3], plane1[3], y);
                                    intersections[4] = EonMathHelper.GetLineFromY(plane2[0], plane1[0], y);

                                    count = 5;
                                }
                                else
                                {
                                    if (plane2[2].Y > y)
                                    {
                                        intersections[2] = EonMathHelper.GetLineFromY(plane2[2], plane1[2], y);
                                        intersections[3] = EonMathHelper.GetLineFromY(plane2[2], plane2[3], y);

                                        count = 4;
                                    }
                                    else
                                    {
                                        intersections[2] = EonMathHelper.GetLineFromY(plane2[1], plane2[2], y);
                                        count = 3;
                                    }

                                    if (plane2[0].Y > y)
                                    {
                                        if (count == 4)
                                        {
                                            intersections[4] = EonMathHelper.GetLineFromY(plane2[0], plane2[3], y);
                                            intersections[5] = EonMathHelper.GetLineFromY(plane2[0], plane1[0], y);
                                        }
                                        else
                                        {
                                            intersections[3] = EonMathHelper.GetLineFromY(plane2[0], plane2[3], y);
                                            intersections[4] = EonMathHelper.GetLineFromY(plane2[0], plane1[0], y);
                                        }

                                        count += 2;
                                    }
                                    else
                                    {
                                        if (count == 4)
                                            intersections[4] = EonMathHelper.GetLineFromY(plane2[1], plane2[0], y);
                                        else
                                            intersections[3] = EonMathHelper.GetLineFromY(plane2[1], plane2[0], y);

                                        count++;
                                    }

                                }
                            }

                            #endregion
                        }
                    }
                }
                else
                {
                    if (plane1[2].Y > y)
                    {
                        intersections[0] = EonMathHelper.GetLineFromY(plane1[2], plane1[1], y);

                        if (plane1[3].Y > y)
                        {
                            intersections[1] = EonMathHelper.GetLineFromY(plane1[3], plane1[0], y);

                            #region Plane2 Projection

                            if (plane2[3].Y > y)
                            {
                                if (plane2[0].Y > y)
                                {
                                    if (plane2[1].Y > y)
                                    {
                                        intersections[2] = EonMathHelper.GetLineFromY(plane2[0], plane1[0], y);
                                        intersections[3] = EonMathHelper.GetLineFromY(plane2[1], plane1[1], y);

                                        count = 4;
                                    }
                                    else
                                    {
                                        intersections[2] = EonMathHelper.GetLineFromY(plane2[0], plane1[0], y);
                                        intersections[3] = EonMathHelper.GetLineFromY(plane2[0], plane2[1], y);
                                        intersections[4] = EonMathHelper.GetLineFromY(plane2[2], plane2[1], y);

                                        count = 5;
                                    }
                                }
                                else
                                {
                                    if (plane2[1].Y > y)
                                    {
                                        intersections[2] = EonMathHelper.GetLineFromY(plane2[3], plane2[0], y);
                                        intersections[3] = EonMathHelper.GetLineFromY(plane2[1], plane2[0], y);
                                        intersections[4] = EonMathHelper.GetLineFromY(plane2[1], plane1[1], y);

                                        count = 5;
                                    }
                                    else
                                    {
                                        if (plane2[2].Y > y)
                                        {
                                            intersections[2] = EonMathHelper.GetLineFromY(plane2[3], plane2[0], y);
                                            intersections[3] = EonMathHelper.GetLineFromY(plane2[2], plane2[1], y);

                                            count = 4;
                                        }
                                        else
                                        {
                                            intersections[2] = EonMathHelper.GetLineFromY(plane2[3], plane2[0], y);
                                            intersections[3] = EonMathHelper.GetLineFromY(plane2[3], plane2[2], y);
                                            intersections[4] = EonMathHelper.GetLineFromY(plane2[2], plane1[2], y);

                                            count = 5;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (plane2[2].Y > y)
                                {
                                    intersections[2] = EonMathHelper.GetLineFromY(plane2[3], plane1[3], y);
                                    intersections[3] = EonMathHelper.GetLineFromY(plane2[2], plane2[3], y);
                                    intersections[4] = EonMathHelper.GetLineFromY(plane2[2], plane2[1], y);

                                    count = 5;
                                }
                                else
                                {
                                    intersections[2] = EonMathHelper.GetLineFromY(plane2[3], plane1[3], y);
                                    intersections[3] = EonMathHelper.GetLineFromY(plane2[2], plane1[2], y);
                                    count = 4;
                                }
                            }

                            #endregion
                        }
                        else
                        {
                            intersections[1] = EonMathHelper.GetLineFromY(plane1[2], plane1[3], y);

                            #region Plane2 Projection

                            if (plane2[2].Y < y)
                            {
                                intersections[2] = EonMathHelper.GetLineFromY(plane2[2], plane1[2], y);
                                count = 3;
                            }
                            else
                            {
                                if (plane2[0].Y > y)
                                {
                                    intersections[2] = EonMathHelper.GetLineFromY(plane2[3], plane1[3], y);
                                    intersections[3] = EonMathHelper.GetLineFromY(plane2[0], plane1[0], y);
                                    intersections[4] = EonMathHelper.GetLineFromY(plane2[1], plane1[1], y);

                                    count = 5;
                                }
                                else
                                {
                                    if (plane2[3].Y > y)
                                    {
                                        intersections[2] = EonMathHelper.GetLineFromY(plane2[3], plane1[3], y);
                                        intersections[3] = EonMathHelper.GetLineFromY(plane2[3], plane2[0], y);

                                        count = 4;
                                    }
                                    else
                                    {
                                        intersections[2] = EonMathHelper.GetLineFromY(plane2[2], plane2[3], y);

                                        count = 3;
                                    }

                                    if (plane2[1].Y > y)
                                    {
                                        if (count == 4)
                                        {
                                            intersections[4] = EonMathHelper.GetLineFromY(plane2[1], plane2[0], y);
                                            intersections[5] = EonMathHelper.GetLineFromY(plane2[1], plane1[1], y);
                                        }
                                        else
                                        {
                                            intersections[3] = EonMathHelper.GetLineFromY(plane2[1], plane2[0], y);
                                            intersections[4] = EonMathHelper.GetLineFromY(plane2[1], plane1[1], y);
                                        }

                                        count += 2;
                                    }
                                    else
                                    {
                                        if (count == 4)
                                            intersections[4] = EonMathHelper.GetLineFromY(plane2[2], plane2[1], y);
                                        else
                                            intersections[3] = EonMathHelper.GetLineFromY(plane2[2], plane2[1], y);

                                        count++;
                                    }
                                }
                            }

                            #endregion
                        }
                    }
                    else
                    {
                        intersections[0] = EonMathHelper.GetLineFromY(plane1[3], plane1[2], y);
                        intersections[1] = EonMathHelper.GetLineFromY(plane1[3], plane1[0], y);

                        #region Plane2 Projection

                        if (plane2[3].Y < y)
                        {
                            intersections[2] = EonMathHelper.GetLineFromY(plane2[3], plane1[3], y);
                            count = 3;
                        }
                        else
                        {
                            if (plane2[1].Y > y)
                            {
                                intersections[2] = EonMathHelper.GetLineFromY(plane2[0], plane1[0], y);
                                intersections[3] = EonMathHelper.GetLineFromY(plane2[1], plane1[1], y);
                                intersections[4] = EonMathHelper.GetLineFromY(plane2[2], plane1[2], y);

                                count = 5;
                            }
                            else
                            {
                                if (plane2[0].Y > y)
                                {
                                    intersections[2] = EonMathHelper.GetLineFromY(plane2[0], plane1[0], y);
                                    intersections[3] = EonMathHelper.GetLineFromY(plane2[0], plane2[1], y);

                                    count = 4;
                                }
                                else
                                {
                                    intersections[2] = EonMathHelper.GetLineFromY(plane2[3], plane2[0], y);
                                    count = 3;
                                }

                                if (plane2[2].Y > y)
                                {
                                    if (count == 4)
                                    {
                                        intersections[4] = EonMathHelper.GetLineFromY(plane2[2], plane2[1], y);
                                        intersections[5] = EonMathHelper.GetLineFromY(plane2[2], plane1[2], y);
                                    }
                                    else
                                    {
                                        intersections[3] = EonMathHelper.GetLineFromY(plane2[2], plane2[1], y);
                                        intersections[4] = EonMathHelper.GetLineFromY(plane2[2], plane1[2], y);
                                    }

                                    count += 2;
                                }
                                else
                                {
                                    if (count == 4)
                                        intersections[4] = EonMathHelper.GetLineFromY(plane2[3], plane2[2], y);
                                    else
                                        intersections[3] = EonMathHelper.GetLineFromY(plane2[3], plane2[2], y);

                                    count++;
                                }
                            }
                        }

                        #endregion
                    }
                }
            }

            Vector2[]points = new Vector2[count];

            for (int i = 0; i < count; i++)
                points[i] = new Vector2(intersections[i].X, intersections[i].Z);

            return ViewClipShape.FromPoints(points);
        }

        ViewClipShape FromNoIntersection(float y,
            ClippingPlane plane1, ClippingPlane plane2)
        {
            Vector2[] points = new Vector2[4];

            for (int i = 0; i < points.Length; i++)
            {
                Vector3 line = EonMathHelper.GetLineFromY(plane1[i], plane2[i], y);
                points[i] = new Vector2(line.X, line.Z);
            }

            return ViewClipShape.FromPoints(points);
        }

        void AddCameraPos(ref ViewClipShape shape, Vector2 camPos)
        {
            int closest = 0;
            float closestDist = Vector2.Distance(camPos, shape[0]);

            for (int i = 0; i < shape.Count; i++)
            {
                float dist = Vector2.Distance(camPos, shape[i]);

                if (dist <= closestDist)
                    closestDist = i;
            }

            int adj1, adj2;

            if (closest == 0)
            {
                adj1 = shape.Count - 1;
                adj2 = closest + 1;
            }
            else if (closest == shape.Count - 1)
            {
                adj1 = closest - 1;
                adj2 = 0;
            }
            else
            {
                adj1 = closest - 1;
                adj2 = closest + 1;
            }

            float angle1 = EonMathHelper.LineAngle(
                shape[closest] - camPos, shape[adj1] - camPos);

            float angle2 = EonMathHelper.LineAngle(
                shape[closest] - camPos, shape[adj2] - camPos);

            if (angle1 < 90 && angle2 < 90)
                shape.ReplacePoint(closest, camPos);
            else
            {
                if (angle1 >= 90)
                    shape.InsertAt(closest, camPos);
                else
                    shape.InsertAt(adj2, camPos);
            }
        }

        #endregion
    }
}
