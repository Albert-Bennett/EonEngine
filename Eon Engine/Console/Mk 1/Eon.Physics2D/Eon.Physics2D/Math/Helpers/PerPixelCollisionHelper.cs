/* Created 02/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Physics2D.PerPixel;
using Eon.Helpers;
using Microsoft.Xna.Framework;

namespace Eon.Physics2D.Math.Helpers
{
    /// <summary>
    /// A helper class used to calculate per pixel collisions.
    /// </summary>
    public static class PerPixelCollisionHelper
    {
        /// <summary>
        /// Used to calculate collisions between 
        /// PerPixelObjects.
        /// </summary>
        /// <param name="object1">PerPixelObject 1.</param>
        /// <param name="object2">PerPixelObject 2.</param>
        /// <returns>The result of the check.</returns>
        public static bool PerPixelCollision(
            PerPixelObject object1, PerPixelObject object2)
        {
            Vector2 poc = Vector2.Zero;

            if (CollisionShapeHelper.Collides(object1.Bounds, object2.Bounds, out poc) != CollisionType.None)
            {
                int x1 = (int)EonMathHelper.Max(object1.Bounds.X, object2.Bounds.X);
                int x2 = (int)EonMathHelper.Min(object1.Bounds.Right, object2.Bounds.Right);
                int y1 = (int)EonMathHelper.Max(object1.Bounds.Y, object2.Bounds.Y);
                int y2 = (int)EonMathHelper.Min(object1.Bounds.Bottom, object2.Bounds.Bottom);

                for (int y = y1; y < y2; y++)
                    for (int x = x1; x < x2; x++)
                    {
                        Color colour1 = object1.ColourBits[(x - object1.Bounds.X) +
                            (y - object1.Bounds.Y) * object1.Texture.Width];

                        Color colour2 = object2.ColourBits[(x - object2.Bounds.X) +
                            (y - object2.Bounds.Y) * object2.Texture.Width];

                        if (colour1.A != 0 && colour2.A != 0)
                            return true;
                    }
            }

            return false;
        }
    }
}
