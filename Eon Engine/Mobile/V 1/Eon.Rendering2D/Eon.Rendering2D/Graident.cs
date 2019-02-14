/* Created 18/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Rendering2D
{
    /// <summary>
    /// Used to define a gradient.
    /// </summary>
    public struct Graident
    {
        Color startColour;
        Color endColour;
        byte alpha;

        /// <summary>
        /// Creates a new Gradient object.
        /// </summary>
        /// <param name="alpha">The value for alpha.</param>
        /// <param name="startColour">The colour at the start of the Gradient.</param>
        /// <param name="endColour">The colour at the end of the Gradient.</param>
        public Graident(byte alpha, Color startColour, Color endColour)
        {
            this.alpha = alpha;

            this.startColour = startColour;
            this.endColour = endColour;
        }

        /// <summary>
        /// Gets the colour of the Gradient at a specific position. (between 0 -> 1) 
        /// </summary>
        /// <param name="position"></param>
        /// <returns>The colour at that position.</returns>
        public Color GetGradient(float position)
        {
            Color c = Color.Lerp(startColour, endColour, position);
            c.A = alpha;

            return c;
        }
    }
}
