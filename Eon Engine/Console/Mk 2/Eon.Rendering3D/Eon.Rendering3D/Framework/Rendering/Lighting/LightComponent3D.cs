/* Created 15/12/2013
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
        protected Vector3 position;
        protected Vector3 direct;
        protected Vector3 colour;
        protected float intensity;

        Matrix world;

        /// <summary>
        /// The position of the Light.
        /// </summary>
        public virtual Vector3 Position
        {
            get
            {
                if (Owner != null)
                    return Owner.World.Translation + position;

                return position;
            }
            set { position = value; }
        }

        /// <summary>
        /// The direction that the LigtingComponent3D is facing.
        /// </summary>
        public virtual Vector3 Direction
        {
            get { return direct; }
            set { direct = value; }
        }

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
        }

        /// <summary>
        /// Creates a new LightComponent.
        /// </summary>
        /// <param name="id">The unique ID to give the LightComponent.</param>
        /// <param name="position">The position of the LightComponent.</param>
        /// <param name="colour">The colour of the LightComponent.</param>
        /// <param name="intensity">The intensity of the LightComponent.</param>
        /// <param name="castShadows">Wheather or not this LightComponent can cast shadows.</param>
        public LightComponent3D(string id, Vector3 position,
            Vector3 colour, float intensity)
            : base(id)
        {
            this.colour = colour;
            this.position = position;
            this.intensity = intensity;

            LightManager.Add(this);
        }

        public override void Destroy(bool remove)
        {
            LightManager.Remove(this.ID);

            base.Destroy(remove);
        }
    }
}
