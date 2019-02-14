/* Created 19/05/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Rendering3D.Framework.Rendering.Lighting
{
    /// <summary>
    /// Used to define a light that shines 
    /// infinatly in a single direction 
    /// from a single point.
    /// </summary>
    public sealed class DirectionalLight : LightComponent3D
    {
        /// <summary>
        /// The world matrix of the DirectionalLight.
        /// </summary>
        public override Matrix World { get { return Matrix.Identity; } }

        /// <summary>
        /// Creates a new DirectionalLight.
        /// </summary>
        /// <param name="id">The id of the DirectionalLight.</param>
        /// <param name="position">The position of the DirectionalLight.</param>
        /// <param name="colour">The colour of the DirectionalLight.</param>
        /// <param name="intensity">The intensity of the DirectionalLight.</param>
        /// <param name="direction">The direction of the DirectionalLight.</param>
        /// <param name="castsShadows">Wheather or not this DirectionalLight can cast shadows.</param>
        public DirectionalLight(string id,
            Vector3 colour, float intensity, Vector3 direction)
            : base(id, Vector3.Zero, colour, intensity)
        {
            this.direct = Vector3.Normalize(direction);
        }
    }
}
