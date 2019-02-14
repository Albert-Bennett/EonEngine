/* Created 04/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Particles2D.Emitters
{
    /// <summary>
    /// Defines an emmition type that creates 
    /// spawn points from a single point.
    /// </summary>
    public class PointEmitter : IEmitterType
    {
        Vector2 pos;

        /// <summary>
        /// The position where the Particles originate from.
        /// </summary>
        public Vector2 Position
        {
            get { return pos; }
            set { pos = value; }
        }

        /// <summary>
        /// Creates a new PointEmitter.
        /// </summary>
        /// <param name="position">The point iof emmision.</param>
        public PointEmitter(float x, float y)
        {
            pos = new Vector2(x, y);
        }

        /// <summary>
        /// Creates a position where Particle can be emitted from.
        /// </summary>
        /// <returns>The generated spawn point.</returns>
        public Vector2 CreateEmittionPoint()
        {
            return pos;
        }
    }
}
