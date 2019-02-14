/* Created: 01/06/2013
 * Last Updated; 22/06/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Rendering2D.Lighting
{
    /// <summary>
    /// Used to define a light that appers to 
    /// be emmitting light from everywhere.
    /// </summary>
    public class DominateLight2D : Light2D
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
        public DominateLight2D(string id, int r, int g, int b,
            float intensity, float specPow, float directX, float directY, float directZ)
            : base(id, r, g, b, intensity, specPow)
        {
            direct = new Vector3(directX, directY, directZ);
        }
    }
}
