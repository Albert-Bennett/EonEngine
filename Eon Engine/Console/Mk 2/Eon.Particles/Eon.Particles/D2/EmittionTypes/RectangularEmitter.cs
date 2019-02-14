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
    /// Used to define a means of emitting 
    /// Particles in a cubical fashion.
    /// </summary>
    public sealed class RectangularEmitter : IEmitter2D
    {
        Vector2 pos;

        float width;
        float height;

        public RectangularEmitter(float x, float y,
            float width, float height)
        {
            pos = new Vector2(x, y);

            this.width = width;
            this.height = height;
        }

        public Vector2 GeneratePosition()
        {
            return pos + new Vector2(
                RandomHelper.GetRandom(0, width),
                RandomHelper.GetRandom(0, height));
        }

        public void SetPosition(Vector2 position)
        {
            pos = position;
            position.X -= width / 2;
            position.Y -= height / 2;
        }
    }
}
