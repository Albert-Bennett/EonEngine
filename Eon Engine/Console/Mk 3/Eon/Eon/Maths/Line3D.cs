/* Created: 03/09/2014
 * Last Updated: 04/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;
using System;

namespace Eon.Maths
{
    /// <summary>
    /// Used top define a 3D line.
    /// </summary>
    public struct Line3D
    {
        /// <summary>
        /// The Line3D starting position.
        /// </summary>
        public Vector3 StartPosition;

        /// <summary>
        /// The Line3D end position.
        /// </summary>
        public Vector3 EndPosition;

        /// <summary>
        /// The thickness of the Line3D.
        /// </summary>
        public float Thickness;

        /// <summary>
        /// Creates a new Line3D.
        /// </summary>
        /// <param name="startPosition">The Line3D starting position.</param>
        /// <param name="endPosition">The Line3D end position.</param>
        /// <param name="thickness">The thickness of the Line3D.</param>
        public Line3D(Vector3 startPosition, Vector3 endPosition, float thickness)
        {
            StartPosition = startPosition;
            EndPosition = endPosition;
            Thickness = thickness;
        }

        /// <summary>
        /// Gets the angle of the Line3D.
        /// </summary>
        /// <returns>The angle of the Line3D</returns>
        public float Angle()
        {
            Vector3 direct = EndPosition - StartPosition;
            return (float)Math.Atan2(direct.Y, direct.X);
        }

        /// <summary>
        /// Gets the length of the Line3D tangent.
        /// </summary>
        /// <returns>The length of the Line3D tangent.</returns>
        public float TangentLength()
        {
            return (EndPosition - StartPosition).Length();
        }

        /// <summary>
        /// Gets the direction fo the Line3D.
        /// </summary>
        /// <returns>The direction of the Line3D.</returns>
        public Vector3 Direction()
        {
            return EndPosition - StartPosition;
        }
    }
}
