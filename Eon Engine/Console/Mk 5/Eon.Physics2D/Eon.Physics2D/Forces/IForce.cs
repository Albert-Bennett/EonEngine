/* Created: 03/10/2013
 * Last Updated: 19/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Interfaces.Base;

namespace Eon.Physics2D.Forces
{
    /// <summary>
    /// Used to specify a specific force.
    /// </summary>
    public interface IForce : IID
    {
        void ApplyForce(Particle accumulator);

        void Remove();
    }
}
