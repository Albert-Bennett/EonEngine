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
    /// Used to define 3 LOD for CylinderPrimative. 
    /// </summary>
    public sealed class LODCylinder : BaseLOD
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
        /// Creates a new LODCylinder.
        /// </summary>
        /// <param name="world">The world matrix of the LODCylinder.</param>
        /// <param name="texture">The texture of the LODCylinder.</param>
        public LODCylinder(Matrix world, Texture2D texture)
            : base(world)
        {
            SetPrimatives( new Primative[]
            {
                new CylinderPrimative(texture, 12),
                new CylinderPrimative(texture, 6),
                new CylinderPrimative(texture, 3)
            });

            ModelManager.Add(this);
        }

        /// <summary>
        /// Creates a new LODCylinder.
        /// </summary>
        /// <param name="position">The position of the LODCylinder.</param>
        /// <param name="scale">The scale of the LODCylinder.</param>
        /// <param name="rotation">The rotation of the LODCylinder.</param>
        /// <param name="textureFilepath">The filepath of the texture of the LODCylinder.</param>
        public LODCylinder(Vector3 position, Vector3 scale,
            Vector3 rotation, string textureFilepath)
            : base(position, scale, rotation)
        {
            SetPrimatives(new Primative[]
            {
                new CylinderPrimative(textureFilepath, 12),
                new CylinderPrimative(textureFilepath, 6),
                new CylinderPrimative(textureFilepath, 3)
            });

            ModelManager.Add(this);
        }

        /// <summary>
        /// Creates a new LODCylinder.
        /// </summary>
        /// <param name="position">The position of the LODCylinder.</param>
        /// <param name="scale">The scale of the LODCylinder.</param>
        /// <param name="rotation">The rotation of the LODCylinder.</param>
        /// <param name="textureFilepath">The filepath of the texture of the LODCylinder.</param>
        public LODCylinder(Vector3 position, Vector3 scale,
            Vector3 rotation, string textureFilepath, int lod1, int lod2, int lod3)
            : base(position, scale, rotation)
        {
            SetPrimatives(new Primative[]
            {
                new CylinderPrimative(textureFilepath, lod1),
                new CylinderPrimative(textureFilepath, lod2),
                new CylinderPrimative(textureFilepath, lod3)
            });

            ModelManager.Add(this);
        }

        /// <summary>
        /// Creates a new LODCylinder.
        /// </summary>
        /// <param name="world">The LODCylinder's world matrix.</param>
        /// <param name="colour">The colour of the LODCylinder.</param>
        public LODCylinder(Matrix world, Color colour)
            : base(world)
        {
            SetPrimatives(new Primative[]
            {
                new CylinderPrimative(colour, 12),
                new CylinderPrimative(colour, 6),
                new CylinderPrimative(colour, 3)
            });

            ModelManager.Add(this);
        }

        protected override bool IsInView()
        {
            return CameraManager.CurrentCamera.IsInView(box);
        }
    }
}
