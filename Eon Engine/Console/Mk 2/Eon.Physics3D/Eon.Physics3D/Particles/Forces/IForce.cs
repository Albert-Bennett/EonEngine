/* Created 05/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Interfaces;

namespace Eon.Physics3D.Particles.Forces
{
    /// <summary>
    /// Used to define a Force that can affect objects.
    /// </summary>
    public interface IForce : IID
    {
        void ApplyForce(IForceAccumulator accumulator);

        void Destroy();
    }
}
