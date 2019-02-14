/* Created: 27/09/2014
 * Last Updated: 14/12/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering3D.Cameras;
using Eon.Rendering3D.Primatives.Constructs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering3D.Primatives
{
    /// <summary>
    /// Used to define 3 LOD for a SpherePrimative.
    /// </summary>
    public sealed class LODSphere : BaseLOD
    {
        BoundingSphere sphere;

        public override Vector3 Position
        {
            get { return base.Position; }
            set
            {
                base.Position = value;

                sphere.Center = value;
            }
        }

        public override Matrix World
        {
            get { return base.World; }
            set
            {
                base.World = value;

                ReConstructBounds();
            }
        }

        void ReConstructBounds()
        {
            Vector3 t;
            Vector3 scale;
            Quaternion rot;

            World.Decompose(out scale, out rot, out t);

            sphere = new BoundingSphere(t, scale.X);
        }

        /// <summary>
        /// Creates a new LODSphere.
        /// </summary>
        /// <param name="world">The world matrix of the LODSphere.</param>
        /// <param name="texture">The texture of the LODSphere.</param>
        public LODSphere(Matrix world, Texture2D texture)
            : base(world)
        {
            SetPrimatives(new Primative[]
            {
                new SpherePrimative(texture, 12),
                new SpherePrimative(texture, 6),
                new SpherePrimative(texture, 3)
            });

            ModelManager.Add(this);
        }

        /// <summary>
        /// Creates a new LODSphere.
        /// </summary>
        /// <param name="position">The position of the LODSphere.</param>
        /// <param name="scale">The scale of the LODSphere.</param>
        /// <param name="rotation">The rotation of the LODSphere.</param>
        /// <param name="textureFilepath">The filepath of the texture of the LODSphere.</param>
        public LODSphere(Vector3 position, Vector3 scale,
            Vector3 rotation, string textureFilepath)
            : base(position, scale, rotation)
        {
            SetPrimatives(new Primative[]
            {
                new SpherePrimative(textureFilepath, 12),
                new SpherePrimative(textureFilepath, 6),
                new SpherePrimative(textureFilepath, 3)
            });

            ModelManager.Add(this);
        }

        /// <summary>
        /// Creates a new LODSphere.
        /// </summary>
        /// <param name="position">The position of the LODSphere.</param>
        /// <param name="scale">The scale of the LODSphere.</param>
        /// <param name="rotation">The rotation of the LODSphere.</param>
        /// <param name="colour">The colour of the LODSphere.</param>
        public LODSphere(Vector3 position, Vector3 scale,
            Vector3 rotation, Color colour)
            : base(position, scale, rotation)
        {
            SetPrimatives(new Primative[]
            {
                new SpherePrimative(colour, 12),
                new SpherePrimative(colour, 6),
                new SpherePrimative(colour, 3)
            });

            ModelManager.Add(this);
        }

        /// <summary>
        /// Creates a new LODSphere.
        /// </summary>
        /// <param name="position">The position of the LODSphere.</param>
        /// <param name="scale">The scale of the LODSphere.</param>
        /// <param name="rotation">The rotation of the LODSphere.</param>
        /// <param name="textureFilepath">The filepath of the texture of the LODSphere.</param>
        public LODSphere(Vector3 position, Vector3 scale,
            Vector3 rotation, string textureFilepath, int lod1, int lod2, int lod3)
            : base(position, scale, rotation)
        {
            SetPrimatives(new Primative[]
            {
                new SpherePrimative(textureFilepath, lod1),
                new SpherePrimative(textureFilepath, lod2),
                new SpherePrimative(textureFilepath, lod3)
            });

            ModelManager.Add(this);
        }

        /// <summary>
        /// Creates a new LODSphere.
        /// </summary>
        /// <param name="world">The LODSphere's world matrix.</param>
        /// <param name="colour">The colour of the LODSphere.</param>
        public LODSphere(Matrix world, Color colour)
            : base(world)
        {
            SetPrimatives(new Primative[]
            {
                new SpherePrimative(colour, 12),
                new SpherePrimative(colour, 6),
                new SpherePrimative(colour, 3)
            });

            ModelManager.Add(this);
        }

        protected override bool IsInView()
        {
            return CameraManager.CurrentCamera.IsInView(sphere);
        }
    }
}
