/* Created 01/09/2014
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
    /// A means of emitting particles in a cylandrical fashion.
    /// </summary>
    public sealed class CylindricalEmitter : IEmitter3D
    {
        Vector3 center;

        float r;
        float h;

        public CylindricalEmitter(float x, float y, float z,
            float radius, float height)
        {
            center = new Vector3(x, y, z);

            r = radius;
            h = height;
        }

        public Vector3 GeneratePosition()
        {
            return center + new Vector3(
                RandomHelper.GetRandom(center.X - r, center.X + r),
                RandomHelper.GetRandom(0, h),
                RandomHelper.GetRandom(center.Z - r, center.Z + r));
        }
    }
}
