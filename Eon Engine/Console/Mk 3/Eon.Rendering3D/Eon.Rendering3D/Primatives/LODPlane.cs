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
    /// Used to define various LOD for a PlanePrimative.
    /// </summary>
    public sealed class LODPlane : BaseLOD
    {
        /// <summary>
        /// Creates a new LODPlane.
        /// </summary>
        /// <param name="position">The position of the LODPlane.</param>
        /// <param name="scale">The scale of the LODPlane.</param>
        /// <param name="rotation">The rotation of the LODPlane.</param>
        /// <param name="textureFilepath">The filepath of the texture of the LODPlane.</param>
        public LODPlane(Vector3 position, Vector3 scale, Vector3 rotation, string textureFilepath)
            : base(position, scale, rotation)
        {
            SetPrimatives(new Primative[]
            {
                new PlanePrimative(textureFilepath)
            });

            ModelManager.Add(this);
        }

        /// <summary>
        /// Creates a new LODPlane.
        /// </summary>
        /// <param name="world">The PlanePrimatives world matrix.</param>
        /// <param name="texture">The texture of the LODPlane.</param>
        public LODPlane(Matrix world, Texture2D texture)
            : base(world)
        {
            SetPrimatives(new Primative[]
            {
              new PlanePrimative(texture)
            });

            ModelManager.Add(this);
        }

        /// <summary>
        /// Creates a new LODPlane.
        /// </summary>
        /// <param name="world">The LODPlane's world matrix.</param>
        /// <param name="colour">The colour of the LODPlane.</param>
        public LODPlane(Matrix world, Color colour)
            : base(world)
        {
            SetPrimatives(new Primative[]
            {
                new PlanePrimative(colour)
            });

            ModelManager.Add(this);
        }

        protected override bool IsInView()
        {
            return CameraManager.CurrentCamera.IsInView(Position);
        }
    }
}
