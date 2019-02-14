/* Created 04/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;
using Microsoft.Xna.Framework;

namespace Eon.Particles2D.Emitters
{
    /// <summary>
    /// Creates an emmitter type that can be used
    /// to emitt Particles from a range of positions
    /// from inside a circle.
    /// </summary>
    public class CircleEmitter : IEmitterType
    {
        Vector2 position;
        float radius;

        /// <summary>
        /// The center position of this CircleEmitter.
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// Creates a new CircleEmitter. 
        /// </summary>
        /// <param name="center">The center of the CircleEmitter.</param>
        /// <param name="radius">The radius of the CircleEmitter.</param>
        public CircleEmitter(float x, float y, float radius)
        {
            this.position = new Vector2(x, y);
            this.radius = radius;
        }

        /// <summary>
        /// Generates a spawning point in the circle.
        /// </summary>
        /// <returns>The generated spawn point.</returns>
        public Vector2 CreateEmittionPoint()
        {
            Vector2 r = new Vector2(radius, radius);

            return RandomHelper.GetRandom(
                position - r, position + r);
        }
    }
}
