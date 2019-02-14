/* Created 03/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Particles2D.Attachments
{
    /// <summary>
    /// Used to define the state of a Colour.
    /// </summary>
    struct ColourState
    {
        public byte ColourDecay;
        public Color Colour;
        public Color EffectColour;

        public void Update()
        {
            if (Colour.R != 0)
                Colour.R -= (byte)ColourDecay;
            else if (Colour.R < 0)
                Colour.R = 0;

            if (Colour.G != 0)
                Colour.G -= (byte)ColourDecay;
            else if (Colour.G < 0)
                Colour.G = 0;

            if (Colour.B != 0)
                Colour.B -= (byte)ColourDecay;
            else if (Colour.B < 0)
                Colour.B = 0;

            if (Colour.A != 0)
                Colour.A -= (byte)ColourDecay;
            else if (Colour.A < 0)
                Colour.A = 0;

            if (Colour != Color.Transparent)
            {
                float avg = (Colour.R + Colour.G + Colour.B) / 3;

                EffectColour = new Color(avg, avg, avg, Colour.A);
            }
        }
    }
}
