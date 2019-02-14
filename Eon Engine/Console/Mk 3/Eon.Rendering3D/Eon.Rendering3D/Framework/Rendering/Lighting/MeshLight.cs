/* Created: 19/05/2014
 * Last Updated: 31/12/2014
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

        Vector3 position;

        /// <summary>
        /// The position of the MeshLight.
        /// </summary>
        public Vector3 Position
        {
            get
            {
                if (Owner != null)
                    return Owner.World.Position + position;

                return position;
            }
            set 
            {
                ConstructBoundingSphere();
                position = value;
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
                Matrix baseTransform = Matrix.Identity;

                if (Owner != null)
                    baseTransform = Owner.World.Matrix;

                return Matrix.CreateScale(size) *
                    Matrix.CreateTranslation(position + baseTransform.Translation);
            }
            set
            {
                Vector3 t;
                Vector3 scale;
                Quaternion rot;

                value.Decompose(out scale, out rot, out t);

                size = (scale.X + scale.Y + scale.Z) / 3;
                position = t;

                ConstructBoundingSphere();
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
                    pos += Owner.World.Position;

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
            : base(id, colour, intensity)
        {
            this.size = size;
            this.position = position;

            volume = Common.ContentBuilder.Load<Model>(modelFilepath);

            ConstructBoundingSphere();
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
        public MeshLight(string id, Vector3 position, 
            Color colour, float intensity, float size,
            string modelFilepath)
            : base(id, colour)
        {
            this.size = size;
            this.position = position;

            volume = Common.ContentBuilder.Load<Model>(modelFilepath);

            ConstructBoundingSphere();
        }

        void ConstructBoundingSphere()
        {
            Vector3 pos = position;

            if (Owner != null)
                pos += Owner.World.Position;

            sphere = new BoundingSphere(pos, size);
        }
    }
}
