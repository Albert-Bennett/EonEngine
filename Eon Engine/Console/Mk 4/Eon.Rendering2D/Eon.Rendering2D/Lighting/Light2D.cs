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
    /// Defines an abstract class that each light inherits from.
    /// </summary>
    public abstract class Light2D : GameObject
    {
        Vector3 colour = Color.White.ToVector3();

        float intensity = 1;
        float specPow = 1;

        /// <summary>
        /// The colour of the Light.
        /// </summary>
        public Vector3 Colour
        {
            get { return colour; }
            set { colour = value; }
        }

        /// <summary>
        /// The intensity of the Light.
        /// </summary>
        public float Intensity
        {
            get { return intensity; }
            set
            {
                intensity = MathHelper.Clamp(value, 0.01f, 1f);
            }
        }

        /// <summary>
        /// The specular power of the Light. 
        /// </summary>
        public float SpecularPower
        {
            get { return specPow; }
            set { specPow = value; }
        }

        /// <summary>
        /// Creates a new Light.
        /// </summary>
        /// <param name="id">The ID of the Light.</param>
        /// <param name="colour">The colour of the Light.</param>
        /// <param name="intensity">The intensity of the Light.</param>
        /// <param name="specPow">The pecular power of the Light.</param>
        public Light2D(string id, int r, int g, int b, float intensity, float specPow)
            : base(id)
        {
            this.colour = new Vector3(r, g, b);
            Intensity = intensity;
        }

        protected override void Initialize()
        {
            LightingManager2D.Add(this);

            base.Initialize();
        }
    }
}
