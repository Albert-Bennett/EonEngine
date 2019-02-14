/* Created 02/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Aeon Studios.
 */

using Microsoft.Xna.Framework;

namespace Eon.Physics2D.Particles
{
    /// <summary>
    /// Defines a particle.
    /// </summary>
    public sealed class ParticleObject
    {
        Vector2 position;
        Vector2 velocity;
        Vector2 acceleration;
        Vector2 worldForces;

        float mass;
        float area = 1;

        string id = "None";

        /// <summary>
        /// The id of the CollisionObject this is attached to.
        /// </summary>
        public string AttachedToID
        {
            get { return id; }
            set { AttachedToID = value; }
        }

        /// <summary>
        /// The velocity of the ParticleObject.
        /// </summary>
        public Vector2 Velocity
        {
            get { return velocity; }
        }

        /// <summary>
        /// The surface area of the ParticleObject.
        /// </summary>
        public float SurfaceArea
        {
            get { return area; }
        }

        /// <summary>
        /// A delegate used catch when the
        /// ParticleObject has updated.
        /// </summary>
        public OnParticleUpdateEvent OnUpdate;

        /// <summary>
        /// The acceleration of the Particle.
        /// </summary>
        public Vector2 Acceleration
        {
            get { return acceleration; }
            internal set { acceleration = value; }
        }

        /// <summary>
        /// The mass of the particle.
        /// </summary>
        public float Mass
        {
            get { return mass; }
        }

        /// <summary>
        /// The position of the Particle.
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            internal set { position = value; }
        }

        internal Vector2 WorldForces
        {
            get { return worldForces; }
            set { worldForces = value; }
        }

        /// <summary>
        /// Creates a new ParticleObject.
        /// </summary>
        /// <param name="position">The position of the Particle.</param>
        /// <param name="acceleration">The acceleration of the Particle.</param>
        /// <param name="mass">The mass of the ParticleObject.</param>
        /// <param name="area">The area of the ParticleObject.</param>
        public ParticleObject(Vector2 position,
            Vector2 acceleration, float mass, Rectangle area)
            : this(position, acceleration, mass)
        {
            this.area = area.Width * area.Height;
        }

        /// <summary>
        /// Creates a new ParticleObject.
        /// </summary>
        /// <param name="position">The position of the Particle.</param>
        /// <param name="acceleration">The acceleration of the Particle.</param>
        /// <param name="mass">The mass of the ParticleObject.</param>
        /// <param name="area">The area of the ParticleObject.</param>
        public ParticleObject(Vector2 position,
            Vector2 acceleration, float mass, float area)
            : this(position, acceleration, mass)
        {
            this.area = area;
        }

        /// <summary>
        /// Creates a new ParticleObject.
        /// </summary>
        /// <param name="position">The position of the Particle.</param>
        /// <param name="acceleration">The acceleration of the Particle.</param>
        /// <param name="mass">The mass of the ParticleObject.</param>
        public ParticleObject(Vector2 position, Vector2 acceleration, float mass)
        {
            this.position = position;
            this.acceleration = acceleration;
            worldForces = Vector2.Zero;
            velocity = Vector2.Zero;

            this.mass = mass;

            ParticleManager.Add(this);
        }

        public void Update()
        {
            float dt = (float)Common.ElapsedTimeDelta.TotalSeconds;

            velocity = mass * (acceleration + worldForces) * dt;// *dt;
            position += velocity;

            if (OnUpdate != null)
                OnUpdate();

            worldForces = Vector2.Zero;
        }

        /// <summary>
        /// Removes this Particle from the ParticleManager. 
        /// </summary>
        public void Remove()
        {
            ParticleManager.Remove(this);
        }
    }
}
