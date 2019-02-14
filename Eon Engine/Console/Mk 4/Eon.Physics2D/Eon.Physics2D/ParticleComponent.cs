/* Created: 02/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Physics2D.Forces;
using Eon.System.Interfaces;
using Microsoft.Xna.Framework;

namespace Eon.Physics2D
{
    /// <summary>
    /// Defines an ObjectComponent that is used 
    /// to add particle physics to a GameObject.
    /// </summary>
    public sealed class ParticleComponent : ObjectComponent, IForceAccumulator, IUpdate
    {
        Vector2 velocity;
        Vector2 acceleration;
        Vector2 worldForces;

        float mass;

        public int Priority
        {
            get { return 0; }
        }

        /// <summary>
        /// The velocity of the ParticleComponent.
        /// </summary>
        public Vector2 Velocity
        {
            get { return velocity; }
        }

        /// <summary>
        /// The mass of the particle.
        /// </summary>
        public float Mass
        {
            get { return mass; }
        }

        /// <summary>
        /// The applyable forces.
        /// </summary>
        public Vector2 Forces
        {
            get { return worldForces; }
        }

        /// <summary>
        /// The position of the Particle2DComponent.
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return new Vector2(
                    Owner.World.Position.X,
                    Owner.World.Position.Y);
            }
        }

        /// <summary>
        /// Creates a new ParticleComponent.
        /// </summary>
        /// <param name="acceleration">The acceleration of the Particle.</param>
        /// <param name="mass">The mass of the ParticleComponent.</param>
        public ParticleComponent(string id, Vector2 acceleration, float mass)
            : base(id)
        {
            this.acceleration = acceleration;
            worldForces = Vector2.Zero;
            velocity = Vector2.Zero;

            this.mass = mass;

            ForceManager.Add(this);
        }

        public void _Update()
        {
            float dt = (float)Common.ElapsedTimeDelta.TotalSeconds;

            acceleration += (worldForces * mass) * dt;
            velocity += acceleration * dt;
            Owner.World.Position += new Vector3(velocity, 0);

            Reset();
        }

        public void _PostUpdate() { }

        /// <summary>
        /// Used to accumulate outside forces.
        /// </summary>
        /// <param name="force">The magnitude of the force.</param>
        public void AccumulateForces(Vector2 force)
        {
            worldForces += force;
        }

        /// <summary>
        /// Resets the accumulated forces.
        /// </summary>
        public void Reset()
        {
            worldForces = Vector2.Zero;
        }

        public override void Destroy(bool remove)
        {
            ForceManager.Remove(this);

            base.Destroy(remove);
        }
    }
}
