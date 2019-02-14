/* Created: 01/06/2013
 * Last Updated: 22/06/2015
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
        /// Creates a new PointLight.
        /// </summary>
        /// <param name="id">The ID of the PointLight.</param>
        /// <param name="colour">The colour of the PointLight.</param>
        /// <param name="intensity">The intensity of the light.</param>
        /// <param name="specPow">The specular power of the PointLight.</param>
        /// <param name="pos">The position of the center of the PointLight.</param>
        /// <param name="radius">The radius of the PointLight.</param>
        public PointLight2D(string id, int r, int g, int b,
            float intensity, float specPow, float x, float y, float z, int radius)
            : base(id, r, g, b, intensity, specPow)
        {
            World.Position = new Vector3(x, y, z);
            this.radius = radius;
        }
    }
}
