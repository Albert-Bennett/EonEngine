/* Created: 27/09/2014
 * Last Updated: 14/12/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering3D.Cameras;
using Eon.Rendering3D.Primatives.Constructs;
using Eon.Testing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eon.Rendering3D.Primatives
{
    /// <summary>
    /// Used to define lod for a CubePrimative.
    /// </summary>
    public sealed class LODCube : BaseLOD
    {
        BoundingBox box;

        public override Vector3 Position
        {
            get { return base.Position; }
            set
            {
                base.Position = value;

                Vector3 difference = box.Max - box.Min;

                box.Min = box.Max = value;

                box.Max += difference;
            }
        }

        public override Matrix World
        {
            get { return base.World; }
            set
            {
                base.World = value;

                box = new BoundingBox(Vector3.Transform(Vector3.Zero, World),
                    Vector3.Transform(Vector3.One, World));
            }
        }

        /// <summary>
        /// Creates a new LODCube.
        /// </summary>
        /// <param name="position">The position of the LODCube.</param>
        /// <param name="scale">The scale of the LODCube.</param>
        /// <param name="rotation">The rotation of the LODCube.</param>
        /// <param name="textureFilepath">The filepath of the texture of the LODCube.</param>
        public LODCube(Vector3 position, Vector3 scale, Vector3 rotation, string textureFilepath)
            : base(position, scale, rotation)
        {
            SetPrimatives(new Primative[]
            {
                new CubePrimative(textureFilepath)
            });

            ModelManager.Add(this);
        }

        /// <summary>
        /// Creates a new LODCube.
        /// </summary>
        /// <param name="position">The position of the LODCube.</param>
        /// <param name="scale">The scale of the LODCube.</param>
        /// <param name="rotation">The rotation of the LODCube.</param>
        /// <param name="colour">The colour of the LODCube.</param>
        public LODCube(Vector3 position, Vector3 scale, Vector3 rotation, Color colour)
            : base(position, scale, rotation)
        {
            SetPrimatives(new Primative[]
            {
                new CubePrimative(colour)
            });

            ModelManager.Add(this);
        }

        /// <summary>
        /// Creates a new LODCube from a set of points.
        /// </summary>
        /// <param name="colour">The colour of the LODCube.</param>
        /// <param name="frustum">The frustum that will make up the LODCube</param>
        public LODCube(Color colour, BoundingFrustum frustum)
            : base(Matrix.Identity)
        {
            SetPrimatives(new Primative[]
            {
                new CubePrimative(colour, frustum)
            });

            ModelManager.Add(this);
        }

        /// <summary>
        /// Creates a new LODCube.
        /// </summary>
        /// <param name="world">The LODCube world matrix.</param>
        /// <param name="texture">The texture of the LODCube.</param>
        public LODCube(Matrix world, Texture2D texture)
            : base(world)
        {
            SetPrimatives(new Primative[]
            {
              new CubePrimative(texture)
            });

            ModelManager.Add(this);
        }

        /// <summary>
        /// Creates a new LODCube.
        /// </summary>
        /// <param name="world">The LODCube world matrix.</param>
        /// <param name="colour">The colour of the LODCube.</param>
        public LODCube(Matrix world, Color colour)
            : base(world)
        {
            SetPrimatives(new Primative[]
            {
                new CubePrimative(colour)
            });

            ModelManager.Add(this);
        }

        protected override bool IsInView()
        {
            return CameraManager.CurrentCamera.IsInView(box);
        }
    }
}
