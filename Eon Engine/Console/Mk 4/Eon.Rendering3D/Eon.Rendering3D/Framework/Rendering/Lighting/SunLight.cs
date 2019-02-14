/* Created: 29/03/2015
 * Last Updated: 22/06/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Rendering3D.Framework.Rendering.Lighting
{
    /// <summary>
    /// A light that is rendered the same as a 
    /// DirectonalLight but has extra properties 
    /// to achieve with Atmospheric Scattering 
    /// and Light Rays. 
    /// </summary>
    public sealed class SunLight : DirectionalLight
    {
        float harshness;

        Vector3 position;

        /// <summary>
        /// How harsh the SunLight is.
        /// </summary>
        public float Harshness
        {
            get { return harshness; }
            set { harshness = value; }
        }

        /// <summary>
        /// Creates a new SunLight.
        /// </summary>
        /// <param name="id">The id of the SunLight.</param>
        /// <param name="colour">The colour of the SunLight.</param>
        /// <param name="intensity">The intensity of the SunLight.</param>
        /// <param name="direction">The direction of the SunLight.</param>
        /// <param name="castsShadows">Whether or not this SunLight can cast shadows.</param>
        /// <param name="position">The position of the SunLight.</param>
        /// <param name="harshness">The harshness of the SunLight</param>
        public SunLight(string id, int r, int g, int b,
            float intensity, float directX, float directY, float directZ,
            bool castsShadows, float x, float y, float z, float harshness)
            : base(id, r, g, b, intensity,
            directX, directY, directZ, castsShadows)
        {
            this.position = new Vector3(x, y, z);
            this.harshness = harshness;
        }
    }
}
