/* Created 02/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Interfaces;
using Eon.Physics2D.Particles;
using Microsoft.Xna.Framework;

namespace Eon.Physics2D
{
    /// <summary>
    /// Defines an ObjectComponent that is used 
    /// to add particle physics to a GameObject.
    /// </summary>
    public sealed class ParticleComponent : ObjectComponent, IUpdate
    {
        Vector2 position;
        Vector2 velocity;
        Vector2 acceleration;
        Vector2 worldForces;

        float mass;
        float area = 1;

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
        /// The surface area of the ParticleComponent.
        /// </summary>
        public float SurfaceArea
        {
            get { return area; }
        }

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
            set { position = value; }
        }

        internal Vector2 WorldForces
        {
            get { return worldForces; }
            set { worldForces = value; }
        }

        /// <summary>
        /// Creates a new ParticleComponent.
        /// </summary>
        /// <param name="position">The position of the Particle.</param>
        /// <param name="acceleration">The acceleration of the Particle.</param>
        /// <param name="mass">The mass of the ParticleComponent.</param>
        /// <param name="area">The area of the ParticleComponent.</param>
        public ParticleComponent(string id, Vector2 position,
            Vector2 acceleration, float mass, Rectangle area)
            : this(id, position, acceleration, mass)
        {
            this.area = area.Width * area.Height;
        }

        /// <summary>
        /// Creates a new ParticleComponent.
        /// </summary>
        /// <param name="position">The position of the Particle.</param>
        /// <param name="acceleration">The acceleration of the Particle.</param>
        /// <param name="mass">The mass of the ParticleComponent.</param>
        /// <param name="area">The area of the ParticleComponent.</param>
        public ParticleComponent(string id, Vector2 position,
            Vector2 acceleration, float mass, float area)
            : this(id, position, acceleration, mass)
        {
            this.area = area;
        }

        /// <summary>
        /// Creates a new ParticleComponent.
        /// </summary>
        /// <param name="position">The position of the Particle.</param>
        /// <param name="acceleration">The acceleration of the Particle.</param>
        /// <param name="mass">The mass of the ParticleComponent.</param>
        public ParticleComponent(string id, Vector2 position,
            Vector2 acceleration, float mass)
            : base(id)
        {
            this.position = position;
            this.acceleration = acceleration;
            worldForces = Vector2.Zero;
            velocity = Vector2.Zero;

            this.mass = mass;

            ParticleManager.Add(this);
        }

        public void _Update()
        {
            float dt = (float)Common.ElapsedTimeDelta.TotalSeconds;

            velocity = mass * (acceleration + worldForces) * dt;// *dt;
            position += velocity;

            worldForces = Vector2.Zero;
        }

        public override void Destroy(bool remove)
        {
            ParticleManager.Remove(this);

            base.Destroy(remove);
        }
    }
}
