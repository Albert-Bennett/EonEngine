/* Created 03/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Particles2D.Attachments
{
    /// <summary>
    /// Defines a set of properties accociated with Particles. 
    /// </summary>
    public class PropertySet
    {
        public float Rotation = 0;
        public float Scale = 1;
        public Color Colour = Color.White;
        public Color EffectColour = Color.White;

        public static PropertySet operator +(PropertySet value1, PropertySet value2)
        {
            return new PropertySet()
            {
                Rotation = value1.Rotation + value2.Rotation,
                Scale = value1.Scale * value2.Scale,
                Colour = AddColours(value1.Colour, value2.Colour),
                EffectColour = AddColours(value1.EffectColour, value2.EffectColour)
            };
        }

        static Color AddColours(Color color1, Color color2)
        {
            return new Color
                (color1.R + color2.R,
                color1.G + color2.G,
                color1.B + color2.B,
                color1.A + color2.A);
        }
    }
}
