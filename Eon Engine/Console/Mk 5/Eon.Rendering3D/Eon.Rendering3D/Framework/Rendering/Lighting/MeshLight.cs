/* Created: 19/05/2014
 * Last Updated: 22/06/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering3D.Cameras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering3D.Framework.Rendering.Lighting
{
    /// <summary>
    /// Used to define a light which is rendered using a mesh.
    /// </summary>
    public sealed class MeshLight : LightComponent3D
    {
        Model volume;
        BoundingSphere sphere;

        /// <summary>
        /// The model that is rendered to apply the lighting.
        /// </summary>
        public Model MeshVolume
        {
            get { return volume; }
            set { volume = value; }
        }

        /// <summary>
        /// A check to see if the MeshLight
        /// is in view of the current CameraComponent.
        /// </summary>
        public override bool IsInView
        {
            get
            {
                Vector3 pos = World.Position;
                float size = World.Size.X;

                BoundingSphere sphere = new BoundingSphere();

                foreach (ModelMesh mesh in volume.Meshes)
                {
                    BoundingSphere meshSphere = mesh.BoundingSphere;

                    meshSphere.Center += pos;
                    meshSphere.Radius *= size;

                    sphere = BoundingSphere.CreateMerged(sphere, meshSphere);
                }

                return CameraManager.CurrentCamera.IsInView(sphere);
            }
        }

        /// <summary>
        /// Creates a new Meshlight.
        /// </summary>
        /// <param name="id">The id of the MeshLight.</param>
        /// <param name="position">The position of the MeshLight.</param>
        /// <param name="colour">The colour of the MeshLight</param>
        /// <param name="intensity">The intensity of the MeshLight.</param>
        /// <param name="size">The size of the MeshLight.</param>
        /// <param name="modelFilepath">The file path for the model to be used 
        /// when rendering this Meshlight.</param>
        public MeshLight(string id, float x, float y, float z,
            int r, int g, int b, float intensity, float size,
            string modelFilepath)
            : base(id, r, g, b, intensity)
        {
            World.Position = new Vector3(x, y, z);
            World.Size = new Vector3(size);

            volume = Common.ContentBuilder.Load<Model>(modelFilepath);

            ConstructBoundingSphere();
        }

        protected override void Update()
        {
            ConstructBoundingSphere();

            base.Update();
        }

        void ConstructBoundingSphere()
        {
            Vector3 pos = World.Position;

            sphere = new BoundingSphere(World.Position, World.Size.X);
        }
    }
}
