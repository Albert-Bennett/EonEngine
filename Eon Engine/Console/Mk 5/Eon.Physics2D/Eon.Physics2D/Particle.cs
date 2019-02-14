/* Created: 02/10/2013
 * Last Updated: 25/09/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Physics2D.Forces;
using Microsoft.Xna.Framework;

namespace Eon.Physics2D
{
    /// <summary>
    /// Defines an ObjectComponent that is used 
    /// to add Particle2D physics to a GameObject.
    /// </summary>
    public sealed class Particle
    {
        Vector2 velocity;
        Vector2 acceleration;
        Vector2 worldForces;

        Vector2 position;

        float mass;
        bool isStatic;

        /// <summary>
        /// The velocity of the Particle2D.
        /// </summary>
        public Vector2 Velocity
        {
            get { return velocity; }
        }

        /// <summary>
        /// The mass of the Particle2D.
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
        /// The position of the Particle2D.
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// Does the Particle move.
        /// </summary>
        public bool IsStatic
        {
            get { return isStatic; }
        }

        /// <summary>
        /// Creates a new Particle2D.
        /// </summary>
        /// <param name="position">The position of the Particle.</param>
        /// <param name="acceleration">The acceleration of the Particle.</param>
        /// <param name="mass">The mass of the Particle.</param>
        public Particle(Vector2 position, Vector2 acceleration, float mass)
        {
            this.acceleration = acceleration;
            worldForces = Vector2.Zero;
            velocity = Vector2.Zero;

            this.position = position;

            this.mass = mass;

            ForceManager.Add(this);
        }

        /// <summary>
        /// Creates a new Particle2D.
        /// </summary>
        /// <param name="position">The position of the Particle.</param>
        /// <param name="acceleration">The acceleration of the Particle.</param>
        /// <param name="mass">The mass of the Particle.</param>
        /// <param name="isStatic">Does the Particle move.</param>
        public Particle(Vector2 position, Vector2 acceleration,
            float mass, bool isStatic)
            : this(position, acceleration, mass)
        {
            this.isStatic = isStatic;
        }

        internal void Update()
        {
            float dt = (float)Common.ElapsedTimeDelta.TotalSeconds;

            acceleration += (worldForces * mass) * dt;
            velocity += acceleration * dt;

            position += velocity;

            Reset();
        }

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

        /// <summary>
        /// Destroys the Particle2D.
        /// </summary>
        public void Destroy()
        {
            ForceManager.Remove(this);
        }
    }
}
