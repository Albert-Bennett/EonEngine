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
    /// Used to define a means of emitting 
    /// Particles in a cubical fashion.
    /// </summary>
    public sealed class CubicalEmitter : IEmitter3D
    {
        Vector3 pos;

        float length;
        float width;
        float height;

        public CubicalEmitter(float x, float y, float z,
            float length, float width, float height)
        {
            pos = new Vector3(x, y, z);

            this.length = length;
            this.width = width;
            this.height = height;
        }

        public Vector3 GeneratePosition()
        {
            return pos + new Vector3(
                RandomHelper.GetRandom(0, width),
                RandomHelper.GetRandom(0, height),
                RandomHelper.GetRandom(0, length));
        }
    }
}
