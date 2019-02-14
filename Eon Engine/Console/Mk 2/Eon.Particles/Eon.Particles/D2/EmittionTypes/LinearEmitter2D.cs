/* Created 01/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Maths.Helpers;
using Eon.Particles.D2.EmittionTypes.Base;
using Microsoft.Xna.Framework;

namespace Eon.Particles.D2.EmittionTypes
{
    /// <summary>
    /// Used to define an emittion type of Linear.
    /// </summary>
    public sealed class LinearEmitter2D : IEmitter2D
    {
        Vector2 startPos;
        Vector2 endPos;

        public LinearEmitter2D(float startX, float startY,
            float endX, float endY)
        {
            startPos = new Vector2(startX, startY);
            endPos = new Vector2(endX, endY);
        }

        public Vector2 GeneratePosition()
        {
            return RandomHelper.GetRandom(startPos, endPos);
        }

        public void SetPosition(Vector2 position)
        {
            float dist = Vector2.Distance(startPos, endPos);
            dist /= 2;

            Vector2 direct = endPos - startPos;
            direct.Normalize();

            startPos = position - (direct * dist);
            endPos = position + (direct * dist);
        }
    }
}
