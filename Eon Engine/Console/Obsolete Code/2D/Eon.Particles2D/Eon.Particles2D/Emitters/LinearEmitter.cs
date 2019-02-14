/* Created 04/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Helpers;
using Microsoft.Xna.Framework;
using System;

namespace Eon.Particles2D.Emitters
{
    /// <summary>
    /// Defines an emitter that can 
    /// spawn Particles between two points.
    /// </summary>
    public class LinearEmitter : IEmitterType
    {
        Vector2 pos;
        Vector2 range;

        /// <summary>
        /// The position from where the Particles can be spawned from.
        /// </summary>
        public Vector2 Position
        {
            get { return pos; }
            set { pos = value; }
        }

        /// <summary>
        /// Creates a new LinearEmitter.
        /// </summary>
        /// <param name="position">The centeral possition from which 
        /// Particles can be spawned from.</param>
        /// <param name="range">The maximum range in any direction 
        /// that Particles can be spawned from.</param>
        public LinearEmitter(float x, float y, float maxX, float maxY)
        {
            pos = new Vector2(x, y);
            this.range = new Vector2(maxX, maxY);
        }

        /// <summary>
        /// Return a position where Particles can be spawned from.
        /// </summary>
        /// <returns></returns>
        public Vector2 CreateEmittionPoint()
        {
            return RandomHelper.GetRandom(pos - range, pos + range);
        }
    }
}
