/* Created 05/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Physics2D.Forces
{
    /// <summary>
    /// Defines a class that is used to manage force accumulation.
    /// </summary>
    public interface IForceAccumulator
    {
        Vector2 Forces { get; }

        Vector2 Position { get; }
        Vector2 Velocity { get; }

        void AccumulateForces(Vector2 force);
        void Reset();
    }
}
