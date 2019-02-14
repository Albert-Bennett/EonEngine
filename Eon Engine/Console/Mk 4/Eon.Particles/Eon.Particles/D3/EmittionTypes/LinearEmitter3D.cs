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
    /// Used to define an emittion type of Linear.
    /// </summary>
    public sealed class LinearEmitter3D : IEmitter3D
    {
        Vector3 startPos;
        Vector3 endPos;

        public LinearEmitter3D(float startX, float startY, float startZ,
            float endX, float endY, float endZ)
        {
            startPos = new Vector3(startX, startY, startZ);
            endPos = new Vector3(endX, endY, endZ);
        }

        public Vector3 GeneratePosition()
        {
            return RandomHelper.GetRandom(startPos, endPos);
        }

        public void SetPosition(Vector3 position)
        {
            float dist = Vector3.Distance(startPos, endPos);
            dist /= 2;

            Vector3 direct = endPos - startPos;
            direct.Normalize();

            startPos = position - (direct * dist);
            endPos = position + (direct * dist);
        }
    }
}
