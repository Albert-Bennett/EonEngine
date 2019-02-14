/* Created 10/03/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Helpers
{
    /// <summary>
    /// Defines a set of methods used to help in calculating math with rectangles.
    /// </summary>
    public sealed class RectangleHelper
    {
        /// <summary>
        /// A check to see if a point is inside of a given rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle to use.</param>
        /// <param name="position">The position to be checked.</param>
        /// <returns>The result of the check.</returns>
        public static bool IsInsideOf(Rectangle rectangle, Vector2 position)
        {
            if (position.X <= rectangle.X + rectangle.Width && position.X >= rectangle.X)
                if (position.Y <= rectangle.Y + rectangle.Height && position.Y >= rectangle.Y)
                    return true;

            return false;
        }

        /// <summary>
        /// Transforms a rectangle using a matrix.
        /// </summary>
        /// <param name="rectangle">The rectangle to be transformed.</param>
        /// <param name="matrix">The matrix to use to transform.</param>
        /// <returns>The transformed rectangle.</returns>
        public static Rectangle Transform(Rectangle rectangle, Matrix matrix)
        {
            Vector3 translation = matrix.Translation;
            Vector3 scale = new Vector3();

            EonMathHelper.GetMatrixScale(matrix, out scale);

            rectangle.X += (int)translation.X;
            rectangle.Y += (int)translation.Y;
            rectangle.Width = (int)((float)rectangle.Width * scale.X);
            rectangle.Height = (int)((float)rectangle.Height * scale.Y);

            return rectangle;
        }
    }
}
