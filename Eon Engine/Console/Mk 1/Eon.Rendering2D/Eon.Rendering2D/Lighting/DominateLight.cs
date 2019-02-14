/* Created 01/06/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Rendering2D.Lighting
{
    /// <summary>
    /// Used to define a light that appers to 
    /// be emmitting light from everywhere.
    /// </summary>
    public class DominateLight : Light
    {
        Vector3 direct = Vector3.Zero;

        /// <summary>
        /// The direction of the DominateLight.
        /// </summary>
        public Vector3 Direction
        {
            get { return direct; }
            set { direct = Vector3.Normalize(value); }
        }

        /// <summary>
        /// Creates a new DominateLight.
        /// </summary>
        /// <param name="id">The ID of the DominateLight.</param>
        /// <param name="colour">The colour of the DominateLight.</param>
        /// <param name="intensity">The intensity of the light.</param>
        /// <param name="specPow">The specular power of the light.</param>
        /// <param name="direction">The direction of the light.</param>
        public DominateLight(string id, Vector3 colour,
            float intensity, float specPow, Vector3 direction)
            : base(id, colour, intensity, specPow)
        {
            direct = direction;
        }
    }
}
