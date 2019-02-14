/* Created: 01/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Particles.D2.EmittionTypes.Base;
using Microsoft.Xna.Framework;

namespace Eon.Particles.D2.EmittionTypes
{
    /// <summary>
    /// Used to define an Emittion type of Point.
    /// </summary>
    public sealed class PointEmitter2D : IEmitter2D
    {
        Vector2 position;

        public PointEmitter2D(float x, float y)
        {
            position = new Vector2(x, y);
        }

        public Vector2 GeneratePosition()
        {
            return position;
        }

        public void SetPosition(Vector2 position)
        {
            this.position = position;
        }
    }
}
