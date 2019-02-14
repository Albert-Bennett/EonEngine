/* Created 10/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Eon.Physics2D.Particles;
using Microsoft.Xna.Framework;

namespace Eon.Physics2D.Forces.LocalForces
{
    /// <summary>
    /// Used to define a Spring force.
    /// </summary>
    public sealed class Spring
    {
        float springConstant;
        float springLenght;
        float frictionConstant;

        ParticleObject startParticle;
        ParticleObject connectingParticle;

        /// <summary>
        /// The ParticleObject that determines the beginning of the Spring.
        /// </summary>
        public ParticleObject StartParticle
        {
            get { return startParticle; }
        }

        /// <summary>
        /// The ParticleObject that determines the end of the Spring.
        /// </summary>
        public ParticleObject EndParticle
        {
            get { return connectingParticle; }
        }

        /// <summary>
        /// Creates a new Spring force.
        /// </summary>
        /// <param name="startParticle">The ParticleObject determining the start of the Spring.</param>
        /// <param name="connectingParticle">The ParticleObject detemining the end of the Spring.</param>
        /// <param name="springStiffness">The stiffness of the Spring.</param>
        /// <param name="springLenght">The lenght of the Spring.</param>
        /// <param name="springFriction">The amount of friction to be applied to the Spring.</param>
        public Spring(ParticleObject startParticle,
            ParticleObject connectingParticle, float springStiffness,
            float springLenght, float springFriction)
        {
            this.startParticle = startParticle;
            this.connectingParticle = connectingParticle;

            springConstant = springStiffness;
            this.springLenght = springLenght;
            frictionConstant = springFriction;
        }

        /// <summary>
        /// Calculates the forces applied by the Spring.
        /// </summary>
        public void CalculateForce()
        {
            Vector2 springVector = startParticle.Position -
                connectingParticle.Position;

            float r = springVector.Length();

            Vector2 force = Vector2.Zero;

            if (r != 0)
                force += (springVector / r) * (r - springLenght) * (-springConstant);

            force += -(startParticle.Velocity -
                connectingParticle.Velocity) * frictionConstant;

            startParticle.WorldForces += force;
            connectingParticle.WorldForces -= force;
        }

        public void Dispose()
        {
            startParticle.Remove();
            connectingParticle.Remove();

            startParticle = null;
            connectingParticle = null;
        }
    }
}
