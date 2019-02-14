/* Created: 09/01/2014
 * Last Updated: 09/07/2015
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
        BoundingSphere sphere;

        /// <summary>
        /// The size of the light volume to be rendered.
        /// </summary>
        public float Radius
        {
            get { return World.Size.X / 2; }
            set { World.Size = new Vector3(value) * 2; }
        }

        /// <summary>
        /// A check to see if the PointLight
        /// is in view of the current CameraComponent.
        /// </summary>
        public override bool IsInView
        {
            get { return CameraManager.CurrentCamera.IsInView(sphere); }
        }

        /// <summary>
        /// Creates a new PointLight.
        /// </summary>
        /// <param name="id">The unique ID to give the PointLight.</param>
        /// <param name="position">The position of the PointLight.</param>
        /// <param name="colour">The colour of the PointLight.</param>
        /// <param name="radius">The radius of the PointLight.</param>
        public PointLight(string id, float x, float y, float z,
            int r, int g, int b, float intensity, float radius)
            : base(id, r, g, b, intensity)
        {
            World.Position = new Vector3(x, y, z);
            World.Size = new Vector3(radius);
        }

        protected override void Update()
        {
            ConstructBoundingSphere();

            base.Update();
        }

        void ConstructBoundingSphere()
        {
            sphere = new BoundingSphere(World.Position, World.Size.X);
        }
    }
}
