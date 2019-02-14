/* Created: 24/11/2014
 * Last Updated: 14/12/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Rendering3D.Cameras;
using Eon.Rendering3D.Primatives.Constructs;
using Microsoft.Xna.Framework;

namespace Eon.Rendering3D.Primatives
{
    /// <summary>
    /// Used to define an LODLine model.
    /// </summary>
    public sealed class LODLine : BaseLOD
    {
        /// <summary>
        /// Creates a new LODLine.
        /// </summary>
        /// <param name="position">The starting position of the LODLine.</param>
        /// <param name="direction">The direction of the LODLine.</param>
        /// <param name="length">The lenght of the LODLine.</param>
        /// <param name="width">The width of the LODLine.</param>
        /// <param name="colour">The colour of the LODLine.</param>
        public LODLine(Vector3 position,
            Vector3 direction, float length, float width, Color colour)
            : base(Matrix.Identity)
        {
            SetPrimatives(new Primative[]
            {
                new LinePrimative(colour, position, direction, length, width)
            });

            ModelManager.Add(this);
        }

        protected override bool IsInView()
        {
            return CameraManager.CurrentCamera.IsInView(Position);
        }
    }
}
