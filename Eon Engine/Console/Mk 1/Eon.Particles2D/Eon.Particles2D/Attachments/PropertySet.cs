/* Created 03/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
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
                Colour = value2.Colour,
                EffectColour = value2.EffectColour
            };
        }
    }
}
