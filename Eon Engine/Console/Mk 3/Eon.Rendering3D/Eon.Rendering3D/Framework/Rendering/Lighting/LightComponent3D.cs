/* Created: 15/12/2013
 * Last Updated: 24/02/2015
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
    public abstract class LightComponent3D : ObjectComponent
    {
        protected Vector3 direct;
        protected Vector3 colour;
        protected float intensity;

        Matrix world = Matrix.Identity;

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
            set
            {
                value = MathHelper.Clamp(value, 0.0f, 1.0f);

                intensity = value;
            }
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
        /// The world matrix of the LightComponent3D.
        /// </summary>
        public virtual Matrix World
        {
            get { return world; }
            set { world = value; }
        }

        /// <summary>
        /// Creates a new LightComponent.
        /// </summary>
        /// <param name="id">The unique ID to give the LightComponent.</param>
        /// <param name="colour">The colour of the LightComponent.</param>
        public LightComponent3D(string id, Color colour)
            : base(id)
        {
            this.colour = colour.ToVector3();
            this.intensity = colour.A / 255.0f;
        }

        protected override void Attached()
        {
            LightManager.Add(this);
        }

        /// <summary>
        /// Creates a new LightComponent.
        /// </summary>
        /// <param name="id">The unique ID to give the LightComponent.</param>
        /// <param name="colour">The colour of the LightComponent.</param>
        /// <param name="intensity">The intensity of the LightComponent.</param>
        public LightComponent3D(string id, Vector3 colour, float intensity)
            : base(id)
        {
            this.colour = colour;
            this.intensity = intensity;
        }

        public override void Destroy(bool remove)
        {
            LightManager.Remove(this.ID);

            base.Destroy(remove);
        }
    }
}
