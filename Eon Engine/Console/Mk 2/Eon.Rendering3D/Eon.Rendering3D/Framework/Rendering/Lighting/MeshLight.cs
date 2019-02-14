/* Created 19/05/2014
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
        float size;

        Model volume;
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
        public float Size
        {
            get { return size; }
            set
            {
                ConstructBoundingSphere();

                size = value;
            }
        }

        /// <summary>
        /// The model that is rendered to apply the lighting.
        /// </summary>
        public Model MeshVolume
        {
            get { return volume; }
            set { volume = value; }
        }

        /// <summary>
        /// The world matrix of the MeshLight.
        /// </summary>
        public override Matrix World
        {
            get
            {
                Matrix offset = Matrix.Identity;

                if (Owner != null)
                    offset = Owner.World.Matrix;

                return Matrix.CreateScale(size) *
                    Matrix.CreateTranslation(position + offset.Translation);
            }
        }

        /// <summary>
        /// A check to see if the MeshLight
        /// is in view of the current CameraComponent.
        /// </summary>
        public override bool IsInView
        {
            get
            {
                Vector3 pos = position;

                if (Owner != null)
                    pos += Owner.World.Translation;

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
        /// <param name="colour">The colour of the MeshLight.</param>
        /// <param name="intensity">The intensity of the MeshLight.</param>
        /// <param name="size">The size of the MeshLight.</param>
        /// <param name="modelFilepath">The file path for the model to be used 
        /// when rendering this Meshlight.</param>
        public MeshLight(string id, Vector3 position,
            Vector3 colour, float intensity,
            float size, string modelFilepath)
            : base(id, position, colour, intensity)
        {
            this.size = size;

            volume = Common.ContentManager.Load<Model>(modelFilepath);

            ConstructBoundingSphere();
        }

        void ConstructBoundingSphere()
        {
            Vector3 pos = position;

            if (Owner != null)
                pos += Owner.World.Translation;

            sphere = new BoundingSphere(pos, size);
        }
    }
}
