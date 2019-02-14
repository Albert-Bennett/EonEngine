/* Created 10/11/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

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

        ParticleComponent startParticle;
        ParticleComponent connectingParticle;

        /// <summary>
        /// The ParticleComponent that determines the beginning of the Spring.
        /// </summary>
        public ParticleComponent StartParticle
        {
            get { return startParticle; }
        }

        /// <summary>
        /// The ParticleComponent that determines the end of the Spring.
        /// </summary>
        public ParticleComponent EndParticle
        {
            get { return connectingParticle; }
        }

        /// <summary>
        /// Creates a new Spring force.
        /// </summary>
        /// <param name="startParticle">The ParticleComponent determining the start of the Spring.</param>
        /// <param name="connectingParticle">The ParticleComponent detemining the end of the Spring.</param>
        /// <param name="springStiffness">The stiffness of the Spring.</param>
        /// <param name="springLenght">The lenght of the Spring.</param>
        /// <param name="springFriction">The amount of friction to be applied to the Spring.</param>
        public Spring(ParticleComponent startParticle,
            ParticleComponent connectingParticle, float springStiffness,
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
            Vector2 startPos = new Vector2(
                startParticle.Owner.World.Translation.X,
                startParticle.Owner.World.Translation.Y);

            Vector2 endPos = new Vector2(
                connectingParticle.Owner.World.Translation.X,
                connectingParticle.Owner.World.Translation.Y);

            Vector2 springVector = startPos - endPos;

            float r = springVector.Length();

            Vector2 force = Vector2.Zero;

            if (r != 0)
                force += (springVector / r) * (r - springLenght) * (-springConstant);

            force += -(startParticle.Velocity -
                connectingParticle.Velocity) * frictionConstant;

            startParticle.WorldForces += force;
            connectingParticle.WorldForces -= force;
        }

        public void Destroy()
        {
            startParticle.Destroy(false);
            connectingParticle.Destroy(false);

            startParticle = null;
            connectingParticle = null;
        }
    }
}
