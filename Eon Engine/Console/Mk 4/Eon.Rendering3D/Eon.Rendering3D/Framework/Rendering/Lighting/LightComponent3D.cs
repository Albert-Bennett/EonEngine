/* Created: 15/12/2013
 * Last Updated: 22/06/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Rendering3D.Framework.Rendering.Lighting
{
    /// <summary>
    /// Used to define a light in 3D space.
    /// </summary>
    public abstract class LightComponent3D : GameObject
    {
        protected Vector3 colour;
        protected float intensity;

        /// <summary>
        /// The colour of the Light.
        /// </summary>
        public Vector3 Colour
        {
            get { return colour; }
            set { colour = value; }
        }

        /// <summary>
        /// The intensity of the LightComponent3D.
        /// </summary>
        public float Intensity
        {
            get { return intensity; }
            set { intensity = value; }
        }

        /// <summary>
        /// A check to see if the LightComponent3D
        /// is in view of the current CameraComponent.
        /// </summary>
        public virtual bool IsInView
        {
            get { return true; }
        }

        /// <summary>
        /// Creates a new LightComponent.
        /// </summary>
        /// <param name="id">The unique ID to give the LightComponent.</param>
        /// <param name="colour">The colour of the LightComponent.</param>
        public LightComponent3D(string id, int r, int g, int b, float intensity)
            : base(id)
        {
            this.colour = new Vector3(r, g, b) / new Vector3(255);
            this.intensity = intensity;

            LightManager.Add(this);
        }

        public override void Destroy()
        {
            LightManager.Remove(this.ID);

            base.Destroy();
        }
    }
}
