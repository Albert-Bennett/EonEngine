/* Created 03/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Physics2D.Particles;
using Eon.Interfaces;
using Microsoft.Xna.Framework;

namespace Eon.Physics2D.Forces
{
    /// <summary>
    /// Used to specify a specific force.
    /// </summary>
    public interface IWorldForce : IID
    {
        Vector2 CalculateAppliedForce(ParticleObject particle);

        void Remove();
    }
}
