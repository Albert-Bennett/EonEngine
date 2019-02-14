/* Created 02/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Physics2D.PerPixel
{
    /// <summary>
    /// Defines a struct that is used to detect pre pixel collisions.
    /// </summary>
    public struct PerPixelObject
    {
        public Texture2D Texture;
        public Rectangle Bounds;

        public Color[] ColourBits;

        /// <summary>
        /// Creates a new PerPixelObject.
        /// </summary>
        /// <param name="texture">The texture of the PerPixelObject.</param>
        /// <param name="bounds">The bounding area of the PerPixelObject.</param>
        public PerPixelObject(Texture2D texture, Rectangle bounds)
        {
            Texture = texture;
            Bounds = bounds;

            ColourBits = new Color[texture.Width * texture.Height];
            texture.GetData(ColourBits);
        }
    }
}
