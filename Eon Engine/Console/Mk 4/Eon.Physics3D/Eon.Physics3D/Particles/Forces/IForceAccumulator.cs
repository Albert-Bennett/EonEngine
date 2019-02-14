/* Created: 05/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Physics3D.Particles.Forces
{
    /// <summary>
    /// Defines a class that is used to manage force accumulation.
    /// </summary>
    public interface IForceAccumulator
    {
        Vector3 Forces { get; }

        Vector3 Position { get; }
        Vector3 Velocity { get; }

        void AccumulateForces(Vector3 force);
        void Reset();
    }
}
