/* Created 15/12/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Rendering3D.Lighting
{
    /// <summary>
    /// Used to define a light in 3D space.
    /// </summary>
    public abstract class LightComponent3D : ObjectComponent
    {
        protected Vector3 position;
        protected Vector3 colour;

        /// <summary>
        /// The position of the Light.
        /// </summary>
        public Vector3 Position
        {
            get
            {
                if (Owner != null)
                    return Owner.World.Translation + position;

                return position;
            }
        }

        /// <summary>
        /// The colour of the Light.
        /// </summary>
        public Vector3 Colour
        {
            get { return colour; }
        }

        /// <summary>
        /// Creates a new LightComponent.
        /// </summary>
        /// <param name="id">The unique ID to give the LightComponent.</param>
        /// <param name="position">The position of the LightComponent.</param>
        /// <param name="colour">The colour of the LightComponent.</param>
        public LightComponent3D(string id,
            Vector3 position, Vector3 colour):base(id)
        {
            this.colour = colour;
            this.position = position;

            LightManager.Add(this);
        }

        public override void Destroy()
        {
            LightManager.Remove(this);

            base.Destroy();
        }
    }
}
