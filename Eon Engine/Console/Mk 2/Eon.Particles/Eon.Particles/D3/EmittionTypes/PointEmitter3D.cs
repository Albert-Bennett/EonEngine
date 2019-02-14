/* Created 01/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Particles.D3.EmittionTypes.Base;
using Microsoft.Xna.Framework;

namespace Eon.Particles.D3.EmittionTypes
{
    /// <summary>
    /// Used to define an Emittion type of Point.
    /// </summary>
    public sealed class PointEmitter3D : IEmitter3D
    {
        Vector3 position;

        public PointEmitter3D(float x, float y, float z)
        {
            position = new Vector3(x, y, z);
        }

        public Vector3 GeneratePosition()
        {
            return position;
        }
    }
}
