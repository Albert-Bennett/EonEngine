/* Created 11/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Physics2D.Particles;
using Microsoft.Xna.Framework;

namespace Eon.Physics2D.Forces
{
    /// <summary>
    /// A force that applies friction to objects.
    /// </summary>
    public sealed class Friction : IForce
    {
        float friction;

        string id;

        /// <summary>
        /// The ID of the Friction force.
        /// </summary>
        public string ID
        {
            get { return id; }
        }

        /// <summary>
        /// Creates a new force that applies friction to objects.
        /// </summary>
        /// <param name="frictionValue">The constant value for friction.</param>
        public Friction(string id, float frictionValue)
        {
            this.id = id;
            friction = frictionValue;
        }

        public Vector2 CalculateAppliedForce(ParticleComponent particle)
        {
            return particle.Velocity * friction;
        }

        /// <summary>
        /// Removes this force from the ForceManager. 
        /// </summary>
        public void Remove()
        {
            ForceManager.Remove(this);
        }
    }
}
