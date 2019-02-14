/* Created: 03/10/2013
 * Last Updated: 09/09/2014
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.System.Interfaces;
using Microsoft.Xna.Framework;

namespace Eon.Physics2D.Forces
{
    /// <summary>
    /// Used to specify a specific force.
    /// </summary>
    public interface IForce : IID
    {
        void ApplyForce(object accumulator);

        void Remove();
    }
}
