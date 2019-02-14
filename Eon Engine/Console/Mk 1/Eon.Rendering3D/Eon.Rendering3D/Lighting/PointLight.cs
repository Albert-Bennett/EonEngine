/* Created 09/01/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Rendering3D.Lighting
{
    /// <summary>
    /// Used to define a point light in 3D space.
    /// </summary>
    public class PointLight : LightComponent3D
    {
        protected float radius;

        /// <summary>
        /// The radius of the PointLight.
        /// </summary>
        public float Radius
        {
            get { return radius; }
        }

        /// <summary>
        /// Creates a new PointLight.
        /// </summary>
        /// <param name="id">The unique ID to give the PointLight.</param>
        /// <param name="position">The position of the PointLight.</param>
        /// <param name="colour">The colour of the PointLight.</param>
        /// <param name="radius">The radius of the PointLight.</param>
        public PointLight(string id, Vector3 position,
            Vector3 colour, float radius)
            : base(id, position, colour)
        {
            this.radius = radius;
        }
    }
}
