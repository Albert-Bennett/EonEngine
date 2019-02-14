/* Created 09/01/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering3D.Cameras;
using Microsoft.Xna.Framework;

namespace Eon.Rendering3D.Framework.Rendering.Lighting
{
    /// <summary>
    /// Used to define a point light in 3D space.
    /// </summary>
    public sealed class PointLight : LightComponent3D
    {
        float radius;

        BoundingSphere sphere;

        /// <summary>
        /// The position of the MeshLight.
        /// </summary>
        public override Vector3 Position
        {
            get
            {
                return base.Position;
            }
            set
            {
                ConstructBoundingSphere();

                base.Position = value;
            }
        }

        /// <summary>
        /// The size of the light volume to be rendered.
        /// </summary>
        public float Radius
        {
            get { return radius; }
            set
            {
                ConstructBoundingSphere();

                radius = value;
            }
        }

        public override void ToogleEnable()
        {
            base.ToogleEnable();
        }

        /// <summary>
        /// The world matrix of the PointLight.
        /// </summary>
        public override Matrix World
        {
            get
            {
                Matrix offset = Matrix.Identity;

                if (Owner != null)
                    offset = Owner.World.Matrix;

                return Matrix.CreateScale(radius) *
                    Matrix.CreateTranslation(position + offset.Translation);
            }
        }

        /// <summary>
        /// A check to see if the PointLight
        /// is in view of the current CameraComponent.
        /// </summary>
        public override bool IsInView
        {
            get
            {
                Vector3 pos = position;

                if (Owner != null)
                    pos += Owner.World.Translation;

                BoundingSphere sphere = new BoundingSphere(pos, radius);

                return CameraManager.CurrentCamera.IsInView(sphere);
            }
        }

        /// <summary>
        /// Creates a new PointLight.
        /// </summary>
        /// <param name="id">The unique ID to give the PointLight.</param>
        /// <param name="position">The position of the PointLight.</param>
        /// <param name="colour">The colour of the PointLight.</param>
        /// <param name="intensity">The intensity of the PointLight.</param>
        /// <param name="radius">The radius of the PointLight.</param>
        public PointLight(string id, Vector3 position,
            Vector3 colour, float intensity, float radius)
            : base(id, position, colour, intensity)
        {
            this.radius = radius;

            ConstructBoundingSphere();
        }

        void ConstructBoundingSphere()
        {
            Vector3 pos = position;

            if (Owner != null)
                pos += Owner.World.Translation;

            sphere = new BoundingSphere(pos, radius);
        }
    }
}
