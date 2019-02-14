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
    /// Used top define a 2D line.
    /// </summary>
    public struct Line2D
    {
        /// <summary>
        /// The Line2D starting position.
        /// </summary>
        public Vector2 StartPosition;

        /// <summary>
        /// The Line2D end position.
        /// </summary>
        public Vector2 EndPosition;

        /// <summary>
        /// The thickness of the Line2D.
        /// </summary>
        public float Thickness;

        /// <summary>
        /// Creates a new Line2D.
        /// </summary>
        /// <param name="startPosition">The Line2D starting position.</param>
        /// <param name="endPosition">The Line2D end position.</param>
        /// <param name="thickness">The thickness of the Line2D.</param>
        public Line2D(Vector2 startPosition, Vector2 endPosition, float thickness)
        {
            StartPosition = startPosition;
            EndPosition = endPosition;
            Thickness = thickness;
        }

        /// <summary>
        /// Gets the angle of the Line2D.
        /// </summary>
        /// <returns>The angle of the Line2D</returns>
        public float Angle()
        {
            Vector2 direct = EndPosition - StartPosition;
            float angle = (float)Math.Atan2(direct.Y, direct.X);

            return angle;
        }

        /// <summary>
        /// Gets the length of the Line2D tangent.
        /// </summary>
        /// <returns>The length of the Line2D tangent.</returns>
        public float TangentLength()
        {
            return (EndPosition - StartPosition).Length();
        }

        /// <summary>
        /// Gets the direction fo the Line2D.
        /// </summary>
        /// <returns>The direction of the Line2D.</returns>
        public Vector2 Direction()
        {
            return EndPosition - StartPosition;
        }
    }
}
