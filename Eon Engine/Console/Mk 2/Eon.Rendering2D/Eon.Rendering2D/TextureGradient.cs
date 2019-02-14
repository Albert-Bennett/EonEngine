/* Created 15/01/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering2D
{
    /// <summary>
    /// Used to define a gradient comprised of a texture instead.
    /// </summary>
    public struct TextureGradient
    {
        Color[] colours;

        int maxPos;

        /// <summary>
        /// The maximum position of values in the gradient.
        /// </summary>
        public int MaxPosition
        {
            get { return maxPos; }
        }

        /// <summary>
        /// Creates a new TextureGradient.
        /// </summary>
        /// <param name="textureFilepath">The file path of the Texture
        /// to use in the TextureGradient.</param>
        public TextureGradient(string textureFilepath)
        {
            Texture2D tex = Common.ContentManager.Load<Texture2D>(textureFilepath);

            colours = new Color[tex.Width * tex.Height];

            tex.GetData<Color>(colours);

            maxPos = tex.Width;
        }

        /// <summary>
        /// Creates a new TextureGradient.
        /// </summary>
        /// <param name="texture">The Texture to use in the TextureGradient.</param>
        public TextureGradient(Texture2D texture)
        {
            colours = new Color[texture.Width * texture.Height];

            texture.GetData<Color>(colours);

            maxPos = texture.Width;
        }

        /// <summary>
        /// Gets a colour of the gradient at a given position.
        /// </summary>
        /// <param name="position">The position of the colour to get.</param>
        /// <returns>The found colour.</returns>
        public Color GetColour(int position)
        {
            Color c = Color.Transparent;

            if (position <= maxPos && position >= 0)
                c = colours[position];

            return c;
        }
    }
}
