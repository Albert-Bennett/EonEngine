/* Created: 01/09/2014
 * Last Updated: 11/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.Helpers;
using Eon.Particles.D3.EmittionTypes.Base;
using Microsoft.Xna.Framework;

namespace Eon.Particles.D3.EmittionTypes
{
    /// <summary>
    /// Defines an means for emitting particles in a spherical manner.
    /// </summary>
    public sealed class SphericalEmitter : IEmitter3D
    {
        Vector3 center;
        Vector3 r;

        public SphericalEmitter(float centerX, float centerY,
            float centerZ, float radius)
        {
            center = new Vector3(centerX, centerY, centerZ);
            r = new Vector3(radius);
        }

        public Vector3 GeneratePosition()
        {
            return RandomHelper.GetRandom(center - r, center + r);
        }

        public void SetPosition(Vector3 position)
        {
            this.center = position;
        }
    }
}
