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
    /// An object used to apply an aerodynamic force to objects.
    /// </summary>
    public sealed class Drag : IWorldForce
    {
        string id;

        float fluidDensity;
        float frictionCoefficient;

        /// <summary>
        /// The ID of this Drag force.
        /// </summary>
        public string ID
        {
            get { return id; }
        }

        /// <summary>
        /// Creates a new Drag force.
        /// </summary>
        /// <param name="id">The id of the Drag force.</param>
        /// <param name="fluidDensity">The density of the fliud.</param>
        /// <param name="frictionCoefficient">The co-efficient of the friction to be applied.</param>
        public Drag(string id, float fluidDensity, float frictionCoefficient)
        {
            this.id = id;
            this.fluidDensity = fluidDensity;
            this.frictionCoefficient = frictionCoefficient;
        }

        public Vector2 CalculateAppliedForce(ParticleComponent particle)
        {
            return 0.5f * frictionCoefficient *
                particle.SurfaceArea * fluidDensity * particle.Velocity;
        }

        public void Remove()
        {
            ForceManager.Remove(this);
        }
    }
}
