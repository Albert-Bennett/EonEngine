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
    /// Defines an means for emitting particles in a spherical manner.
    /// </summary>
    public sealed class CircularEmitter : IEmitter2D
    {
        Vector2 center;
        Vector2 r;

        public CircularEmitter(float centerX, float centerY, float radius)
        {
            center = new Vector2(centerX, centerY);
            r = new Vector2(radius);
        }

        public Vector2 GeneratePosition()
        {
            return RandomHelper.GetRandom(center - r, center + r);
        }

        public void SetPosition(Vector2 position)
        {
            center = position;
        }
    }
}
