/* Created 04/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Particles2D.Attachments
{
    /// <summary>
    /// Used to define the state of a lerping colour.
    /// </summary>
    struct ColourLerpState
    {
        public float LerpRate;
        public Color Colour;
        public Color ToLerpTo;

        public void Update()
        {
            Colour = Color.Lerp(Colour, ToLerpTo, LerpRate);
        }
    }
}
