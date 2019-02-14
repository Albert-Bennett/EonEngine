/* Created 01/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Rendering2D.Lighting
{
    /// <summary>
    /// Defines a light that is emitted
    /// within the limits of a radius.
    /// </summary>
    public class PointLight2D : Light2D
    {
        Vector3 pos;

        int radius = 2;

        /// <summary>
        /// The radius of the PoitnLight.
        /// </summary>
        public int Radius
        {
            get { return radius; }
            set { radius = value; }
        }

        /// <summary>
        /// The center of the PointLight.
        /// </summary>
        public Vector3 Pos
        {
            get
            {
                if (Owner != null)
                    return pos + Owner.World.Translation;
                else
                    return pos;
            }
            set { pos = value; }
        }

        /// <summary>
        /// Creates a new PointLight.
        /// </summary>
        /// <param name="id">The ID of the PointLight.</param>
        /// <param name="colour">The colour of the PointLight.</param>
        /// <param name="intensity">The intensity of the light.</param>
        /// <param name="specPow">The specular power of the PointLight.</param>
        /// <param name="pos">The position of the center of the PointLight.</param>
        /// <param name="radius">The radius of the PointLight.</param>
        public PointLight2D(string id, Vector3 colour,
            float intensity, float specPow, Vector3 pos, int radius)
            : base(id, colour, intensity, specPow)
        {
            this.pos = pos;
            this.radius = radius;
        }
    }
}
